using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoWallet.Model
{
    public class PaymentTransaction
    {
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public string Txid { get; set; }
        public decimal Amount { get; set; }
        public string Address { get; set; }
        public int Confirmations { get; set; }
        public string Tx_state { get; set; }
        public int Currency_id { get; set; }
        public DateTime Received_at { get; set; }

        public PaymentTransaction()
        {

        }
        public PaymentTransaction(int Id_, string Txid_, decimal Amount_, string Address_, int Confirmations_, string Tx_state_, int Currency_id_, DateTime Received_at_)
        {
            this.Id = Id_;
            this.Txid = Txid_;
            this.Amount = Amount_;
            this.Address = Address_;
            this.Confirmations = Confirmations_;
            this.Tx_state = Tx_state_;
            this.Currency_id = Currency_id_;
            this.Received_at = Received_at_;
        }
    }
    public enum TransactionState
    {
        CONFIRMED,
        PENDING,
        COMPLETED,
        REJECTED
    }
}
