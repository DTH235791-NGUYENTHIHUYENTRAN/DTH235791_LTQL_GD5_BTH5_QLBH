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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất dữ liệu ra tập tin Excel";
            saveFileDialog.Filter = "Tập tin Excel|*.xls;*.xlsx";
            saveFileDialog.FileName = "HoaDon_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Data.DataTable table = new System.Data.DataTable();

                    table.Columns.AddRange(new DataColumn[5] {
                new DataColumn("ID", typeof(int)),
                new DataColumn("NhanVienID", typeof(int)),
                new DataColumn("KhachHangID", typeof(int)),
                new DataColumn("NgayLap", typeof(DateTime)),
                new DataColumn("GhiChuHoaDon", typeof(string))
            });

                    var hoaDon = context.HoaDon.ToList();

                    if (hoaDon != null)
                    {
                        foreach (var hd in hoaDon)
                        {
                            table.Rows.Add(
                                hd.ID,
                                hd.NhanVienID,
                                hd.KhachHangID,
                                hd.NgayLap,
                                hd.GhiChuHoaDon
                            );
                        }
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "HoaDon");
                        sheet.Columns().AdjustToContents();

                        wb.SaveAs(saveFileDialog.FileName);

                        MessageBox.Show("Đã xuất dữ liệu ra Excel thành công.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Nhập dữ liệu từ tập tin Excel";
            openFileDialog.Filter = "Tập tin Excel|*.xls;*.xlsx";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Data.DataTable table = new System.Data.DataTable();

                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        IXLWorksheet worksheet = workbook.Worksheet(1);

                        bool firstRow = true;
                        string readRange = "1:1";

                        foreach (IXLRow row in worksheet.RowsUsed())
                        {
                            if (firstRow)
                            {
                                readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);

                                foreach (IXLCell cell in row.Cells(readRange))
                                    table.Columns.Add(cell.Value.ToString());

                                firstRow = false;
                            }
                            else
                            {
                                table.Rows.Add();
                                int cellIndex = 0;

                                foreach (IXLCell cell in row.Cells(readRange))
                                {
                                    table.Rows[table.Rows.Count - 1][cellIndex] = cell.Value.ToString();
                                    cellIndex++;
                                }
                            }
                        }

                        if (table.Rows.Count > 0)
                        {
                            foreach (DataRow r in table.Rows)
                            {
                                HoaDon hd = new HoaDon();

                                hd.NhanVienID = Convert.ToInt32(r["NhanVienID"]);
                                hd.KhachHangID = Convert.ToInt32(r["KhachHangID"]);
                                hd.NgayLap = Convert.ToDateTime(r["NgayLap"]);
                                hd.GhiChuHoaDon = r["GhiChuHoaDon"].ToString();

                                context.HoaDon.Add(hd);
                            }

                            context.SaveChanges();

                            MessageBox.Show("Đã nhập thành công " + table.Rows.Count + " dòng.");
                            FrmHoaDon_Load(sender, e);
                        }

                        if (firstRow)
                            MessageBox.Show("Tập tin Excel rỗng.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}


    

