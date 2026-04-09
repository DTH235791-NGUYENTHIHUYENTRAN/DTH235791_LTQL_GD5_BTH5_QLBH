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

namespace QuanLyBanHang.From
{
    public partial class frmHangSanXuat : Form
    {
        public frmHangSanXuat()
        {
            InitializeComponent();
        }


        QLBHDbContext context = new QLBHDbContext();
        bool xulyThem = false;
        int id;
        private void BatTatChucNang(bool giaTri)
        {
            btnHuyBo.Enabled = giaTri;
            btnLuu.Enabled = giaTri;
            txtTenHangSanXuat.Enabled = giaTri;

            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
        }

        private void frmHangSanXuat_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);

            List<HangSanXuat> hsx = new List<HangSanXuat>();
            hsx = context.HangSanXuat.ToList();

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = hsx;

            txtTenHangSanXuat.DataBindings.Clear();
            txtTenHangSanXuat.DataBindings.Add("Text", bindingSource, "TenHangSanXuat", false, DataSourceUpdateMode.Never);

            dataGridView.DataSource = bindingSource;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xulyThem = true;
            BatTatChucNang(true);
            txtTenHangSanXuat.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            xulyThem = false;
            BatTatChucNang(true);
            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa hãng sản xuất?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
                HangSanXuat hsx = context.HangSanXuat.Find(id);
                if (hsx != null)
                {
                    context.HangSanXuat.Remove(hsx);
                }
                context.SaveChanges();

                frmHangSanXuat_Load(sender, e);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenHangSanXuat.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hãng sản xuất?", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (xulyThem)
                {
                    HangSanXuat hsx = new HangSanXuat();
                    hsx.TenHangSanXuat = txtTenHangSanXuat.Text;
                    context.HangSanXuat.Add(hsx);

                    context.SaveChanges();
                }
                else
                {
                    HangSanXuat hsx = context.HangSanXuat.Find(id);
                    if (hsx != null)
                    {
                        hsx.TenHangSanXuat = txtTenHangSanXuat.Text;
                        context.HangSanXuat.Update(hsx);

                        context.SaveChanges();
                    }
                }
                frmHangSanXuat_Load(sender, e);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            frmHangSanXuat_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Chọn file Excel Hãng Sản Xuất"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        var worksheet = workbook.Worksheet(1);
                        // Skip(1) để bỏ qua dòng tiêu đề
                        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

                        foreach (var row in rows)
                        {
                            HangSanXuat hsx = new HangSanXuat();

                            // Giả sử file Excel xuất ra có: Cột 1 là ID, Cột 2 là TenHangSanXuat
                            // Chúng ta chỉ cần nhập Tên Hãng, ID sẽ tự tăng trong DB
                            hsx.TenHangSanXuat = row.Cell(2).Value.ToString();

                            context.HangSanXuat.Add(hsx);
                        }

                        context.SaveChanges();
                        MessageBox.Show("Nhập dữ liệu thành công!", "Thông báo");

                        // Gọi lại hàm Load để cập nhật giao diện
                        frmHangSanXuat_Load(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi nhập dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel|*.xlsx",
                FileName = "HangSanXuat_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("HangSanXuat");
                        // Lấy toàn bộ danh sách Hãng Sản Xuất
                        var data = context.HangSanXuat.ToList();

                        // Chèn bảng dữ liệu vào ô A1
                        worksheet.Cell(1, 1).InsertTable(data);
                        worksheet.Columns().AdjustToContents(); // Tự căn chỉnh độ rộng cột

                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
    }
}
