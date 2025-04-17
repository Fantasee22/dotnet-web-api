using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    [Table("EMPLOYEE_POSITION")]
    public class EmployeePosition
    {
        public EmployeePosition()
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

        [InverseProperty("EmployeePosition")]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
