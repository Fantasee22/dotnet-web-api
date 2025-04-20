using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DTO
{
    [Keyless]
    public class EmployeeDetail
    {
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public string ContractStartDt { get; set; }
        public string ContractEndDt { get; set; }
    }
}
