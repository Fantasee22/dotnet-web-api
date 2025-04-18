namespace WebApplication1.DTO
{
    public class ResEmployee
    {
        public ResEmployee()
        {

        }
        public long Id { get; set; } = 0;
        public string Name { get; set; } = String.Empty;
        public string Code { get; set; } = String.Empty;
        public bool IsContract { get; set; } = false;
        public DateTime? StartDt { get; set; } = null;
        public DateTime? EndDt { get; set; } = null;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public long? EmployeePositionId { get; set; } = null;
        public long? OfficeBranchId { get; set; } = null;
        public long? UploadFileId { get; set; } = null;
    }
}
