using System;
using System.Collections.Generic;
using System.Text;

namespace Remote_Interactive_Wallet_Base
{
    public interface CentralEndpoints
    {
        void NotificationNewTX();
        void NotificationNewBlock();
        void NotificationTXMined();
    }
}
