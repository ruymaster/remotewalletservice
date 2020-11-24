using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace CryptoWallet.BlockchainClients
{
    
    public class Erlondlient : IBlockchainClient
    {
        public string url;
        
        //public static HttpClient httpClient;
        List<string> paymentAddresses;
        List<string> contractAddresses;
        public long lastCheckedBlockNumber;
        public long latestBlockNumber;
        public static Uri gethProxy;
        int limitBlocks;
        public ErlondProxy erlondProxy;

        public Erlondlient(string serverUrl, long  _lastCheckedBlockNumber =0)
        {
            url = serverUrl;
            lastCheckedBlockNumber = _lastCheckedBlockNumber;
            erlondProxy = new ErlondProxy();
            //gethProxy = new Uri(url);
            //web3 = new GethRpcProxy(gethProxy);
        }
        public Erlondlient(string serverUrl, List<string> _addresses, List<string> _contractAddresses,long _lastCheckedBlockNumber, int _limitBlocks=0)
        {
            url = serverUrl;
            lastCheckedBlockNumber = _lastCheckedBlockNumber;
            //gethProxy = new Uri(url);
            //web3 = new GethRpcProxy(gethProxy);
            paymentAddresses = _addresses.ConvertAll(x => x.ToLower());
            contractAddresses = _contractAddresses.ConvertAll(x => x.ToLower());
            erlondProxy = new ErlondProxy();
            limitBlocks = _limitBlocks;
        }
        public  async Task<decimal> LoadBalance(string address, string currency)
        {
            try
            {
                var addressFrom = address; // PRECREDITED ACCOUNT IN DEV testchain CHAIN                
                var accountBalance = "0";  //await web3.Eth.GetBalanceAsync(addressFrom);
                var transaction = await erlondProxy.GetTransactionAsync(0);
                if (accountBalance != null && accountBalance != "")
                    return Convert.ToDecimal(accountBalance);
            }
            catch
            {

            }
            return 0;
        }
        //Task<IEnumerable<decimal>> GetBlockHeigh(IEnumerable<decimal> height);
        public async Task<Deposit> LoadDeposit(string txId)
        {

            //await Thread.(10); // web3.Eth.GetTransactionByHashAsync(txId);
            var transaction =await erlondProxy.GetTransactionAsync(0);
            //var result = new Deposit
            //{
            //    txid = transaction.Hash,
            //    //amount = Convert.ToDecimal(transaction.Value),
            //    amount = transaction.Value,
            //    toAddress = transaction.To
            //};
            return new Deposit();
        }
        public async Task<CheckedDeposits> GetNextDeposit(List<string> _addresses, List<string> _contractAddress = null, int _limitBlocks = 100)
        {
            CheckedDeposits checkedDeposits= new CheckedDeposits();
            List<Deposit> deposits=new List<Deposit>();            

            latestBlockNumber = await erlondProxy.GetLastBlockNumber();

            long lastBlockToCheck = latestBlockNumber < lastCheckedBlockNumber + _limitBlocks ? latestBlockNumber : lastCheckedBlockNumber + _limitBlocks;
            Console.WriteLine("checking block "+ lastCheckedBlockNumber +"~" + lastBlockToCheck + " from ");

            for (long i=lastCheckedBlockNumber; i<= lastBlockToCheck; i++)
            {
                var res = await LoadBlock(i);                
                if(res!=null && res.Count()>0)deposits.AddRange(res.ToList());
                Console.WriteLine("processed block " + i);
                Thread.Sleep(30);
            }
            if (deposits.Count > 0)
            {
                checkedDeposits.deposits = deposits;
            }
            checkedDeposits.lastCheckedBlock = lastBlockToCheck;
            checkedDeposits.latestBlockNumber = latestBlockNumber;

            return checkedDeposits;
        }
        async Task<List<Deposit>> LoadBlock(long _blockNumber)
        {
            List<Deposit> depositList = new List<Deposit>();
            ErlondHyperBlock block = await erlondProxy.GetBlockByNumberAsync(_blockNumber);

            // Block block = await web3.Eth.GetBlockByNumberAsync("0x" + _blockNumber.ToString("X"), true);

            if (block == null || block.Transactions==null || block.Transactions.Count() < 1) return null;
            foreach(ErlondTransaction t in block.Transactions)
            {
                //if( paymentAddresses.Contains(t.ToAddress.ToLower()))
                //long lNumber = Convert.ToInt64(t.BlockNumber, 16);
                if (t.Receiver == null || t.Receiver == "") continue;

                if (paymentAddresses.Contains(t.Receiver.ToLower()))
                {
                    //BigInteger bigAmount = BigInteger.Parse(t.Value);
                    Console.WriteLine("====checked txid " + t.Hash);                    
                    depositList.Add(new Deposit{txid = t.Hash, amount =  t.Value, toAddress = t.Receiver, confirm= latestBlockNumber - _blockNumber, blockNumber=_blockNumber, contractAddress ="" });
                }                
            }
            if (depositList.Count() > 0) return depositList;
            else return null;
        }
        static string BuildRpcRequest(string methodeName, params object[] parameters)
        {
            // TODO : add exception catching
            StringBuilder rpcReq = new StringBuilder();
            rpcReq.Append("{ \"jsonrpc\":\"2.0\",\"method\":\"");
            rpcReq.Append(methodeName);
            rpcReq.Append("\",\"params\":[");
            string str;
            for (int n = 0; n < parameters.Length; n++)
            {
                if (n > 0) // not on the 1st param -> add coma
                    rpcReq.Append(" , ");
                if (parameters[n] is IBlockchainClient) // object is for API call -> serializing it in json
                {
                    rpcReq.Append(JsonConvert.SerializeObject(parameters[n], new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
                }
                else if (parameters[n] is bool)
                {
                    rpcReq.Append(parameters[n].ToString().ToLower());
                }
                else
                {
                    str = parameters[n] as string;
                    if (str != null) // parameter is a string -> adding trailing double quote
                        rpcReq.AppendFormat("\"{0}\"", str.Replace("\"", "\\\""));
                    else // parameter is not a string  -> adding tostring 
                        rpcReq.Append(parameters[n].ToString());
                }
            }
            rpcReq.Append("],\"id\": ");
            rpcReq.Append("1");
            rpcReq.Append("}");
            return rpcReq.ToString();
        }
    }
        
}
