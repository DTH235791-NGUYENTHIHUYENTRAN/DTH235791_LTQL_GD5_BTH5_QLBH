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
using System.Drawing.Printing;
using ClosedXML.Excel;

namespace QuanLyBanHang.From
{
    public partial class FrmHoaDon_ChiTiet : Form
    {
        BindingList<HoaDon_ChiTiet> listChiTiet = new BindingList<HoaDon_ChiTiet>();
        private PrintDocument InHoaDon = new PrintDocument();
        int id = 0; // ID hóa đơn hiện tại
        QLBHDbContext context = new QLBHDbContext();
        public FrmHoaDon_ChiTiet(int maHoaDon = 0)
        {
            InitializeComponent();
            id = maHoaDon;
            InHoaDon.PrintPage += InHoaDon_PrintPage;
        }

        void LoadCombobox()
        {
            cboNhanVien.DataSource = context.NhanVien.ToList();
            cboNhanVien.DisplayMember = "HoVaTen";
            cboNhanVien.ValueMember = "ID";

            cboKhachHang.DataSource = context.KhachHang.ToList();
            cboKhachHang.DisplayMember = "HoVaTen";
            cboKhachHang.ValueMember = "ID";
            var danhSachSanPham = context.SanPham.ToList();

            // Gán vào ComboBox
            cboSanPham.DataSource = danhSachSanPham;
            cboSanPham.DisplayMember = "TenSanPham"; // Tên cột bạn muốn hiển thị
            cboSanPham.ValueMember = "ID";
        }

        void LoadSanPhamVaoGrid()
        {
            var sp = context.SanPham
                .Select(x => new
                {
                    x.ID,
                    x.TenSanPham,
                    SoLuongBan = 0,
                    DonGiaBan = x.DonGia
                })
                .ToList();

            dataGridView.DataSource = sp;
        }


        private void btnXacNhanBan_Click(object sender, EventArgs e)
        {
            int spId = (int)cboSanPham.SelectedValue;
            var sp = context.SanPham.Find(spId); // Lấy thông tin sản phẩm từ DB

            HoaDon_ChiTiet ct = new HoaDon_ChiTiet
            {
                SanPhamID = spId,
                SanPham = sp, // <--- BẮT BUỘC CÓ DÒNG NÀY ĐỂ HIỂN THỊ TÊN
                DonGiaBan = (int)numDonGiaBan.Value,
                SoLuongBan = (int)numSoLuongBan.Value
            };

            listChiTiet.Add(ct);
            dataGridView.Refresh();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow?.DataBoundItem is HoaDon_ChiTiet ct)
            {
                listChiTiet.Remove(ct);
            }
        }

        private void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                HoaDon hd = (id == 0) ? new HoaDon() : context.HoaDon.Find(id);

                if (id == 0) context.HoaDon.Add(hd);
                else context.HoaDon_ChiTiet.RemoveRange(context.HoaDon_ChiTiet.Where(x => x.HoaDonID == id));

                hd.NhanVienID = (int)cboNhanVien.SelectedValue;
                hd.KhachHangID = (int)cboKhachHang.SelectedValue;
                hd.NgayLap = DateTime.Now;
                hd.GhiChuHoaDon = txtGhiChuHoaDon.Text;

                foreach (var item in listChiTiet)
                {
                    item.HoaDonID = hd.ID;
                    context.HoaDon_ChiTiet.Add(item);
                }

                context.SaveChanges();
                MessageBox.Show("Lưu thành công!");
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
        private void FrmHoaDon_ChiTiet_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = true;
            dataGridView.DataSource = listChiTiet; 

            LoadCombobox();

            if (id != 0) // Chế độ sửa
            {
                var hd = context.HoaDon.Find(id);
                if (hd != null)
                {
                    cboNhanVien.SelectedValue = hd.NhanVienID;
                    cboKhachHang.SelectedValue = hd.KhachHangID;
                    txtGhiChuHoaDon.Text = hd.GhiChuHoaDon;

                    var items = context.HoaDon_ChiTiet.Where(x => x.HoaDonID == id).ToList();
                    foreach (var item in items) listChiTiet.Add(item);
                }
            }
        }
        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue != null && int.TryParse(cboSanPham.SelectedValue.ToString(), out int spId))
            {
                var sp = context.SanPham.Find(spId);
                if (sp != null)
                {
                    // Tự động điền đơn giá từ cơ sở dữ liệu
                    numDonGiaBan.Value = (decimal)sp.DonGia;

                    // Tự động đặt số lượng mặc định là 1 để người dùng không quên
                    numSoLuongBan.Value = 1;
                }
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (listChiTiet.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm trong hóa đơn để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị xem trước khi in
            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = InHoaDon;
            preview.ShowDialog();
        }
        private void InHoaDon_PrintPage(object sender, PrintPageEventArgs e)
        {
            float y = 50; // Tọa độ dòng hiện tại
            float left = 50; // Lề trái

            // 1. Tiêu đề Hóa đơn
            e.Graphics.DrawString("HÓA ĐƠN BÁN HÀNG", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, 250, y);
            y += 60;

            // 2. Vẽ Tiêu đề cột
            e.Graphics.DrawString("Tên Sản Phẩm", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, left, y);
            e.Graphics.DrawString("SL", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 350, y);
            e.Graphics.DrawString("Đơn giá", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 450, y);
            e.Graphics.DrawString("Thành tiền", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 600, y);

            y += 30;
            e.Graphics.DrawLine(Pens.Black, left, y, 750, y); // Đường kẻ ngang
            y += 10;

            // 3. Duyệt danh sách sản phẩm
            decimal tongTien = 0;
            foreach (var item in listChiTiet)
            {
                decimal thanhTien = item.SoLuongBan * item.DonGiaBan;

                e.Graphics.DrawString(item.SanPham?.TenSanPham, new Font("Arial", 12), Brushes.Black, left, y);
                e.Graphics.DrawString(item.SoLuongBan.ToString(), new Font("Arial", 12), Brushes.Black, 350, y);
                e.Graphics.DrawString(item.DonGiaBan.ToString("N0"), new Font("Arial", 12), Brushes.Black, 450, y);
                e.Graphics.DrawString(thanhTien.ToString("N0"), new Font("Arial", 12), Brushes.Black, 600, y);

                y += 30;
                tongTien += thanhTien;
            }

            // 4. Tổng tiền
            y += 10;
            e.Graphics.DrawLine(Pens.Black, left, y, 750, y);
            y += 10;
            e.Graphics.DrawString($"TỔNG CỘNG: {tongTien:N0} VNĐ", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 450, y);
        }




        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && dataGridView.Columns[e.ColumnIndex].Name == "XemChiTiet")
            {
                int id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);

                FrmHoaDon_ChiTiet frm = new FrmHoaDon_ChiTiet(id);
                frm.ShowDialog();


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
                            foreach (System.Data.DataRow r in table.Rows)
                            {
                                HoaDon_ChiTiet ct = new HoaDon_ChiTiet();

                                ct.HoaDonID = Convert.ToInt32(r["HoaDonID"]);
                                ct.SanPhamID = Convert.ToInt32(r["SanPhamID"]);
                                ct.SoLuongBan = Convert.ToInt32(r["SoLuongBan"]);
                                ct.DonGiaBan = Convert.ToInt32(r["DonGiaBan"]);

                                context.HoaDon_ChiTiet.Add(ct);
                            }

                            context.SaveChanges();

                            MessageBox.Show("Đã nhập thành công " + table.Rows.Count + " dòng.");
                            FrmHoaDon_ChiTiet_Load(sender, e);
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

        private void btnXuat_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất dữ liệu ra tập tin Excel";
            saveFileDialog.Filter = "Tập tin Excel|*.xls;*.xlsx";
            saveFileDialog.FileName = "HoaDonChiTiet_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Data.DataTable table = new System.Data.DataTable();

                    table.Columns.AddRange(new System.Data.DataColumn[5] {
                new System.Data.DataColumn("ID", typeof(int)),
                new System.Data.DataColumn("HoaDonID", typeof(int)),
                new System.Data.DataColumn("SanPhamID", typeof(int)),
                new System.Data.DataColumn("SoLuongBan", typeof(int)),
                new System.Data.DataColumn("DonGiaBan", typeof(int))
            });

                    var chiTiet = context.HoaDon_ChiTiet.ToList();

                    if (chiTiet != null)
                    {
                        foreach (var ct in chiTiet)
                        {
                            table.Rows.Add(
                                ct.ID,
                                ct.HoaDonID,
                                ct.SanPhamID,
                                ct.SoLuongBan,
                                ct.DonGiaBan
                            );
                        }
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "HoaDonChiTiet");
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
    }

}

