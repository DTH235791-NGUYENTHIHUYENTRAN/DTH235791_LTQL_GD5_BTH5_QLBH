namespace QuanLyBanHang.Reports
{
    partial class frmThongKeDoanhThu
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
            lblNgay = new Label();
            lbldenngay = new Label();
            dtpTuNgay = new DateTimePicker();
            dtpDenNgay = new DateTimePicker();
            btnLocKetQua = new Button();
            btnHienTatCa = new Button();
            qlbhDataSet1 = new QLBHDataSet();
            ((System.ComponentModel.ISupportInitialize)qlbhDataSet1).BeginInit();
            SuspendLayout();
            // 
            // reportViewer
            // 
            reportViewer.Location = new Point(24, 108);
            reportViewer.Name = "reportViewer";
            reportViewer.ServerReport.BearerToken = null;
            reportViewer.Size = new Size(875, 308);
            reportViewer.TabIndex = 0;
            // 
            // lblNgay
            // 
            lblNgay.AutoSize = true;
            lblNgay.Location = new Point(24, 32);
            lblNgay.Name = "lblNgay";
            lblNgay.Size = new Size(65, 20);
            lblNgay.TabIndex = 1;
            lblNgay.Text = "Từ Ngày";
            // 
            // lbldenngay
            // 
            lbldenngay.AutoSize = true;
            lbldenngay.Location = new Point(348, 30);
            lbldenngay.Name = "lbldenngay";
            lbldenngay.Size = new Size(75, 20);
            lbldenngay.TabIndex = 2;
            lbldenngay.Text = "Đến Ngày";
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.Location = new Point(95, 27);
            dtpTuNgay.MaxDate = new DateTime(2100, 12, 31, 0, 0, 0, 0);
            dtpTuNgay.MinDate = new DateTime(2020, 1, 1, 0, 0, 0, 0);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(235, 27);
            dtpTuNgay.TabIndex = 3;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.CustomFormat = "dd/MM/yyyy ";
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.Location = new Point(452, 25);
            dtpDenNgay.MaxDate = new DateTime(2100, 12, 31, 0, 0, 0, 0);
            dtpDenNgay.MinDate = new DateTime(2020, 1, 1, 0, 0, 0, 0);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(130, 27);
            dtpDenNgay.TabIndex = 4;
            // 
            // btnLocKetQua
            // 
            btnLocKetQua.Location = new Point(625, 25);
            btnLocKetQua.Name = "btnLocKetQua";
            btnLocKetQua.Size = new Size(141, 29);
            btnLocKetQua.TabIndex = 5;
            btnLocKetQua.Text = "Lọc Kết Quả";
            btnLocKetQua.UseVisualStyleBackColor = true;
            btnLocKetQua.Click += btnLocKetQua_Click;
            // 
            // btnHienTatCa
            // 
            btnHienTatCa.Location = new Point(805, 24);
            btnHienTatCa.Name = "btnHienTatCa";
            btnHienTatCa.Size = new Size(94, 29);
            btnHienTatCa.TabIndex = 6;
            btnHienTatCa.Text = "Hiện Tất Cả";
            btnHienTatCa.UseVisualStyleBackColor = true;
            btnHienTatCa.Click += btnHienTatCa_Click;
            // 
            // qlbhDataSet1
            // 
            qlbhDataSet1.DataSetName = "QLBHDataSet";
            qlbhDataSet1.Namespace = "http://tempuri.org/QLBHDataSet.xsd";
            qlbhDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmThongKeDoanhThu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(923, 450);
            Controls.Add(btnHienTatCa);
            Controls.Add(btnLocKetQua);
            Controls.Add(dtpDenNgay);
            Controls.Add(dtpTuNgay);
            Controls.Add(lbldenngay);
            Controls.Add(lblNgay);
            Controls.Add(reportViewer);
            Name = "frmThongKeDoanhThu";
            Text = "Thống kê doanh thu";
            Load += frmThongKeDoanhThu_Load;
            ((System.ComponentModel.ISupportInitialize)qlbhDataSet1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private Label lblNgay;
        private Label lbldenngay;
        private DateTimePicker dtpTuNgay;
        private DateTimePicker dtpDenNgay;
        private Button btnLocKetQua;
        private Button btnHienTatCa;
        private QLBHDataSet qlbhDataSet1;
    }
}