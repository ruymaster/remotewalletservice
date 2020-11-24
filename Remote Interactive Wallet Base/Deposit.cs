using System;
using System.Collections.Generic;
using System.Text;

namespace Remote_Interactive_Wallet_Base
{
    public class Deposit
    {
        public decimal Amount { get; set; }
        public decimal FeeAmount { get; set; }

        public DateTime? MomentTXMined { get; set; }

        public long BlockHeight { get; set; }

        public string DestinationAddress { get; set; }

        public string TXHash { get; set; }

        public string InputSources { get; set; }

        /// <summary>
        /// Relevant mostly on FIAT currencies, for DepositKey
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Relevant only on FIAT currencies
        /// </summary>
        public string OriginalName { get; set; }
    }
}
