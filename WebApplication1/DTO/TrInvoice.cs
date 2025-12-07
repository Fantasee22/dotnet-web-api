using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class TrInvoice
    {
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceTo { get; set; }
        public string ShipTo { get; set; }
        public int SalesID { get; set; }
        public int CourierID { get; set; }
        public int PaymentType { get; set; }
        public decimal CourierFee { get; set; }
        public List<TrInvoiceDetail> details { get; set; } = new List<TrInvoiceDetail>();
    }
}
