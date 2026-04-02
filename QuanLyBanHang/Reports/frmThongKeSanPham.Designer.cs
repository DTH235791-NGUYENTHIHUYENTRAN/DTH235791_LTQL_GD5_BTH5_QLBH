namespace QuanLyBanHang.Reports
{
    partial class frmThongKeSanPham
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
            reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            label1 = new Label();
            lblLoaisanpham = new Label();
            cboHangSanXuat = new ComboBox();
            cboLoaiSanPham = new ComboBox();
            btnLocKetQua = new Button();
            SuspendLayout();
            // 
            // reportViewer
            // 
            reportViewer.Location = new Point(12, 94);
            reportViewer.Name = "reportViewer";
            reportViewer.ServerReport.BearerToken = null;
            reportViewer.Size = new Size(912, 464);
            reportViewer.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(42, 35);
            label1.Name = "label1";
            label1.Size = new Size(106, 20);
            label1.TabIndex = 1;
            label1.Text = "Hãng sản xuất:";
            // 
            // lblLoaisanpham
            // 
            lblLoaisanpham.AutoSize = true;
            lblLoaisanpham.Location = new Point(389, 30);
            lblLoaisanpham.Name = "lblLoaisanpham";
            lblLoaisanpham.Size = new Size(105, 20);
            lblLoaisanpham.TabIndex = 2;
            lblLoaisanpham.Text = "Loại sản phẩm";
            // 
            // cboHangSanXuat
            // 
            cboHangSanXuat.FormattingEnabled = true;
            cboHangSanXuat.Location = new Point(176, 27);
            cboHangSanXuat.Name = "cboHangSanXuat";
            cboHangSanXuat.Size = new Size(151, 28);
            cboHangSanXuat.TabIndex = 3;
            // 
            // cboLoaiSanPham
            // 
            cboLoaiSanPham.FormattingEnabled = true;
            cboLoaiSanPham.Location = new Point(516, 27);
            cboLoaiSanPham.Name = "cboLoaiSanPham";
            cboLoaiSanPham.Size = new Size(151, 28);
            cboLoaiSanPham.TabIndex = 4;
            // 
            // btnLocKetQua
            // 
            btnLocKetQua.Location = new Point(718, 21);
            btnLocKetQua.Name = "btnLocKetQua";
            btnLocKetQua.Size = new Size(94, 29);
            btnLocKetQua.TabIndex = 5;
            btnLocKetQua.Text = "Lọc kết quả";
            btnLocKetQua.UseVisualStyleBackColor = true;
            btnLocKetQua.Click += btnLocKetQua_Click;
            // 
            // frmThongKeSanPham
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(936, 570);
            Controls.Add(btnLocKetQua);
            Controls.Add(cboLoaiSanPham);
            Controls.Add(cboHangSanXuat);
            Controls.Add(lblLoaisanpham);
            Controls.Add(label1);
            Controls.Add(reportViewer);
            Name = "frmThongKeSanPham";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmThongKeSanPham";
            Load += frmThongKeSanPham_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private QLBHDataSet qlbhDataSet1;
        private QLBHDataSet qlbhDataSet2;
        private QLBHDataSet qlbhDataSet3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private Label label1;
        private Label lblLoaisanpham;
        private ComboBox cboHangSanXuat;
        private ComboBox cboLoaiSanPham;
        private Button btnLocKetQua;
    }
}