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


namespace QuanLyBanHang.From
{
    public partial class Sản_phẩm : Form
    {
        public Sản_phẩm()
        {
            InitializeComponent();
        }


        QLBHDbContext context = new QLBHDbContext();
        bool xuLyThem = false;
        int id;
        string imagesFolder = Path.Combine(Application.StartupPath, "Images");

        private void LoadDataGridView()
        {
            var danhSach = context.SanPham
       .Include(x => x.LoaiSanPham)
       .Include(x => x.HangSanXuat)
       .AsEnumerable()
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
           HinhAnh = LoadImage(r.HinhAnh)
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
            cboHangSanXuat.DisplayMember = "TenHangSanXuat";
            cboHangSanXuat.ValueMember = "ID";
        }


        private void Sản_phẩm_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            LayLoaiSanPhamVaoComboBox();
            LayHangSanXuatVaoComboBox();

            numDonGia.Maximum = 1000000000;
            numDonGia.ThousandsSeparator = true;

            dataGridView.AutoGenerateColumns = false;

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

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;

            xuLyThem = false;
            BatTatChucNang(true);

            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboLoaiSanPham.Text))
                MessageBox.Show("Vui lòng chọn loại sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(cboHangSanXuat.Text))
                MessageBox.Show("Vui lòng chọn hãng sản xuất.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(txtTenSanPham.Text))
                MessageBox.Show("Vui lòng nhập tên sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (numSoLuong.Value <= 0)
                MessageBox.Show("Số lượng phải lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (numDonGia.Value <= 0)
                MessageBox.Show("Đơn giá sản phẩm phải lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (xuLyThem)

                    if (xuLyThem)
                    {
                        SanPham sp = new SanPham();
                        sp.LoaiSanPhamID = (int)cboLoaiSanPham.SelectedValue;
                        sp.HangSanXuatID = (int)cboHangSanXuat.SelectedValue;
                        sp.TenSanPham = txtTenSanPham.Text;
                        sp.MoTa = txtMoTa.Text;
                        sp.SoLuong = (int)numSoLuong.Value;
                        sp.DonGia = (int)numDonGia.Value;
                        sp.HinhAnh = picHinhAnh.Tag?.ToString();

                        context.SanPham.Add(sp);
                    }
                    else
                    {
                        SanPham sp = context.SanPham.Find(id);
                        if (sp != null)
                        {
                            sp.LoaiSanPhamID = (int)cboLoaiSanPham.SelectedValue;
                            sp.HangSanXuatID = (int)cboHangSanXuat.SelectedValue;
                            sp.TenSanPham = txtTenSanPham.Text;
                            sp.MoTa = txtMoTa.Text;
                            sp.SoLuong = (int)numSoLuong.Value;
                            sp.DonGia = (int)numDonGia.Value;
                            sp.HinhAnh = picHinhAnh.Tag?.ToString();
                        }
                    }

                context.SaveChanges();
                Sản_phẩm_Load(sender, e);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;

            var row = dataGridView.CurrentRow.DataBoundItem as DanhSachSanPham;
            if (row == null) return;

            id = row.ID;

            var sp = context.SanPham.Find(id);
            if (sp != null)
            {
                context.SanPham.Remove(sp);
                context.SaveChanges();
            }

            Sản_phẩm_Load(sender, e);

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
            var excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;

            var wb = excel.Workbooks.Add();
            var ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;

            // Tiêu đề cột
            ws.Cells[1, 1] = "ID";
            ws.Cells[1, 2] = "Loại sản phẩm";
            ws.Cells[1, 3] = "Hãng sản xuất";
            ws.Cells[1, 4] = "Tên sản phẩm";
            ws.Cells[1, 5] = "Số lượng";
            ws.Cells[1, 6] = "Đơn giá";

            var list = context.SanPham
                              .Include(x => x.LoaiSanPham)
                              .Include(x => x.HangSanXuat)
                              .ToList();

            int row = 2;

            foreach (var sp in list)
            {
                ws.Cells[row, 1] = sp.ID;
                ws.Cells[row, 2] = sp.LoaiSanPham?.TenLoai;
                ws.Cells[row, 3] = sp.HangSanXuat?.TenHangSanXuat;
                ws.Cells[row, 4] = sp.TenSanPham;
                ws.Cells[row, 5] = sp.SoLuong;
                ws.Cells[row, 6] = sp.DonGia;
                row++;
            }

            // Tự động chỉnh độ rộng cột
            ws.Columns.AutoFit();

            MessageBox.Show("Xuất Excel thành công!");
        }
        


        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "CSV file (*.csv)|*.csv";

            if (open.ShowDialog() == DialogResult.OK)
            {
                var lines = File.ReadAllLines(open.FileName);

                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(',');

                    SanPham sp = new SanPham()
                    {
                        TenSanPham = parts[0],
                        SoLuong = int.Parse(parts[1]),
                        DonGia = int.Parse(parts[2]),
                        LoaiSanPhamID = 1,      // cần chỉnh theo hệ thống bạn
                        HangSanXuatID = 1       // cần chỉnh theo hệ thống bạn
                    };

                    context.SanPham.Add(sp);
                }

                context.SaveChanges();
                Sản_phẩm_Load(sender, e);

                MessageBox.Show("Nhập dữ liệu sản phẩm thành công!");
            }

        }

        private void btnDoiAnh_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm trước!");
                return;
            }

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (open.ShowDialog() != DialogResult.OK) return;

            // Tạo thư mục Images nếu chưa có
            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            // Lấy tên file
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(open.FileName);
            string savePath = Path.Combine(imagesFolder, fileName);

            // Copy ảnh vào thư mục Images
            File.Copy(open.FileName, savePath, true);

            // Lấy ID bằng DataBoundItem (KHÔNG dùng Cells["ID"])
            var row = dataGridView.CurrentRow.DataBoundItem as DanhSachSanPham;
            if (row == null) return;

            var sp = context.SanPham.Find(row.ID);
            if (sp == null) return;

            // Cập nhật DB
            sp.HinhAnh = fileName;
            context.SaveChanges();

            // Hiển thị ảnh bằng LoadImage (không khóa file)
            picHinhAnh.Image = LoadImage(fileName);
            picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
            picHinhAnh.Tag = fileName;

            // Load lại DataGridView
            LoadDataGridView();

            MessageBox.Show("Đổi ảnh thành công!");
        }
       

        private Image LoadImage(string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                    return null;

                string fullPath = Path.Combine(imagesFolder, fileName);

                if (!File.Exists(fullPath))
                    return null;

                byte[] bytes = File.ReadAllBytes(fullPath);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    return Image.FromStream(ms);
                }
            }
            catch
            {
                return null;
            }
        }



        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView.Rows[e.RowIndex].DataBoundItem as DanhSachSanPham;
            if (row == null) return;

            var sp = context.SanPham.Find(row.ID);

            if (sp != null)
            {
                picHinhAnh.Image = LoadImage(sp.HinhAnh);
                picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
                picHinhAnh.Tag = sp.HinhAnh;
            }
        }

        private void btnXoayanh_Click(object sender, EventArgs e)
        {
            if (picHinhAnh.Image == null)
            {
                MessageBox.Show("Chưa có ảnh để xoay!");
                return;
            }

            picHinhAnh.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            picHinhAnh.Refresh();

            string fileName = picHinhAnh.Tag?.ToString();
            if (!string.IsNullOrEmpty(fileName))
            {
                string path = Path.Combine(imagesFolder, fileName);
                picHinhAnh.Image.Save(path);
            }

            LoadDataGridView();
        }
    }
}


    

