using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace CryptoWallet.Model
{
    [Serializable]
    public class CurrencyHolding
    {
        #region Public Properties
        public CurrencySymbol Symbol { get; set; }
        //public ObservableCollection<BlockChainAddressInformation> BlockChainAddresses { get; } = new ObservableCollection<BlockChainAddressInformation>();
        public bool IsToken { get; set; }

        
        #endregion

        #region Constructors
        /// <summary>
        /// Only for serialization
        /// </summary>
        public CurrencyHolding()
        {

        }

        public CurrencyHolding(CurrencySymbol symbol)
        {
            Symbol = symbol;
        }
               
        #endregion
    }
}
