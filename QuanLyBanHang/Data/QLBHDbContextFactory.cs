
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;

namespace QuanLyBanHang.Data
{
    public class QLBHDbContextFactory
        : IDesignTimeDbContextFactory<QLBHDbContext>
    {
        public QLBHDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<QLBHDbContext>();

            var conn = ConfigurationManager
                .ConnectionStrings["QLBHDbContext"]
                .ConnectionString;

            optionsBuilder.UseSqlServer(conn);

            return new QLBHDbContext(optionsBuilder.Options);
        }
    }
}