using API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public Difficulty Difficulties { get; set; }

        public Region Regions { get; set; }

        public Walk Walks { get; set; }
    }
}
