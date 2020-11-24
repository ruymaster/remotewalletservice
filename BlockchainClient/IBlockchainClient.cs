using System.Collections.Generic;
using System.Threading.Tasks;
//using CryptoWallet.Model;

namespace CryptoWallet.BlockchainClients
{    
    public interface IBlockchainClient : IAPIClient
    {
        Task<decimal> LoadBalance(string address, string currency);
        //Task<IEnumerable<decimal>> GetBlockHeigh(IEnumerable<decimal> height);
        Task<Deposit> LoadDeposit(string txid);

        Task<CheckedDeposits> GetNextDeposit(List<string> _addresses, List<string> _contractAddress=null, int _limitBlocks =0  );

        //Task<TransactionsAtAddress> GetTransactionsAtAddress(string address);
    }

    public struct CheckedDeposits
    {
        public List<Deposit> deposits;
        public long lastCheckedBlock;
        public long latestBlockNumber;

    }
    public struct Deposit
    {
        public string txid;
        public string amount;
        public string toAddress;
        public long  confirm;
        public long blockNumber;
        public string contractAddress;
    }
}