using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWallet.Model
{
    public class BlockchainClient
    {
        public int Id { get; set; }
        public string ChainName { get; set; }
        public int CheckedBlocks { get; set; }
        public Nullable<DateTime> Updated_at { get; set; }

        public BlockchainClient()
        {

        }
        public BlockchainClient(int Id_, string ChainName_, int CheckedBlocks_, DateTime Updated_at_)
        {
            this.Id = Id_;
            this.ChainName = ChainName_;
            this.CheckedBlocks = CheckedBlocks_;
            this.Updated_at = Updated_at_;
        }
    }
}
