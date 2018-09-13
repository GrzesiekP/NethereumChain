using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NethereumChain.Core.Contracts;
using NethereumChain.Core.Models;

namespace NethereumChain.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/location")]
    public class LocationController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly ISupplyContractRepository _repository;

        public LocationController(IMemoryCache cache, ISupplyContractRepository repository)
        {
            _cache = cache;
            _repository = repository;
        } 

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (_cache.TryGetValue("Locations", out List<Location> locations))
                return StatusCode((int)HttpStatusCode.NotModified);

            locations = await _repository.GetAllLocations();
            _cache.Set("Locations", locations, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            });

            return Ok(locations);
        }

        [HttpGet("{locationName}", Name = "Get")]
        public async Task<IActionResult> Get(string locationName)
        {
            if (_cache.TryGetValue($"Location-{locationName}", out Location location))
                return StatusCode((int)HttpStatusCode.NotModified);

            location = await _repository.GetLocation(locationName);

            if (location == null)
                return NotFound();

            _cache.Set($"Location-{locationName}", location, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });

            return Ok(location);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateLocationCommand createLocation)
        {
            if (string.IsNullOrWhiteSpace(createLocation?.LocationName))
                return BadRequest();

            var result = await _repository.AddNewLocation(
                createLocation.UserAddress,
                createLocation.UserPrivateKey,
                createLocation.Gas,
                createLocation.Value,
                new Location {LocationName = createLocation.LocationName}
            );

            return result == null ? 
                (IActionResult) StatusCode(StatusCodes.Status500InternalServerError) : 
                Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) => StatusCode(StatusCodes.Status405MethodNotAllowed);
    }
}
