pragma solidity ^0.4.17;

contract SupplyChain {
    struct Location {
        uint LocationId;
        uint PreviousLocationId;
        string LocationName;
        string PreviousLocationName;
        uint Timestamp;
    }

    mapping (string=>Location) Chain;
    mapping (uint=>string) public ChainDictionary;
    uint16 ChainCount = 0;
    string LastInsertedLocation = "";

    function AddNewLocation(string locationName) public {
        Location memory newLocation;
        newLocation.LocationId = ChainCount;
        newLocation.LocationName = locationName;
        newLocation.Timestamp = now;
        if (ChainCount != 0){
            newLocation.PreviousLocationId = Chain[LastInsertedLocation].LocationId;
            newLocation.PreviousLocationName = Chain[LastInsertedLocation].LocationName;
        }

        Chain[locationName] = newLocation;
        ChainDictionary[ChainCount] = locationName;
        ChainCount++;
        LastInsertedLocation = locationName;
    }

    function GetChainCount() public view returns (uint16) {
        return ChainCount;
    }

    function GetLocation(string locationName) public view returns (uint, uint, string, string, uint) {
        return (
            Chain[locationName].LocationId,
            Chain[locationName].PreviousLocationId,
            Chain[locationName].LocationName,
            Chain[locationName].PreviousLocationName,
            Chain[locationName].Timestamp
        );
    }
}