using System;
using System.Collections.Generic;
using System.Text;

namespace Remote_Interactive_Wallet_Base
{

    public abstract class WalletResponseData
    {
        public int StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }

        public DateTime DateTimeResponseArrived { get; set; }
    }

    public class WalletResponseDataWrapper<T> : WalletResponseData
    {
        public T WrappedData { get; set; }

        public WalletResponseDataWrapper() { }
        public WalletResponseDataWrapper(T data)
        {
            WrappedData = data;
        }
    }
}
