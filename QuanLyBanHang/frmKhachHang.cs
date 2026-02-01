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

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);

            List<KhachHang> kh = new List<KhachHang>();
            kh = context.KhachHang.ToList();

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = kh;

            txtHoTen.DataBindings.Clear();
            txtHoTen.DataBindings.Add("Text", bindingSource, "HoVaTen", false, DataSourceUpdateMode.Never);


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
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "CSV file (*.csv)|*.csv";

            if (save.ShowDialog() == DialogResult.OK)
            {
                var ds = context.KhachHang.ToList();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("HoVaTen,DienThoai");

                foreach (var k in ds)
                {
                    sb.AppendLine($"{k.HoVaTen},{k.DienThoai},");
                }

                System.IO.File.WriteAllText(save.FileName, sb.ToString(), Encoding.UTF8);

                MessageBox.Show("Xuất dữ liệu thành công!");
            }
        }

        private void btnnhapp_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "CSV file (*.csv)|*.csv";

            if (open.ShowDialog() == DialogResult.OK)
            {
                var lines = System.IO.File.ReadAllLines(open.FileName);

                for (int i = 1; i < lines.Length; i++) // bỏ dòng tiêu đề
                {
                    var parts = lines[i].Split(',');

                    KhachHang kh = new KhachHang()
                    {
                        HoVaTen = parts[1],
                        DienThoai = parts[2],

                    };

                    context.KhachHang.Add(kh);
                }

                context.SaveChanges();
                frmKhachHang_Load(sender, e);

                MessageBox.Show("Nhập dữ liệu thành công!");
            }
        }
    }
}