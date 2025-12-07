using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class TrInvoiceDetail
    {
        public string InvoiceNo { get; set; }
        public int ProductID { get; set; }
        public float Weight { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
