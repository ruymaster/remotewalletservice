using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using System;

namespace CryptoWallet.BlockchainClients
{
    public class ErlondProxy
    {
        string serverUrl = "https://staging-gateway.elrond.com/";
        static HttpClient httpClient;

                
        public async Task<string> GetSSLRPC(string endPoint, string rpcUrl="")
        {
            try
            {
                string rpcStr = rpcUrl == "" ? serverUrl + endPoint : rpcUrl + endPoint;
                
                if (httpClient == null) httpClient = new HttpClient();
                var response = await httpClient.GetAsync(rpcStr);                
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
                
            }
            catch (HttpRequestException e)
            {
                return e.Message;
            }
        }

        public string GetRPC(string endPoint, string rpcUrl = "")
        {
            string rpcStr = rpcUrl == "" ? serverUrl + endPoint : rpcUrl + endPoint;
            var myClient = new WebClient();
            Stream response = myClient.OpenRead(rpcStr);
            StreamReader reader = new StreamReader(response);
            string w3resp = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return w3resp;
        }
        public async Task<long>  GetLastBlockNumber()
        {
            string response;
            response = await GetSSLRPC("network/status/4294967295");
            var rpccallresult = JsonConvert.DeserializeObject<JsonRpcResult<NetworkData>>(response);
            return rpccallresult.Data.Status.ErdHighestFinalNonce;

        }
        public async Task<ErlondHyperBlock> GetBlockByNumberAsync(long blockNumber)
        {
            string response;
            response = await GetSSLRPC("hyperblock/by-nonce/"+blockNumber);
            var rpccallresult = JsonConvert.DeserializeObject<JsonRpcResult<ErlondBlock>>(response);
            return rpccallresult.Data?.HyperBlock;
        }
        public async Task<ErlondHyperBlock> GetTransactionAsync(long blockNumber)
        {
            string response;
            response = await GetSSLRPC("hyperblock/by-nonce/" + blockNumber);
            var rpccallresult = JsonConvert.DeserializeObject<JsonRpcResult<ErlondBlock>>(response);
            return rpccallresult.Data?.HyperBlock;
        }
    }

    public class JsonRpcResult<ResultT>
    {
        [JsonProperty("data")]
        public ResultT Data { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
        
    class JsonRpcResult : JsonRpcResult<string>
    { }

    public class NetworkData
    {
        [JsonProperty("status")]
        public NetworkStatus Status { get; set; }
    }
    public class NetworkStatus
    {
        [JsonProperty("erd_highest_final_nonce")]
        public long ErdHighestFinalNonce { get; set; }
    }
    
    public class ErlondBlock
    {
        [JsonProperty("hyperblock")]
        public ErlondHyperBlock HyperBlock { get; set; }
    }
    public class ErlondHyperBlock
    {
        [JsonProperty("transactions")]
        public ErlondTransaction[] Transactions;
    }
    public class ErlondTransaction
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("sender")]
        public string Sender { get; set; }
        [JsonProperty("receiver")]
        public string Receiver { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
