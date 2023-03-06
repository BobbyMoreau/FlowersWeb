using flowers.web.Models;
using Microsoft.EntityFrameworkCore;

namespace flowers.web.Data
{
    public class FlowersContext : DbContext
    {
        public DbSet<FlowerModel> Flowers { get; set; }
        //public DbSet<FamilyModel> Families { get; set; }
        public FlowersContext(DbContextOptions options) : base(options) { }
    }
}