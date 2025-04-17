using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    [Table("UPLOAD_FILE")]
    public class UploadFile
    {
        public UploadFile()
        {

        }

        [Key]
        [Column("ID")]
        public long Id { get; set; } = 0;

        [Column("NAME")]
        [StringLength(300)]
        public string Name { get; set; } = String.Empty;

        [Column("STATUS")]
        [StringLength(50)]
        public string Status { get; set; } = String.Empty;

        [Column("BASE64_DATA")]
        [StringLength(3000)]
        public string Base64Data { get; set; } = String.Empty;

        [InverseProperty("UploadFile")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
