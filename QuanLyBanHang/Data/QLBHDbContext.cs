
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace QuanLyBanHang.Data
{

    public class QLBHDbContext : DbContext
    {
        public QLBHDbContext(DbContextOptions<QLBHDbContext> options)
         : base(options)
        {
        }

        // Constructor cho design-time
        public QLBHDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conn = ConfigurationManager
                    .ConnectionStrings["QLBHDbContext"]?.ConnectionString;

                if (!string.IsNullOrEmpty(conn))
                {
                    optionsBuilder.UseSqlServer(conn);
                }
            }
        }

        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public DbSet<HangSanXuat> HangSanXuat { get; set; }
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<HoaDon_ChiTiet> HoaDon_ChiTiet { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }

    }
}