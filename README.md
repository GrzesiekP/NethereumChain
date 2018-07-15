# NethereumChain
https://nethereumchain.azurewebsites.net/api/location

## Prerequisites:
1. Ganache
2. Truffle
3. Metamask

## How to run (Ganache version, deprecated)
1. Run Ganache
2. Copy hostname and port number from Ganache (RPC SERVER) to NethereumChain appsettings.json to Blockchain section
3. Run powershell in NethereumChain/eth directory
4. Run command "truffle compile"
5. Run command "truffle deploy --reset --network ganache"
6. In console output find and copy deployed contract address: e.g. "SupplyChain: 0x6ac0f5f416ecb32cd14db9df5f0ca11f41b5c625" where 0x6ac0f5f416ecb32cd14db9df5f0ca11f41b5c625 is the address.
7. Paste contract address in appsettings.json "SupplyChain": "0x6ac0f5f416ecb32cd14db9df5f0ca11f41b5c625"

## How to run (Testnet)
1. Set infura network address and API Key in appsettings.json "Infura" section.
2. Deploy contract to this network. You can use Remix and Metamask.
3. Set deployed contract address in appsettings.json "SupplyChain" section.
4. Run application.

## Important notes
1. You need an account with some ether on test network to perform POST action.
2. POST is submitting transaction, you have to wait for it to be processed. API method returns transaction address, so you can check it.
3. Suggested gas amount is at least 400000, value 0.

