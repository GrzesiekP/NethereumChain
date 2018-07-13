using Nethereum.ABI.FunctionEncoding.Attributes;

namespace NethereumChain.Core.Models
{
    [FunctionOutput]
    public class Location
    {
        [Parameter("uint", 1)]
        public int LocationId{ get; set; }
        [Parameter("uint", 2)]
        public int PreviousLocationId { get; set; }
        [Parameter("string", 3)]
        public string LocationName { get; set; }
        [Parameter("string", 4)]
        public string PreviousLocationName { get; set; }
        [Parameter("uint", 5)]
        public long Timestamp { get; set; }
    }
}
