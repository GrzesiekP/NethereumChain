cd eth
powershell truffle compile
cd ..

.\NethereumChain.Core\solc\solc.exe .\eth\contracts\SupplyChain.sol --abi > .\NethereumChain.Core\solc\ABI\SupplyChainAbi.txt
.\NethereumChain\solc\solc.exe .\eth\contracts\SupplyChain.sol --abi > .\NethereumChain\solc\ABI\SupplyChainAbi.txt

cat .\NethereumChain.Core\solc\ABI\SupplyChainAbi.txt
cat .\NethereumChain\solc\ABI\SupplyChainAbi.txt