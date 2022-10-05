using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            var balanceInEther = Web3.Convert.FromWei(balance.Value);
            return balanceInEther.ToString();
        }

        public static async Task<string> GetAddressTrack(string address)
        {
            string result = "";
            var web3 = new Web3("https://mainnet.infura.io/v3/e05cefe05ec349cf859625f63f0f965d");
            var block = await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(BlockParameter.CreatePending());
            foreach (var txn in block.Transactions)
            {
                if(txn.To == address || txn.From == address)
                {
                    result += $"From: {txn.From} \r\n To: {txn.To} \r\n Value: {txn.Value} \r\n";
                }
            }
            return result;
        }

    }

}
