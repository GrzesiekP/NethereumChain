pragma solidity ^0.4.17;

contract SupplyChain {
    struct Location {
        string LocationName;
        uint LocationId;
        uint PreviousLocationId;
        uint Timestamp;
        string Secret;
    }

    mapping (uint=>Location) Chain;
    uint16 ChainCount = 0;

    function AddNewLocation(uint locationId, string locationName, string secret) public {
        Location memory newLocation;
        newLocation.LocationId = locationId;
        newLocation.LocationName = locationName;
        newLocation.Secret = secret;
        newLocation.Timestamp = now;
        if (ChainCount != 0){
            newLocation.PreviousLocationId = Chain[ChainCount].LocationId;
        }

        Chain[ChainCount] = newLocation;
        ChainCount++;
    }

    function GetChainCount() public view returns (uint16) {
        return ChainCount;
    }

    function GetLocation(uint16 ChainNumber) public view returns (string, uint, uint, uint, string) {
        return (
            Chain[ChainNumber].LocationName,
            Chain[ChainNumber].LocationId,
            Chain[ChainNumber].PreviousLocationId,
            Chain[ChainNumber].Timestamp,
            Chain[ChainNumber].Secret
        );
    }
}