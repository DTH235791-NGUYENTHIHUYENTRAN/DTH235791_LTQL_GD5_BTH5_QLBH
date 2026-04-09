using QuanLyBanHang.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using ClosedXML.Excel;
using QuanLyBanHang.Reports;
namespace QuanLyBanHang.From
  

{
    public partial class FrmHoaDon : Form
    {
        private PrintDocument printDoc = new PrintDocument();

        QLBHDbContext context = new QLBHDbContext();
        int id;

        List<HoaDon_ChiTiet> listChiTiet = new List<HoaDon_ChiTiet>();
        public FrmHoaDon()
        {
            InitializeComponent();
        }


        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;

            var hd = context.HoaDon
                .Include(r => r.NhanVien)
                .Include(r => r.KhachHang)
                .Include(r => r.HoaDon_ChiTiet)
                .Select(r => new DanhSachHoaDon
                {
                    ID = r.ID,
                    NhanVienID = r.NhanVienID,
                    HoVaTenNhanVien = r.NhanVien != null ? r.NhanVien.HoVaTen : "",
                    KhachHangID = r.KhachHangID,
                    HoVaTenKhachHang = r.KhachHang != null ? r.KhachHang.HoVaTen : "",
                    NgayLap = r.NgayLap,
                    GhiChuHoaDon = r.GhiChuHoaDon,
                    TongTienHoaDon = r.HoaDon_ChiTiet
                        .Sum(ct => (decimal?)ct.SoLuongBan * ct.DonGiaBan) ?? 0,
                    XemChiTiet = "Xem chi tiết"
                })
                .ToList();

            dataGridView.DataSource = hd;
        }
        void LoadHoaDon()
        {
            var hd = context.HoaDon
        .Include(r => r.NhanVien)
        .Include(r => r.KhachHang)
        .Include(r => r.HoaDon_ChiTiet)
        .Select(r => new DanhSachHoaDon
        {
            ID = r.ID,
            NhanVienID = r.NhanVienID,
            HoVaTenNhanVien = r.NhanVien != null ? r.NhanVien.HoVaTen : "",
            KhachHangID = r.KhachHangID,
            HoVaTenKhachHang = r.KhachHang != null ? r.KhachHang.HoVaTen : "",
            NgayLap = r.NgayLap,
            GhiChuHoaDon = r.GhiChuHoaDon,
            TongTienHoaDon = r.HoaDon_ChiTiet
                .Sum(ct => (decimal?)ct.SoLuongBan * ct.DonGiaBan) ?? 0,
            XemChiTiet = "Xem chi tiết"
        })
        .ToList();

            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = hd;
        }
        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            using (FrmHoaDon_ChiTiet chiTiet = new FrmHoaDon_ChiTiet())
            {
                chiTiet.ShowDialog();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
            using (FrmHoaDon_ChiTiet chiTiet = new FrmHoaDon_ChiTiet(id))
            {
                chiTiet.ShowDialog();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;

            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value);

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa hóa đơn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var hoaDon = context.HoaDon.Find(id);
                if (hoaDon != null)
                {
                    context.HoaDon.Remove(hoaDon);
                    context.SaveChanges();
                    LoadHoaDon();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 &&
                dataGridView.Columns[e.ColumnIndex].Name == "XemChiTiet")
            {
                id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);

                using (FrmHoaDon_ChiTiet f = new FrmHoaDon_ChiTiet(id))
                {
                    f.ShowDialog();
                }
            }
        }



        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
            using (frmInHoaDon inHoaDon = new frmInHoaDon(id))
            {
                inHoaDon.ShowDialog();
            }
        }

        private void InHoaDon_PrintPage(object sender, PrintPageEventArgs e)
        {
            float y = 50;
            e.Graphics.DrawString("HÓA ĐƠN", new System.Drawing.Font("Arial", 20), Brushes.Black, 250, y);
            y += 50;

            foreach (var item in listChiTiet)
            {
                e.Graphics.DrawString($"{item.SanPham.TenSanPham}: {item.SoLuongBan} x {item.DonGiaBan}",
                    new System.Drawing.Font("Arial", 12), Brushes.Black, 50, y);
                y += 30;
            }
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = Microsoft.VisualBasic.Interaction.InputBox(
        "Nhập tên khách hàng cần tìm:",
        "Tìm kiếm hóa đơn",
        "");

            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!");
                return;
            }

            var hd = context.HoaDon
                .Include(r => r.NhanVien)
                .Include(r => r.KhachHang)
                .Include(r => r.HoaDon_ChiTiet)
                .Where(r => r.KhachHang != null &&
                            r.KhachHang.HoVaTen.Contains(tuKhoa))
                .Select(r => new DanhSachHoaDon
                {
                    ID = r.ID,
                    NhanVienID = r.NhanVienID,
                    HoVaTenNhanVien = r.NhanVien != null ? r.NhanVien.HoVaTen : "",
                    KhachHangID = r.KhachHangID,
                    HoVaTenKhachHang = r.KhachHang != null ? r.KhachHang.HoVaTen : "",
                    NgayLap = r.NgayLap,
                    GhiChuHoaDon = r.GhiChuHoaDon,
                    TongTienHoaDon = r.HoaDon_ChiTiet
                        .Sum(ct => (decimal?)ct.SoLuongBan * ct.DonGiaBan) ?? 0,
                    XemChiTiet = "Xem chi tiết"
                })
                .ToList();

            dataGridView.DataSource = hd;
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel|*.xlsx",
                FileName = "HoaDon_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("HoaDon");
                        // Lấy toàn bộ danh sách Hóa Đơn từ database
                        var data = context.HoaDon.ToList();

                        // Chèn trực tiếp vào Worksheet, bắt đầu từ ô A1
                        worksheet.Cell(1, 1).InsertTable(data);
                        worksheet.Columns().AdjustToContents(); // Tự căn chỉnh độ rộng cột

                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Xuất danh sách hóa đơn thành công!", "Thông báo");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất file: " + ex.Message);
                }
            }
            }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Chọn file Excel Hóa Đơn"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        var worksheet = workbook.Worksheet(1);
                        // Sử dụng Skip(1) để bỏ qua dòng tiêu đề của Excel
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

                        foreach (var row in rows)
                        {
                            HoaDon hd = new HoaDon();

                            // Lưu ý: Thứ tự cột phải khớp với file Excel bạn đã xuất ra
                            // Cột 1 thường là ID (bỏ qua nếu DB tự tăng)
                            // Cột 2: NhanVienID
                            hd.NhanVienID = int.Parse(row.Cell(2).Value.ToString());

                            // Cột 3: KhachHangID
                            hd.KhachHangID = int.Parse(row.Cell(3).Value.ToString());

                            // Cột 4: NgayLap
                            string ngay = row.Cell(4).Value.ToString();
                            hd.NgayLap = string.IsNullOrEmpty(ngay) ? DateTime.Now : DateTime.Parse(ngay);

                            // Cột 5: GhiChuHoaDon
                            hd.GhiChuHoaDon = row.Cell(5).Value.ToString();

                            context.HoaDon.Add(hd);
                        }

                        context.SaveChanges();
                        MessageBox.Show("Nhập dữ liệu hóa đơn thành công!", "Thông báo");

                        // Refresh lại DataGridView
                        FrmHoaDon_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi nhập dữ liệu: " + ex.Message);
                }
            }
        }
    }
}


    

