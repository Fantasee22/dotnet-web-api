using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    [Table("EMPLOYEE")]
    [Index(nameof(Code), IsUnique = true)]
    public class Employee
    {
        public Employee()
        {

        }

        [Key]
        [Column("ID")]
        public long Id { get; set; } = 0;

        [Column("NAME")]
        [StringLength(300)]
        public string Name { get; set; } = String.Empty;

        [Column("CODE")]
        [StringLength(100)]
        public string Code { get; set; } = String.Empty;

        [Column("IS_CONTRACT")]
        public bool IsContract { get; set; } = false;

        [Column("START_DT")]
        public DateTime? StartDt { get; set; } = null;

        [Column("END_DT")]
        public DateTime? EndDt { get; set; } = null;

        [Column("ADDRESS")]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [Column("PHONE")]
        [StringLength(50)]
        public string Phone { get; set; } = string.Empty;

        [Column("EMPLOYEE_POSITION_ID")]
        public long? EmployeePositionId { get; set; } = null;

        [ForeignKey("EmployeePositionId")]
        [InverseProperty("Employees")]
        public virtual EmployeePosition EmployeePosition { get; set; }

        [Column("OFFICE_BRANCH_ID")]
        public long? OfficeBranchId { get; set; } = null;

        [ForeignKey("OfficeBranchId")]
        [InverseProperty("Employees")]
        public virtual OfficeBranch OfficeBranch { get; set; }

        [Column("UPLOAD_FILE_ID")]
        public long? UploadFileId { get; set; } = null;

        [ForeignKey("UploadFileId")]
        [InverseProperty("Employees")]
        public virtual UploadFile? UploadFile { get; set; }
    }
}
