using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Data
{
    public class NhanVien
    {
        public int ID { get; set; }
        public string HoVaTen { get; set; }
        public String? DienThoai { get; set; }
        public String? DiaChi { get; set; }
        public String TenDangNhap { get; set; }
        public String MatKhau { get; set; }
        public bool QuyenHan {  get; set; }

        public virtual ObservableCollectionListSource<HoaDon> HoaDon { get; } = new();

    }
}
