using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.AzureStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NethereumChain.Core;
using NethereumChain.Core.Contracts;
using NethereumChain.Core.Models;

namespace NethereumChain.Controllers
{
    [Produces("application/json")]
    [Route("api/location")]
    public class LocationController : Controller
    {
        private readonly SupplyContractRepository _repository = new SupplyBlockchain(
            AppConfigurationProvider.BlockchainAddress,
            AppConfigurationProvider.ContractAddress("SupplyChain")).SupplyChainContract;

        // GET: api/Location
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _repository.GetAllLocations());

        // GET: api/Location/5
        [HttpGet("{locationName}", Name = "Get")]
        public async Task<IActionResult> Get(string locationName)
        {
            var location = await _repository.GetLocation(locationName);

            if (location == null)
                return NotFound();

            return Ok(location);
        }
        
        // POST: api/Location
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateLocationCommand createLocation)
        {
            var result = _repository.AddNewLocation(
                createLocation.UserAddress,
                createLocation.Gas,
                createLocation.Value,
                new Location {LocationName = createLocation.LocationName}
            );

            if (result != null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            var createdLocation = await _repository.GetLocation(createLocation.LocationName);

            return CreatedAtRoute("Get", new {locationName = createdLocation.LocationName}, createdLocation);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) => BadRequest("You cannot delete from blockchain");
    }
}
