namespace QuanLyBanHang.From
{
    partial class FrmHoaDon_ChiTiet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            lblthongtin = new Label();
            cboNhanVien = new ComboBox();
            cboKhachHang = new ComboBox();
            label2 = new Label();
            lblKhach = new Label();
            lblghichu = new Label();
            label5 = new Label();
            label6 = new Label();
            lbldongia = new Label();
            lblsoluong = new Label();
            cboSanPham = new ComboBox();
            numDonGiaBan = new NumericUpDown();
            numSoLuongBan = new NumericUpDown();
            btnXacNhanBan = new Button();
            btnXoa = new Button();
            btnLuuHoaDon = new Button();
            btnInHoaDon = new Button();
            btnThoat = new Button();
            dataGridView = new DataGridView();
            SanPhamID = new DataGridViewTextBoxColumn();
            TenSanPham = new DataGridViewTextBoxColumn();
            DonGiaBan = new DataGridViewTextBoxColumn();
            SoLuongBan = new DataGridViewTextBoxColumn();
            ThanhTien = new DataGridViewTextBoxColumn();
            txtGhiChuHoaDon = new TextBox();
            btnNhap = new Button();
            btnXuat = new Button();
            ((System.ComponentModel.ISupportInitialize)numDonGiaBan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSoLuongBan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // lblthongtin
            // 
            lblthongtin.AutoSize = true;
            lblthongtin.Location = new Point(34, 18);
            lblthongtin.Name = "lblthongtin";
            lblthongtin.Size = new Size(139, 20);
            lblthongtin.TabIndex = 0;
            lblthongtin.Text = "Thông Tin Hóa Đơn";
            // 
            // cboNhanVien
            // 
            cboNhanVien.FormattingEnabled = true;
            cboNhanVien.Location = new Point(202, 57);
            cboNhanVien.Name = "cboNhanVien";
            cboNhanVien.Size = new Size(172, 28);
            cboNhanVien.TabIndex = 1;
            // 
            // cboKhachHang
            // 
            cboKhachHang.FormattingEnabled = true;
            cboKhachHang.Location = new Point(578, 57);
            cboKhachHang.Name = "cboKhachHang";
            cboKhachHang.Size = new Size(191, 28);
            cboKhachHang.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 60);
            label2.Name = "label2";
            label2.Size = new Size(124, 20);
            label2.TabIndex = 3;
            label2.Text = "Nhân Viên Lập(*):";
            // 
            // lblKhach
            // 
            lblKhach.AutoSize = true;
            lblKhach.Location = new Point(432, 65);
            lblKhach.Name = "lblKhach";
            lblKhach.Size = new Size(108, 20);
            lblKhach.TabIndex = 4;
            lblKhach.Text = "Khách Hàng(*):";
            // 
            // lblghichu
            // 
            lblghichu.AutoSize = true;
            lblghichu.Location = new Point(34, 111);
            lblghichu.Name = "lblghichu";
            lblghichu.Size = new Size(127, 20);
            lblghichu.TabIndex = 5;
            lblghichu.Text = "Ghi Chú Hóa Đơn:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 174);
            label5.Name = "label5";
            label5.Size = new Size(180, 20);
            label5.TabIndex = 6;
            label5.Text = "Thông tin chi tiết hoa đơn";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 204);
            label6.Name = "label6";
            label6.Size = new Size(94, 20);
            label6.TabIndex = 7;
            label6.Text = "Sản phẩm(*):";
            // 
            // lbldongia
            // 
            lbldongia.AutoSize = true;
            lbldongia.Location = new Point(216, 204);
            lbldongia.Name = "lbldongia";
            lbldongia.Size = new Size(110, 20);
            lbldongia.TabIndex = 8;
            lbldongia.Text = "Đơn giá bán(*):";
            // 
            // lblsoluong
            // 
            lblsoluong.AutoSize = true;
            lblsoluong.Location = new Point(443, 204);
            lblsoluong.Name = "lblsoluong";
            lblsoluong.Size = new Size(117, 20);
            lblsoluong.TabIndex = 9;
            lblsoluong.Text = "Số lượng bán(*):";
            // 
            // cboSanPham
            // 
            cboSanPham.FormattingEnabled = true;
            cboSanPham.Location = new Point(26, 233);
            cboSanPham.Name = "cboSanPham";
            cboSanPham.Size = new Size(151, 28);
            cboSanPham.TabIndex = 10;
            // 
            // numDonGiaBan
            // 
            numDonGiaBan.Location = new Point(224, 233);
            numDonGiaBan.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numDonGiaBan.Name = "numDonGiaBan";
            numDonGiaBan.Size = new Size(150, 27);
            numDonGiaBan.TabIndex = 11;
            numDonGiaBan.ThousandsSeparator = true;
            // 
            // numSoLuongBan
            // 
            numSoLuongBan.Location = new Point(443, 234);
            numSoLuongBan.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numSoLuongBan.Name = "numSoLuongBan";
            numSoLuongBan.Size = new Size(150, 27);
            numSoLuongBan.TabIndex = 12;
            numSoLuongBan.ThousandsSeparator = true;
            // 
            // btnXacNhanBan
            // 
            btnXacNhanBan.Location = new Point(678, 224);
            btnXacNhanBan.Name = "btnXacNhanBan";
            btnXacNhanBan.Size = new Size(128, 29);
            btnXacNhanBan.TabIndex = 13;
            btnXacNhanBan.Text = "Xác nhận bán";
            btnXacNhanBan.UseVisualStyleBackColor = true;
            btnXacNhanBan.Click += btnXacNhanBan_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(876, 224);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 14;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLuuHoaDon
            // 
            btnLuuHoaDon.Location = new Point(183, 516);
            btnLuuHoaDon.Name = "btnLuuHoaDon";
            btnLuuHoaDon.Size = new Size(152, 29);
            btnLuuHoaDon.TabIndex = 15;
            btnLuuHoaDon.Text = "Lưu Hóa Đơn";
            btnLuuHoaDon.UseVisualStyleBackColor = true;
            btnLuuHoaDon.Click += btnLuuHoaDon_Click;
            // 
            // btnInHoaDon
            // 
            btnInHoaDon.Location = new Point(370, 506);
            btnInHoaDon.Name = "btnInHoaDon";
            btnInHoaDon.Size = new Size(105, 35);
            btnInHoaDon.TabIndex = 16;
            btnInHoaDon.Text = "In Hóa Đơn";
            btnInHoaDon.UseVisualStyleBackColor = true;
            btnInHoaDon.Click += btnInHoaDon_Click;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(499, 512);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(94, 29);
            btnThoat.TabIndex = 17;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { SanPhamID, TenSanPham, DonGiaBan, SoLuongBan, ThanhTien });
            dataGridView.Location = new Point(34, 290);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(1080, 203);
            dataGridView.TabIndex = 18;
            dataGridView.CellContentClick += dataGridView_CellContentClick;
            // 
            // SanPhamID
            // 
            SanPhamID.DataPropertyName = "ID";
            SanPhamID.HeaderText = "ID";
            SanPhamID.MinimumWidth = 6;
            SanPhamID.Name = "SanPhamID";
            // 
            // TenSanPham
            // 
            TenSanPham.DataPropertyName = "TenSanPham";
            TenSanPham.HeaderText = "Tên Sản Phẩm";
            TenSanPham.MinimumWidth = 6;
            TenSanPham.Name = "TenSanPham";
            // 
            // DonGiaBan
            // 
            DonGiaBan.DataPropertyName = "DonGiaBan";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            DonGiaBan.DefaultCellStyle = dataGridViewCellStyle1;
            DonGiaBan.HeaderText = "Đơn Giá Bán";
            DonGiaBan.MinimumWidth = 6;
            DonGiaBan.Name = "DonGiaBan";
            // 
            // SoLuongBan
            // 
            SoLuongBan.DataPropertyName = "SoLuongBan";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            SoLuongBan.DefaultCellStyle = dataGridViewCellStyle2;
            SoLuongBan.HeaderText = "Số Lượng Bán";
            SoLuongBan.MinimumWidth = 6;
            SoLuongBan.Name = "SoLuongBan";
            // 
            // ThanhTien
            // 
            ThanhTien.DataPropertyName = "ThanhTien";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            dataGridViewCellStyle3.ForeColor = Color.Blue;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            ThanhTien.DefaultCellStyle = dataGridViewCellStyle3;
            ThanhTien.HeaderText = "Thành Tiền";
            ThanhTien.MinimumWidth = 6;
            ThanhTien.Name = "ThanhTien";
            // 
            // txtGhiChuHoaDon
            // 
            txtGhiChuHoaDon.Location = new Point(202, 111);
            txtGhiChuHoaDon.Name = "txtGhiChuHoaDon";
            txtGhiChuHoaDon.Size = new Size(561, 27);
            txtGhiChuHoaDon.TabIndex = 19;
            // 
            // btnNhap
            // 
            btnNhap.Location = new Point(638, 512);
            btnNhap.Name = "btnNhap";
            btnNhap.Size = new Size(94, 29);
            btnNhap.TabIndex = 20;
            btnNhap.Text = "Nhập";
            btnNhap.UseVisualStyleBackColor = true;
            btnNhap.Click += btnNhap_Click;
            // 
            // btnXuat
            // 
            btnXuat.Location = new Point(786, 509);
            btnXuat.Name = "btnXuat";
            btnXuat.Size = new Size(94, 29);
            btnXuat.TabIndex = 21;
            btnXuat.Text = "Xuất";
            btnXuat.UseVisualStyleBackColor = true;
            btnXuat.Click += btnXuat_Click;
            // 
            // FrmHoaDon_ChiTiet
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1138, 577);
            Controls.Add(btnXuat);
            Controls.Add(btnNhap);
            Controls.Add(txtGhiChuHoaDon);
            Controls.Add(dataGridView);
            Controls.Add(btnThoat);
            Controls.Add(btnInHoaDon);
            Controls.Add(btnLuuHoaDon);
            Controls.Add(btnXoa);
            Controls.Add(btnXacNhanBan);
            Controls.Add(numSoLuongBan);
            Controls.Add(numDonGiaBan);
            Controls.Add(cboSanPham);
            Controls.Add(lblsoluong);
            Controls.Add(lbldongia);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(lblghichu);
            Controls.Add(lblKhach);
            Controls.Add(label2);
            Controls.Add(cboKhachHang);
            Controls.Add(cboNhanVien);
            Controls.Add(lblthongtin);
            Name = "FrmHoaDon_ChiTiet";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hóa Đơn Chi Tiết";
            Load += FrmHoaDon_ChiTiet_Load;
            ((System.ComponentModel.ISupportInitialize)numDonGiaBan).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSoLuongBan).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblthongtin;
        private ComboBox cboNhanVien;
        private ComboBox cboKhachHang;
        private Label label2;
        private Label lblKhach;
        private Label lblghichu;
        private Label label5;
        private Label label6;
        private Label lbldongia;
        private Label lblsoluong;
        private ComboBox cboSanPham;
        private NumericUpDown numDonGiaBan;
        private NumericUpDown numSoLuongBan;
        private Button btnXacNhanBan;
        private Button btnXoa;
        private Button btnLuuHoaDon;
        private Button btnInHoaDon;
        private Button btnThoat;
        private DataGridView dataGridView;
        private TextBox txtGhiChuHoaDon;
        private DataGridViewTextBoxColumn SanPhamID;
        private DataGridViewTextBoxColumn TenSanPham;
        private DataGridViewTextBoxColumn DonGiaBan;
        private DataGridViewTextBoxColumn SoLuongBan;
        private DataGridViewTextBoxColumn ThanhTien;
        private Button btnNhap;
        private Button btnXuat;
    }
}