using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class CourierFeeRequest
    {
        public int CourierID { get; set; }
        public float TotalWeight { get; set; }
    }
}
