using QuanLyBanHang.Data;
using QuanLyBanHang.From; // Lưu ý: Nếu folder của bạn là "Form" thì hãy sửa lại cho đúng chính tả
using QuanLyBanHang.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class FrmMain : Form
    {
        QLBHDbContext context = new QLBHDbContext();

        // Khai báo các Form chức năng
        frmLoaiSanPham loaiSanPham = null;
        frmHangSanXuat hangSanXuat = null;
        Sản_phẩm sanPham = null; // Đã bỏ chữ 'new' bị dư
        frmKhachHang khachHang = null;
        frmNhanVien nhanVien = null;
        FrmHoaDon hoaDon = null;
        frmDangNhap dangNhap = null;

        // Khai báo thêm 2 form thống kê
        frmThongKeDoanhThu thongKeDoanhThu = null;
        frmThongKeSanPham thongKeSanPham = null;

        string hoVaTenNhanVien = "";

        public FrmMain()
        {
            InitializeComponent();
        }

        #region Xử lý Đăng nhập & Phân quyền
        private void DangNhap()
        {
        LamLai:
            if (dangNhap == null || dangNhap.IsDisposed)
                dangNhap = new frmDangNhap();

            if (dangNhap.ShowDialog() == DialogResult.OK)
            {
                string tenDangNhap = dangNhap.txtTenDangNhap.Text;
                string matKhau = dangNhap.txtMatKhau.Text;

                if (string.IsNullOrWhiteSpace(tenDangNhap))
                {
                    MessageBox.Show("Tên đăng nhập không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dangNhap.txtTenDangNhap.Focus();
                    goto LamLai;
                }
                else if (string.IsNullOrWhiteSpace(matKhau))
                {
                    MessageBox.Show("Mật khẩu không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dangNhap.txtMatKhau.Focus();
                    goto LamLai;
                }
                else
                {
                    var nv = context.NhanVien.Where(r => r.TenDangNhap == tenDangNhap).SingleOrDefault();

                    if (nv == null)
                    {
                        MessageBox.Show("Tên đăng nhập không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dangNhap.txtTenDangNhap.Focus();
                        goto LamLai;
                    }
                    else
                    {
                        // Kiểm tra mật khẩu bằng BCrypt
                        if (BCrypt.Net.BCrypt.Verify(matKhau, nv.MatKhau))
                        {
                            hoVaTenNhanVien = nv.HoVaTen;

                            if (nv.QuyenHan == true)
                                QuyenQuanLy();
                            else
                                QuyenNhanVien();
                        }
                        else
                        {
                            MessageBox.Show("Mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dangNhap.txtMatKhau.Focus();
                            goto LamLai;
                        }
                    }
                }
            }
        }

        void QuyenQuanLy()
        {
            lblTrangThai.Text = "Quản lý: " + hoVaTenNhanVien;

            mnuNhanVien.Enabled = true;
            mnuLoaiSanPham.Enabled = true;
            mnuHangSanXuat.Enabled = true;
            mnuSanPham.Enabled = true;
            mnuKhachHang.Enabled = true;
            mnuHoaDon.Enabled = true;

            // Bật các menu thống kê cho Admin
            mnuThongKeDoanhThu.Enabled = true;
            mnuThongKeSanPham.Enabled = true;
        }

        void QuyenNhanVien()
        {
            lblTrangThai.Text = "Nhân viên: " + hoVaTenNhanVien;

            mnuNhanVien.Enabled = false;
            mnuLoaiSanPham.Enabled = false;
            mnuHangSanXuat.Enabled = false;

            mnuSanPham.Enabled = true;
            mnuKhachHang.Enabled = true;
            mnuHoaDon.Enabled = true;

            // Nhân viên thường có thể không được xem doanh thu (tùy bạn chỉnh)
            mnuThongKeDoanhThu.Enabled = false;
            mnuThongKeSanPham.Enabled = true;
        }

        void ChuaDangNhap()
        {
            lblTrangThai.Text = "Chưa đăng nhập";

            mnuNhanVien.Enabled = false;
            mnuLoaiSanPham.Enabled = false;
            mnuHangSanXuat.Enabled = false;
            mnuSanPham.Enabled = false;
            mnuKhachHang.Enabled = false;
            mnuHoaDon.Enabled = false;
            mnuThongKeDoanhThu.Enabled = false;
            mnuThongKeSanPham.Enabled = false;
        }
        #endregion

        #region Xử lý sự kiện Menu Click
        private void mnuLoaiSanPham_Click(object sender, EventArgs e)
        {
            if (loaiSanPham == null || loaiSanPham.IsDisposed)
            {
                loaiSanPham = new frmLoaiSanPham();
                loaiSanPham.MdiParent = this;
                loaiSanPham.Show();
            }
            else loaiSanPham.Activate();
        }

        private void mnuHangSanXuat_Click(object sender, EventArgs e)
        {
            if (hangSanXuat == null || hangSanXuat.IsDisposed)
            {
                hangSanXuat = new frmHangSanXuat();
                hangSanXuat.MdiParent = this;
                hangSanXuat.Show();
            }
            else hangSanXuat.Activate();
        }

        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            if (sanPham == null || sanPham.IsDisposed)
            {
                sanPham = new Sản_phẩm();
                sanPham.MdiParent = this;
                sanPham.Show();
            }
            else sanPham.Activate();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            if (khachHang == null || khachHang.IsDisposed)
            {
                khachHang = new frmKhachHang();
                khachHang.MdiParent = this;
                khachHang.Show();
            }
            else khachHang.Activate();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            if (nhanVien == null || nhanVien.IsDisposed)
            {
                nhanVien = new frmNhanVien();
                nhanVien.MdiParent = this;
                nhanVien.Show();
            }
            else nhanVien.Activate();
        }

        private void mnuHoaDon_Click(object sender, EventArgs e)
        {
            if (hoaDon == null || hoaDon.IsDisposed)
            {
                hoaDon = new FrmHoaDon();
                hoaDon.MdiParent = this;
                hoaDon.Show();
            }
            else hoaDon.Activate();
        }



        #endregion

        #region Các sự kiện khác (Load, Liên kết, Đăng xuất)
        private void FrmMain_Load(object sender, EventArgs e)
        {
            ChuaDangNhap();
            DangNhap();
        }

        private void mnuDangNhap_Click(object sender, EventArgs e)
        {
            DangNhap();
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            foreach (Form child in MdiChildren)
            {
                child.Close();
            }
            ChuaDangNhap();
            DangNhap(); // Đăng xuất xong thì hiện lại bảng đăng nhập luôn
        }

        private void lblLienKet_Click(object sender, EventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer.exe";
            info.Arguments = "https://fit.agu.edu.vn";
            Process.Start(info);
        }
        #endregion

        private void mnuThongKeSanPham_Click_1(object sender, EventArgs e)
        {
            if (thongKeSanPham == null || thongKeSanPham.IsDisposed)
            {
                thongKeSanPham = new frmThongKeSanPham();
                thongKeSanPham.MdiParent = this;
                thongKeSanPham.Show();
            }
            else thongKeSanPham.Activate();
        }

        private void mnuThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            if (thongKeDoanhThu == null || thongKeDoanhThu.IsDisposed)
            {
                thongKeDoanhThu = new frmThongKeDoanhThu();
                thongKeDoanhThu.MdiParent = this;
                thongKeDoanhThu.Show();
            }
            else thongKeDoanhThu.Activate();
        }
    }
    }
