namespace QuanLyBanHang
{
    partial class frmKhachHang
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
            DienThoai = new DataGridViewTextBoxColumn();
            dataGridView1 = new DataGridView();
            HoTen = new DataGridViewTextBoxColumn();
            idd = new DataGridViewTextBoxColumn();
            btnXuat = new Button();
            btnThoat = new Button();
            btnXoa = new Button();
            btnHuybo = new Button();
            btnLuu = new Button();
            btnThem = new Button();
            txtDienThoai = new TextBox();
            txtHoTen = new TextBox();
            lblDs = new Label();
            lbldt = new Label();
            lblHoTen = new Label();
            lbltt = new Label();
            btnSua = new Button();
            btntimkiem = new Button();
            btnnhapp = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // DienThoai
            // 
            DienThoai.HeaderText = "Điện Thoại";
            DienThoai.MinimumWidth = 6;
            DienThoai.Name = "DienThoai";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { HoTen, idd, DienThoai });
            dataGridView1.Location = new Point(0, 214);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(801, 232);
            dataGridView1.TabIndex = 48;
            // 
            // HoTen
            // 
            HoTen.HeaderText = "Họ Tên";
            HoTen.MinimumWidth = 6;
            HoTen.Name = "HoTen";
            // 
            // idd
            // 
            idd.HeaderText = "ID";
            idd.MinimumWidth = 6;
            idd.Name = "idd";
            // 
            // btnXuat
            // 
            btnXuat.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnXuat.Location = new Point(575, 147);
            btnXuat.Name = "btnXuat";
            btnXuat.Size = new Size(94, 29);
            btnXuat.TabIndex = 47;
            btnXuat.Text = "Xuất";
            btnXuat.UseVisualStyleBackColor = true;
            btnXuat.Click += btnXuat_Click;
            // 
            // btnThoat
            // 
            btnThoat.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnThoat.Location = new Point(333, 147);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(94, 29);
            btnThoat.TabIndex = 46;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // btnXoa
            // 
            btnXoa.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnXoa.Location = new Point(79, 147);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 45;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnHuybo
            // 
            btnHuybo.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnHuybo.Location = new Point(526, 102);
            btnHuybo.Name = "btnHuybo";
            btnHuybo.Size = new Size(94, 29);
            btnHuybo.TabIndex = 43;
            btnHuybo.Text = "Hủy bỏ";
            btnHuybo.UseVisualStyleBackColor = true;
            btnHuybo.Click += btnHuybo_Click;
            // 
            // btnLuu
            // 
            btnLuu.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnLuu.Location = new Point(132, 102);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(94, 29);
            btnLuu.TabIndex = 41;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnThem
            // 
            btnThem.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnThem.Location = new Point(12, 102);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 40;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // txtDienThoai
            // 
            txtDienThoai.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            txtDienThoai.Location = new Point(526, 43);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(262, 30);
            txtDienThoai.TabIndex = 39;
            // 
            // txtHoTen
            // 
            txtHoTen.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            txtHoTen.Location = new Point(90, 43);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(240, 30);
            txtHoTen.TabIndex = 38;
            // 
            // lblDs
            // 
            lblDs.AutoSize = true;
            lblDs.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            lblDs.Location = new Point(0, 188);
            lblDs.Name = "lblDs";
            lblDs.Size = new Size(196, 23);
            lblDs.TabIndex = 37;
            lblDs.Text = "Danh sách khách hàng";
            // 
            // lbldt
            // 
            lbldt.AutoSize = true;
            lbldt.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            lbldt.Location = new Point(396, 50);
            lbldt.Name = "lbldt";
            lbldt.Size = new Size(102, 23);
            lbldt.TabIndex = 36;
            lbldt.Text = "Điện Thoại";
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            lblHoTen.Location = new Point(12, 50);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(72, 23);
            lblHoTen.TabIndex = 35;
            lblHoTen.Text = "Họ Tên";
            // 
            // lbltt
            // 
            lbltt.AutoSize = true;
            lbltt.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            lbltt.Location = new Point(12, 5);
            lbltt.Name = "lbltt";
            lbltt.Size = new Size(190, 23);
            lbltt.TabIndex = 34;
            lbltt.Text = "Thông tin khách hàng";
            // 
            // btnSua
            // 
            btnSua.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnSua.Location = new Point(404, 102);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 42;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btntimkiem
            // 
            btntimkiem.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btntimkiem.Location = new Point(254, 102);
            btntimkiem.Name = "btntimkiem";
            btntimkiem.Size = new Size(120, 29);
            btntimkiem.TabIndex = 49;
            btntimkiem.Text = "Tìm Kiếm";
            btntimkiem.UseVisualStyleBackColor = true;
            btntimkiem.Click += btntimkiem_Click;
            // 
            // btnnhapp
            // 
            btnnhapp.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnnhapp.Location = new Point(656, 102);
            btnnhapp.Name = "btnnhapp";
            btnnhapp.Size = new Size(94, 29);
            btnnhapp.TabIndex = 51;
            btnnhapp.Text = "Nhập";
            btnnhapp.UseVisualStyleBackColor = true;
            btnnhapp.Click += btnnhapp_Click;
            // 
            // frmKhachHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnnhapp);
            Controls.Add(btntimkiem);
            Controls.Add(dataGridView1);
            Controls.Add(btnXuat);
            Controls.Add(btnThoat);
            Controls.Add(btnXoa);
            Controls.Add(btnHuybo);
            Controls.Add(btnLuu);
            Controls.Add(btnThem);
            Controls.Add(txtDienThoai);
            Controls.Add(txtHoTen);
            Controls.Add(lblDs);
            Controls.Add(lbldt);
            Controls.Add(lblHoTen);
            Controls.Add(lbltt);
            Controls.Add(btnSua);
            Name = "frmKhachHang";
            Text = "frmKhachHang";
            Load += frmKhachHang_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridViewTextBoxColumn DienThoai;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn HoTen;
        private DataGridViewTextBoxColumn idd;
        private Button btnXuat;
        private Button btnThoat;
        private Button btnXoa;
        private Button btnHuybo;
        private Button btnLuu;
        private Button btnThem;
        private TextBox txtDienThoai;
        private TextBox txtHoTen;
        private Label lblDs;
        private Label lbldt;
        private Label lblHoTen;
        private Label lbltt;
        private Button btnSua;
        private Button btntimkiem;
        private Button btnnhapp;
    }
}