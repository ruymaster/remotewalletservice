using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoWallet.Model
{
    public class PaymentAddress
    {
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public Nullable<int> Account_id { get; set; }
        public string Address { get; set; }
        public int Currency_id { get; set; }

        public PaymentAddress()
        {

        }
        public PaymentAddress(int Id_, int Account_id_, string Address_, int Currency_id_)
        {
            this.Id = Id_;
            this.Account_id = Account_id_;
            this.Address = Address_;
            this.Currency_id = Currency_id_;
        }
    }
}
