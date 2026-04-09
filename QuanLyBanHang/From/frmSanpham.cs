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
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using ClosedXML.Excel;


namespace QuanLyBanHang.From
{
    public partial class Sản_phẩm : Form
    {
        QLBHDbContext context = new QLBHDbContext();
        bool xuLyThem = false;
        int id;
        string imagesFolder = Path.Combine(Application.StartupPath, "Images");
        public Sản_phẩm()
        {
            InitializeComponent();
        }


        private void LoadDataGridView()
        {
            var danhSach = context.SanPham
                .Include(x => x.LoaiSanPham)
                .Include(x => x.HangSanXuat)
                .Select(r => new DanhSachSanPham
                {
                    ID = r.ID,
                    LoaiSanPhamID = r.LoaiSanPhamID,
                    TenLoai = r.LoaiSanPham.TenLoai,
                    HangSanXuatID = r.HangSanXuatID,
                    TenHangSanXuat = r.HangSanXuat.TenHangSanXuat,
                    TenSanPham = r.TenSanPham,
                    MoTa = r.MoTa,
                    SoLuong = r.SoLuong,
                    DonGia = r.DonGia,
                    HinhAnh = r.HinhAnh 
                })
                .ToList();

            dataGridView.DataSource = danhSach;
            dataGridView.RowTemplate.Height = 80;
        }
        private void BatTatChucNang(bool giaTri)
        {

            btnLuu.Enabled = giaTri;
            btnHuyBo.Enabled = giaTri;
            cboHangSanXuat.Enabled = giaTri;
            cboLoaiSanPham.Enabled = giaTri;
            txtTenSanPham.Enabled = giaTri;
            numSoLuong.Enabled = giaTri;
            numDonGia.Enabled = giaTri;
            txtMoTa.Enabled = giaTri;
            picHinhAnh.Enabled = giaTri;

            btnThem.Enabled = !giaTri;
            btnDoiAnh.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnTimKiem.Enabled = !giaTri;
            btnNhap.Enabled = !giaTri;
            btnXuat.Enabled = !giaTri;
        }



        public void LayLoaiSanPhamVaoComboBox()
        {
            cboLoaiSanPham.DataSource = context.LoaiSanPhams.ToList();
            cboLoaiSanPham.ValueMember = "ID";
            cboLoaiSanPham.DisplayMember = "TenLoai";
        }

        public void LayHangSanXuatVaoComboBox()
        {
            cboHangSanXuat.DataSource = context.HangSanXuat.ToList();
            cboHangSanXuat.ValueMember = "ID";
            cboHangSanXuat.DisplayMember = "TenHangSanXuat";
        }


        private void Sản_phẩm_Load(object sender, EventArgs e)
        {

            if (!Directory.Exists(imagesFolder)) Directory.CreateDirectory(imagesFolder);

            BatTatChucNang(false);
            LayLoaiSanPhamVaoComboBox();
            LayHangSanXuatVaoComboBox();

            numDonGia.Maximum = 1000000000;
            numDonGia.ThousandsSeparator = true;
            dataGridView.AutoGenerateColumns = false;
         
            dataGridView.CellFormatting += dataGridView_CellFormatting;

            LoadDataGridView();

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);
            txtTenSanPham.Clear();
            txtMoTa.Clear();
            numSoLuong.Value = 0;
            numDonGia.Value = 0;
            picHinhAnh.Image = null;
            picHinhAnh.Tag = null;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;
            xuLyThem = false;
            BatTatChucNang(true);
            var row = dataGridView.CurrentRow.DataBoundItem as DanhSachSanPham;
            id = row.ID;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!");
                return;
            }

            SanPham sp;
            if (xuLyThem)
            {
                sp = new SanPham();
                context.SanPham.Add(sp);
            }
            else
            {
                sp = context.SanPham.Find(id);
            }

            if (sp != null)
            {
                sp.LoaiSanPhamID = (int)cboLoaiSanPham.SelectedValue;
                sp.HangSanXuatID = (int)cboHangSanXuat.SelectedValue;
                sp.TenSanPham = txtTenSanPham.Text;
                sp.MoTa = txtMoTa.Text;
                sp.SoLuong = (int)numSoLuong.Value;
                sp.DonGia = (int)numDonGia.Value;
                sp.HinhAnh = picHinhAnh.Tag?.ToString(); // Lưu tên file ảnh

                context.SaveChanges();
                MessageBox.Show("Lưu thành công!");
                BatTatChucNang(false);
                LoadDataGridView();
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;
            if (MessageBox.Show("Xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var row = dataGridView.CurrentRow.DataBoundItem as DanhSachSanPham;
                var sp = context.SanPham.Find(row.ID);
                if (sp != null)
                {
                    context.SanPham.Remove(sp);
                    context.SaveChanges();
                    LoadDataGridView();
                }
            }

            }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            Sản_phẩm_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            var query = context.SanPham.AsQueryable();

            // 1) Lọc theo Loại sản phẩm (ComboBox)
            if (cboLoaiSanPham.SelectedValue != null)
            {
                int maLoai = Convert.ToInt32(cboLoaiSanPham.SelectedValue);
                query = query.Where(sp => sp.LoaiSanPhamID == maLoai);
            }

            // 2) Lọc theo Hãng sản xuất (ComboBox)
            if (cboHangSanXuat.SelectedValue != null)
            {
                int maHang = Convert.ToInt32(cboHangSanXuat.SelectedValue);
                query = query.Where(sp => sp.HangSanXuatID == maHang);
            }

            // 3) Lọc theo Tên sản phẩm (TextBox)
            string tenSP = txtTenSanPham.Text.Trim();
            if (!string.IsNullOrEmpty(tenSP))
            {
                query = query.Where(sp => sp.TenSanPham.Contains(tenSP));
            }

            // 4) Lọc theo Số lượng (NumericUpDown)
            int soLuong = (int)numSoLuong.Value;
            if (soLuong > 0)
            {
                query = query.Where(sp => sp.SoLuong == soLuong);
            }

            // 5) Lọc theo Đơn giá (NumericUpDown)
            decimal donGia = numDonGia.Value;
            if (donGia > 0)
            {
                query = query.Where(sp => sp.DonGia == donGia);
            }

            var ketQua = query
    .Include(x => x.LoaiSanPham)
    .Include(x => x.HangSanXuat)
    .AsEnumerable()
    .Select(r => new
    {
        r.ID,
        TenLoai = r.LoaiSanPham.TenLoai,
        TenHangSanXuat = r.HangSanXuat.TenHangSanXuat,
        r.TenSanPham,
        r.SoLuong,
        r.DonGia,
        HinhAnh = r.HinhAnh
    })
            .ToList();


            dataGridView.DataSource = ketQua;
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel|*.xlsx", FileName = "DanhSachSanPham.xlsx" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Products");
                    var data = context.SanPham.ToList();
                    worksheet.Cell(1, 1).InsertTable(data);
                    workbook.SaveAs(sfd.FileName);
                    MessageBox.Show("Xuất Excel thành công!");
                
            }
            }
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

                        foreach (var row in rows)
                        {
                            SanPham sp = new SanPham();

                            // Cột 2: HangSanXuatID
                            sp.HangSanXuatID = int.Parse(row.Cell(2).Value.ToString());
                            // Cột 3: LoaiSanPhamID
                            sp.LoaiSanPhamID = int.Parse(row.Cell(3).Value.ToString());
                            // Cột 4: TenSanPham
                            sp.TenSanPham = row.Cell(4).Value.ToString();

                            // Cột 5: DonGia (Sửa lỗi gạch đỏ ở đây)
                            string giaTriGia = row.Cell(5).Value.ToString();
                            sp.DonGia = string.IsNullOrEmpty(giaTriGia) ? 0 : int.Parse(giaTriGia);

                            // Cột 6: SoLuong
                            sp.SoLuong = int.Parse(row.Cell(6).Value.ToString());
                            // Cột 7: HinhAnh
                            sp.HinhAnh = row.Cell(7).Value.ToString();
                            // Cột 8: MoTa
                            sp.MoTa = row.Cell(8).Value.ToString();

                            context.SanPham.Add(sp);
                        }
                        context.SaveChanges();
                        MessageBox.Show("Nhập dữ liệu thành công!", "Thông báo");
                        LoadDataGridView(); // Cập nhật lại bảng hiển thị
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnDoiAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png" };
            if (open.ShowDialog() == DialogResult.OK)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(open.FileName);
                string savePath = Path.Combine(imagesFolder, fileName);
                File.Copy(open.FileName, savePath, true);

                picHinhAnh.Image = LoadImage(fileName);
                picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
                picHinhAnh.Tag = fileName; // Gán tên file vào Tag để btnLuu lấy
            }
        }


        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridView.Rows[e.RowIndex].DataBoundItem as DanhSachSanPham;
            if (row != null)
            {
                txtTenSanPham.Text = row.TenSanPham;
                numSoLuong.Value = row.SoLuong;
                numDonGia.Value = row.DonGia;
                txtMoTa.Text = row.MoTa;
                cboLoaiSanPham.SelectedValue = row.LoaiSanPhamID;
                cboHangSanXuat.SelectedValue = row.HangSanXuatID;

                picHinhAnh.Image = LoadImage(row.HinhAnh);
                picHinhAnh.Tag = row.HinhAnh;
            }
        }

        private void btnXoayanh_Click(object sender, EventArgs e)
        {
            if (picHinhAnh.Image == null || picHinhAnh.Tag == null) return;

            // 1. Clone ảnh để tránh khóa file
            Image imgBuffer = (Image)picHinhAnh.Image.Clone();
            imgBuffer.RotateFlip(RotateFlipType.Rotate90FlipNone);

            string fileName = picHinhAnh.Tag.ToString();
            string path = Path.Combine(imagesFolder, fileName);

            // 2. Giải phóng ảnh cũ
            picHinhAnh.Image.Dispose();

            // 3. Lưu đè và gán lại
            imgBuffer.Save(path);
            picHinhAnh.Image = imgBuffer;
            MessageBox.Show("Đã xoay ảnh!");
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "HinhAnh")
            {
                try
                {
                    // 2. Kiểm tra nếu giá trị ô không trống và là chuỗi
                    if (e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        string fileName = e.Value.ToString();
                        string fullPath = Path.Combine(imagesFolder, fileName);

                        // 3. Kiểm tra file có tồn tại thật không trước khi đọc
                        if (File.Exists(fullPath))
                        {
                            // Dùng MemoryStream để đọc ảnh (tránh lỗi khóa file "A generic error occurred in GDI+")
                            byte[] bytes = File.ReadAllBytes(fullPath);
                            using (MemoryStream ms = new MemoryStream(bytes))
                            {
                                Image img = Image.FromStream(ms);
                                // Resize ảnh về 24x24 hoặc kích thước bạn muốn
                                e.Value = new Bitmap(img, 24, 24);
                            }
                            e.FormattingApplied = true; // Xác nhận đã xử lý xong
                        }
                        else
                        {
                            e.Value = null; // Nếu không thấy file thì để trống ô
                        }
                    }
                }
                catch
                {
                    e.Value = null; // Nếu có bất kỳ lỗi gì phát sinh (file hỏng...) thì bỏ qua
                }
            }
            }
        private Image LoadImage(string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName)) return null;
                string fullPath = Path.Combine(imagesFolder, fileName);
                if (!File.Exists(fullPath)) return null;

                // Đọc bytes rồi mới convert sang Image để không chiếm dụng file
                byte[] bytes = File.ReadAllBytes(fullPath);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    return Image.FromStream(ms);
                }
            }
            catch { return null; }
        }
    }
}


    

