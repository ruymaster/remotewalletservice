using System;
using System.ComponentModel.DataAnnotations;

namespace Remote_Interactive_Wallet_Base
{
    public class OutgoingTransferOrder
    {
        public int ID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string DestinationAddress { get; set; }

        public string DestinationAddressExt { get; set; }

        public string DestinationName { get; set; }

        [Required]
        public string CurrencySymbol { get; set; }

        [Required]
        public string Comment1 { get; set; }

        public string Comment2 { get; set; }

        public bool Executed { get; set; }

        public string ResultingTXHash { get; set; }

        public DateTime? ResultingTXMoment { get; set; }

        public string SourceAddress { get; set; }

        public string SourceAddressExt { get; set; }

        public long PublishedAtBlockHeight { get; set; }

        public decimal ResultingTXFee { get; set; }

        public int? ExternalOperatorAddressID { get; set; }
    }
}
