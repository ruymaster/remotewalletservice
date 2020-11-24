using System;
using System.Collections.Generic;
using System.Text;

namespace Remote_Interactive_Wallet_Base
{
    /// <summary>
    /// Used for monitoring Addresses, Transactions
    /// </summary>
    public interface MonitoredAsset
    {
        string IDHash { get; set; }

        string CallBackURL { get; set; }
    }
}
