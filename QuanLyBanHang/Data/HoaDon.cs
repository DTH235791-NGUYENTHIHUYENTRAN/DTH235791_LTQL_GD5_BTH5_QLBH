using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Data
{
    public class HoaDon
    {
        public int ID { get; set; }
        public int NhanVienID { get; set; }
        public int KhachHangID { get;set; }
        public DateTime NgayLap {  get; set; }
        public String? GhiChuHoaDon { get; set; }

        public virtual ObservableCollectionListSource<HoaDon_ChiTiet> HoaDon_ChiTiet { get; } = new();

        public virtual NhanVien NhanVien { get; set; } = null!;
        public virtual KhachHang KhachHang { get; set; } = null!;

    }
}
[NotMapped]
public class DanhSachHoaDon
{
    public int ID { get; set; }
    public int NhanVienID { get; set; }
    public string HoVaTenNhanVien { get; set; } // Thêm 
    public int KhachHangID { get; set; }
    public string HoVaTenKhachHang { get; set; } // Thêm 
    public DateTime NgayLap { get; set; }
    public string? GhiChuHoaDon { get; set; }
    public string? XemChiTiet { get; set; }  // Thêm 
    public decimal TongTienHoaDon { get; set; } // Thêm 
} 



