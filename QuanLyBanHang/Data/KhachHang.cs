using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Data
{
    public class KhachHang
    {
        public int ID { get; set; }
        public string HoVaTen { get; set; }
        public String? DienThoai { get; set; }
        public String? DiaChi { get; set; }

        public virtual ObservableCollectionListSource<HoaDon> HoaDon { get; } = new();

    }
}
