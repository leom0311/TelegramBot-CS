using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace Bot.BlockChain
{
    public  class BlockWeb3
    {
         public static async Task<string> GetBalance(string address)
        {
            var web3 = new Web3("https://mainnet.infura.io/v3/e05cefe05ec349cf859625f63f0f965d");
            var balance = await web3.Eth.GetBalance.SendRequestAsync(address);
            return balance.Value.ToString();
        }
        public static async Task<string> GetAddressTrack(string address)
        {
            Console.WriteLine(address);
            var web3 = new Web3("https://mainnet.infura.io/v3/e05cefe05ec349cf859625f63f0f965d");
            var block = await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(BlockParameter.CreatePending());
            foreach (var txn in block.Transactions)
            {
                if(txn.To == address || txn.From == address)
                {
                    return $"From: {txn.From}, To: {txn.To}, Value: {txn.Value} \r\n";

                }
            }
            return "";
        }

    }



}
