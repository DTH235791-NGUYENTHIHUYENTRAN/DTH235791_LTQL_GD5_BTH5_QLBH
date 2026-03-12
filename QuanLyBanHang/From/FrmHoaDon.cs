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
            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value);

            listChiTiet = context.HoaDon_ChiTiet
                .Include(x => x.SanPham)
                .Where(x => x.HoaDonID == id)
                .ToList();

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDoc;
            printDoc.PrintPage += InHoaDon_PrintPage;
            preview.ShowDialog();
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
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!");
                return;
            }

            var excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Add(Type.Missing);
            Worksheet ws = (Worksheet)wb.ActiveSheet;

            ws.Name = "DanhSachHoaDon";

            // ===== HEADER =====
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                ws.Cells[1, i + 1] = dataGridView.Columns[i].HeaderText;
                ws.Cells[1, i + 1].Font.Bold = true;
            }

            // ===== DATA =====
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (!dataGridView.Rows[i].IsNewRow)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        ws.Cells[i + 2, j + 1] =
                            dataGridView.Rows[i].Cells[j].Value?.ToString();
                    }
                }
            }

            ws.Columns.AutoFit();

            excel.Visible = true;

            MessageBox.Show("Xuất danh sách hóa đơn thành công!");
        }
    }
}


    

