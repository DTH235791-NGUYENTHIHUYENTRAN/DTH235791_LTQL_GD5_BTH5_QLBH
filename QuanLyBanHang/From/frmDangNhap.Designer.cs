namespace QuanLyBanHang
{
    partial class frmDangNhap
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
            lbldangnhap = new Label();
            lbltendn = new Label();
            lblmatkhau = new Label();
            txtTenDangNhap = new TextBox();
            txtMatKhau = new TextBox();
            btnHuyBo = new Button();
            pictureBox1 = new PictureBox();
            btnDangNhapp = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lbldangnhap
            // 
            lbldangnhap.AutoSize = true;
            lbldangnhap.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lbldangnhap.Location = new Point(314, 36);
            lbldangnhap.Name = "lbldangnhap";
            lbldangnhap.Size = new Size(183, 32);
            lbldangnhap.TabIndex = 0;
            lbldangnhap.Text = "ĐĂNG NHẬP";
            // 
            // lbltendn
            // 
            lbltendn.AutoSize = true;
            lbltendn.Font = new Font("Times New Roman", 12F);
            lbltendn.Location = new Point(292, 99);
            lbltendn.Name = "lbltendn";
            lbltendn.Size = new Size(139, 22);
            lbltendn.TabIndex = 1;
            lbltendn.Text = "Tên Đăng Nhập:";
            // 
            // lblmatkhau
            // 
            lblmatkhau.AutoSize = true;
            lblmatkhau.Font = new Font("Times New Roman", 12F);
            lblmatkhau.Location = new Point(297, 182);
            lblmatkhau.Name = "lblmatkhau";
            lblmatkhau.Size = new Size(93, 22);
            lblmatkhau.TabIndex = 2;
            lblmatkhau.Text = "Mật Khẩu:";
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Font = new Font("Times New Roman", 12F);
            txtTenDangNhap.Location = new Point(297, 138);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(258, 30);
            txtTenDangNhap.TabIndex = 3;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Font = new Font("Times New Roman", 12F);
            txtMatKhau.Location = new Point(302, 217);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PasswordChar = '⚫';
            txtMatKhau.Size = new Size(253, 30);
            txtMatKhau.TabIndex = 4;
            txtMatKhau.KeyDown += txtMatKhau_KeyDown;
            // 
            // btnHuyBo
            // 
            btnHuyBo.Font = new Font("Times New Roman", 12F);
            btnHuyBo.Location = new Point(461, 293);
            btnHuyBo.Name = "btnHuyBo";
            btnHuyBo.Size = new Size(94, 29);
            btnHuyBo.TabIndex = 6;
            btnHuyBo.Text = "Hủy Bỏ";
            btnHuyBo.UseVisualStyleBackColor = true;
            btnHuyBo.Click += btnHuyBo_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Screenshot_2026_03_13_084337;
            pictureBox1.Location = new Point(57, 99);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(210, 223);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // btnDangNhapp
            // 
            btnDangNhapp.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnDangNhapp.Location = new Point(302, 293);
            btnDangNhapp.Name = "btnDangNhapp";
            btnDangNhapp.Size = new Size(131, 29);
            btnDangNhapp.TabIndex = 8;
            btnDangNhapp.Text = "Đăng Nhập";
            btnDangNhapp.UseVisualStyleBackColor = true;
            btnDangNhapp.Click += btnDangNhapp_Click;
            // 
            // frmDangNhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(606, 367);
            Controls.Add(btnDangNhapp);
            Controls.Add(pictureBox1);
            Controls.Add(btnHuyBo);
            Controls.Add(txtMatKhau);
            Controls.Add(txtTenDangNhap);
            Controls.Add(lblmatkhau);
            Controls.Add(lbltendn);
            Controls.Add(lbldangnhap);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập ";
            Load += frmDangNhap_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbldangnhap;
        private Label lbltendn;
        private Label lblmatkhau;
        public TextBox txtTenDangNhap;
        private Button button1;
        private Button btnHuyBo;
        public TextBox txtMatKhau;
        private PictureBox pictureBox1;
        private Button btnDangNhapp;
    }
}