using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoWallet.Model
{
    public class Currency
    {
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public string Currency_key { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public string Ccy_type { get; set; }
        public byte Precision { get; set; }
        public string Base_factor { get; set; }
        public int Max_confirm { get; set; }
        public string Blockchain_client { get; set; }
        public string Rpc { get; set; }
        public string Tx_template { get; set; }
        public byte Visible { get; set; }
        public long Checked_blocks { get; set; }
        public string Contract_address { get; set; }

        public Currency()
        {

        }
        public Currency(int Id_, string Currency_key_, string Code_, string Symbol_, string Ccy_type_, byte Precision_, string Base_factor_, int Max_confirm_, string Blockchain_client_, string Rpc_, string Tx_template_, byte Visible_, long Checked_blocks_, string Contract_address_)
        {
            this.Id = Id_;
            this.Currency_key = Currency_key_;
            this.Code = Code_;
            this.Symbol = Symbol_;
            this.Ccy_type = Ccy_type_;
            this.Precision = Precision_;
            this.Base_factor = Base_factor_;
            this.Max_confirm = Max_confirm_;
            this.Blockchain_client = Blockchain_client_;
            this.Rpc = Rpc_;
            this.Tx_template = Tx_template_;
            this.Visible = Visible_;
            this.Checked_blocks = Checked_blocks_;
            this.Contract_address = Contract_address_;
        }
    }
}
