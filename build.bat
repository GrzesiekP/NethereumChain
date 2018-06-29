cd eth
powershell truffle compile
cd ..

.\BusinessLogic\solc\solc.exe .\eth\contracts\SupplyChain.sol --abi > .\BusinessLogic\solc\ABI\SupplyChainAbi.txt

cat .\BusinessLogic\solc\ABI\SupplyChainAbi.txt