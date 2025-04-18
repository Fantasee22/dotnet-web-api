using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    [Table("OFFICE_BRANCH")]
    [Index(nameof(Code), IsUnique = true)]
    public class OfficeBranch
    {
        public OfficeBranch()
        {
            this.Employees = new HashSet<Employee>();
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

        [Column("ADDRESS")]
        [StringLength(500)]
        public string Address { get; set; } = String.Empty;

        [InverseProperty("OfficeBranch")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
