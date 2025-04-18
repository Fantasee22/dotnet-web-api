using WebApplication1.Model;

namespace WebApplication1.DTO
{
    public class ResListEmployee
    {
        public ResListEmployee()
        {

        }

        public List<ResEmployee> listEmployee { get; set; } = new List<ResEmployee>();
    }
}
