using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Data
{
    public class SanPham
    {
        public int ID { get; set; }
        public int HangSanXuatID { get; set; }
        public int LoaiSanPhamID { get; set; }
        public string TenSanPham {  get; set; }
        public int DonGia {  get; set; }
        public int SoLuong {  get; set; }
        public string? HinhAnh { get; set; }
        public String? MoTa {  get; set; }
        



        public virtual ObservableCollectionListSource<HoaDon_ChiTiet> HoaDon_ChiTiet { get; } = new();
        public virtual LoaiSanPham LoaiSanPham { get; set; } = null!;
        public virtual HangSanXuat HangSanXuat { get; set; } = null!;
    }
}
public class DanhSachSanPham
{
    public int ID { get; set; }
    public int HangSanXuatID { get; set; }
    public string TenHangSanXuat { get; set; } // Thêm
    public int LoaiSanPhamID { get; set; }
    public string TenLoai { get; set; } // Thêm
    public string TenSanPham { get; set; }
    public int DonGia { get; set; }
    public int SoLuong { get; set; }
    public Image HinhAnh { get; set; }
    public string? MoTa { get; set; }
}
