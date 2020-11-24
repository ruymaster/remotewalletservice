using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
//using CryptoWallet.Model.

namespace CryptoWallet.BlockchainClients
{
    public class BaseBlockchainClient
    {
        #region Protected Override Properties
        protected  string serverUrl = "https://blockchain.info";
        #endregion 

        #region Constructor
        public BaseBlockchainClient(string serverUrl, CryptoWallet.Model.Currency currency) : base ()
        {
            this.serverUrl = serverUrl;
        }

        //static async Task LoadBalance()
        //{

        //}
        
        #endregion
    }
}