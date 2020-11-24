using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Remote_Interactive_Wallet_Base
{
    public interface WalletEndpoints
    {
        string ServerURL { get; set; }

        IEnumerable<Deposit> GetAllDepositsForAddress(string publicAddress, int vaultIndex);
        IEnumerable<Deposit> GetRecentDeposits(int pageIndex, int vaultIndex);
        IEnumerable<string> GetPublicAddresses(int vaultIndex);
        long GetBlockchainHeight();
        bool UpdateWatchedAddresses(string addr, int vaultIndex);
        bool UpdateWatchedTXs();
        decimal GetFeeForTX(string tx, int vaultIndex, DateTime when);
        long GetTXConfirmationsCount(string txHash, int vaultIndex);

        decimal GetAvailableBalance(int vaultIndex, int index);

        IEnumerable<OutgoingTransferOrder> ExecuteTransfersPackage(IEnumerable<OutgoingTransferOrder> orders, int moveAll, int vaultIndex);
    }
}
