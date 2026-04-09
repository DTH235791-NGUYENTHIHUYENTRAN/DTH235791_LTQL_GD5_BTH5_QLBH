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
using ClosedXML.Excel;

namespace QuanLyBanHang
{
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();

        }
        QLBHDbContext context = new QLBHDbContext();
        // Khởi tạo biến ngữ cảnh CSDL 
        bool xuLyThem = false; // Kiểm tra có nhấn vào nút Thêm hay không? 
        int id;

        private void BatTatChucNang(bool giaTri)
        {
            btnLuu.Enabled = giaTri;
            btnHuybo.Enabled = giaTri;
            txtHoTen.Enabled = giaTri;
            txtDienThoai.Enabled = giaTri;


            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btntimkiem.Enabled = !giaTri;
            btnnhapp.Enabled = !giaTri;
            btnXuat.Enabled = !giaTri;
        }
        void LoadDuLieu()
        {
            var kh = context.KhachHang.ToList();
            dataGridView1.DataSource = kh;

            // Ẩn cột ID
            dataGridView1.Columns["ID"].Visible = true;

            // Đổi tên cột hiển thị cho đẹp
            dataGridView1.Columns["HoVaTen"].HeaderText = "Họ Tên";
            dataGridView1.Columns["DienThoai"].HeaderText = "Điện Thoại";
            dataGridView1.Columns["DiaChi"].HeaderText = "Địa Chỉ";

            txtHoTen.DataBindings.Clear();
            txtHoTen.DataBindings.Add("Text", dataGridView1.DataSource, "HoVaTen", false, DataSourceUpdateMode.Never);

            txtDienThoai.DataBindings.Clear();
            txtDienThoai.DataBindings.Add("Text", dataGridView1.DataSource, "DienThoai", false, DataSourceUpdateMode.Never);
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            
            BatTatChucNang(false);
            LoadDuLieu();
        }

            private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);
            txtHoTen.Clear();
            txtDienThoai.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            xuLyThem = false;
            BatTatChucNang(true);
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                MessageBox.Show("Vui lòng nhập họ và tên khách hàng?", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (xuLyThem)
                {
                    KhachHang kh = new KhachHang();
                    kh.HoVaTen = txtHoTen.Text;
                    kh.DienThoai = txtDienThoai.Text;
                    context.KhachHang.Add(kh);

                    context.SaveChanges();
                }
                else
                {
                    KhachHang kh = context.KhachHang.Find(id);
                    if (kh != null)
                    {
                        kh.HoVaTen = txtHoTen.Text;
                        kh.DienThoai = txtDienThoai.Text;
                        context.KhachHang.Update(kh);

                        context.SaveChanges();
                    }
                }

                frmKhachHang_Load(sender, e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa khách hàng " + txtHoTen.Text + "?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
                KhachHang kh = context.KhachHang.Find(id);
                if (kh != null)
                {
                    context.KhachHang.Remove(kh);
                }
                context.SaveChanges();

                frmKhachHang_Load(sender, e);
            }
        }

        private void btnHuybo_Click(object sender, EventArgs e)
        {
            frmKhachHang_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtHoTen.Text.Trim();

            var ketQua = context.KhachHang
                .Where(k => k.HoVaTen.Contains(tuKhoa) || k.DienThoai.Contains(tuKhoa))
                .ToList();

            dataGridView1.DataSource = ketQua;
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel|*.xlsx",
                FileName = "KhachHang_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("KhachHang");
                        // Lấy toàn bộ danh sách Khách Hàng
                        var data = context.KhachHang.ToList();

                        // Chèn trực tiếp vào ô A1
                        worksheet.Cell(1, 1).InsertTable(data);
                        worksheet.Columns().AdjustToContents(); // Tự động giãn cột

                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Xuất danh sách khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnnhapp_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Chọn file Excel Khách Hàng"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        var worksheet = workbook.Worksheet(1);
                        // Dùng Skip(1) để bỏ qua dòng tiêu đề
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

                        foreach (var row in rows)
                        {
                            KhachHang kh = new KhachHang();

                            // Lưu ý thứ tự cột trong file Excel:
                            // Cột 1: ID (Bỏ qua vì tự tăng)
                            // Cột 2: HoVaTen
                            kh.HoVaTen = row.Cell(2).Value.ToString();

                            // Cột 3: DienThoai
                            kh.DienThoai = row.Cell(3).Value.ToString();

                            // Cột 4: DiaChi
                            kh.DiaChi = row.Cell(4).Value.ToString();

                            context.KhachHang.Add(kh);
                        }

                        context.SaveChanges();
                        MessageBox.Show("Nhập dữ liệu khách hàng thành công!", "Thông báo");

                        // Load lại dữ liệu lên DataGridView
                        frmKhachHang_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi nhập dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            }
        

          
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (dataGridView1.CurrentRow != null)
            {
                txtHoTen.Text = dataGridView1.CurrentRow.Cells["HoVaTen"].Value.ToString();
                txtDienThoai.Text = dataGridView1.CurrentRow.Cells["DienThoai"].Value.ToString();
            }
        }
    }
}