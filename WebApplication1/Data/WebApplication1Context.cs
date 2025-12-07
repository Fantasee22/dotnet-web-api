
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context(DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }
    }
}