using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Data
{
    public class HoaDon_ChiTiet
    {
        public int ID { get; set; }
        public int HoaDonID { get; set; }
        public int SanPhamID { get; set; }
        public int SoLuongBan { get; set; }
        public int DonGiaBan { get; set; }

        // Dùng để lấy tên từ object SanPham
        public string TenSanPham => SanPham?.TenSanPham;

        // Tính thành tiền tự động
        public decimal ThanhTien => SoLuongBan * DonGiaBan;

        public virtual HoaDon HoaDon { get; set; } = null!;
        public virtual SanPham SanPham { get; set; } = null!;
    }
    public class DanhSachHoaDon_ChiTiet
    {
        public int ID { get; set; }
        public int HoaDonID { get; set; }
        public int SanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuongBan { get; set; }
        public decimal DonGiaBan { get; set; }
        public decimal ThanhTien { get; set; }
    }
}

