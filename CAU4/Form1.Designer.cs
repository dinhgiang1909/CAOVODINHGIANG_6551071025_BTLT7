namespace CAU4
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtTenBN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpNgayKham = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpGioKham = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbBacSi = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbTrangThai = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.cmbLocBacSi = new System.Windows.Forms.ComboBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dgvLichKham = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichKham)).BeginInit();
            this.SuspendLayout();

            // Labels & Inputs
            this.label1.AutoSize = true; this.label1.Location = new System.Drawing.Point(20, 20); this.label1.Text = "Tên bệnh nhân";
            this.txtTenBN.Location = new System.Drawing.Point(120, 17); this.txtTenBN.Size = new System.Drawing.Size(200, 23);

            this.label2.AutoSize = true; this.label2.Location = new System.Drawing.Point(20, 60); this.label2.Text = "Số điện thoại";
            this.txtSDT.Location = new System.Drawing.Point(120, 57); this.txtSDT.Size = new System.Drawing.Size(200, 23);

            this.label3.AutoSize = true; this.label3.Location = new System.Drawing.Point(20, 100); this.label3.Text = "Ngày khám";
            this.dtpNgayKham.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpNgayKham.Location = new System.Drawing.Point(120, 97); this.dtpNgayKham.Size = new System.Drawing.Size(120, 23);

            this.label4.AutoSize = true; this.label4.Location = new System.Drawing.Point(260, 100); this.label4.Text = "Giờ khám";
            this.dtpGioKham.Format = System.Windows.Forms.DateTimePickerFormat.Time; this.dtpGioKham.ShowUpDown = true; this.dtpGioKham.Location = new System.Drawing.Point(320, 97); this.dtpGioKham.Size = new System.Drawing.Size(100, 23);

            this.label5.AutoSize = true; this.label5.Location = new System.Drawing.Point(440, 60); this.label5.Text = "Bác sĩ";
            this.cmbBacSi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbBacSi.Location = new System.Drawing.Point(440, 80); this.cmbBacSi.Size = new System.Drawing.Size(250, 23);

            this.label6.AutoSize = true; this.label6.Location = new System.Drawing.Point(710, 60); this.label6.Text = "Trạng thái";
            this.cmbTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbTrangThai.Items.AddRange(new object[] { "Chờ khám", "Đã khám", "Đã hủy" }); this.cmbTrangThai.Location = new System.Drawing.Point(710, 80); this.cmbTrangThai.Size = new System.Drawing.Size(120, 23);

            // Buttons
            this.btnThem.Location = new System.Drawing.Point(600, 17); this.btnThem.Text = "Thêm"; this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            this.btnSua.Location = new System.Drawing.Point(680, 17); this.btnSua.Text = "Sửa"; this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            this.btnXoa.Location = new System.Drawing.Point(760, 17); this.btnXoa.Text = "Xóa"; this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            this.btnLamMoi.Location = new System.Drawing.Point(840, 17); this.btnLamMoi.Text = "Làm mới"; this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // Search Filters
            this.label7.AutoSize = true; this.label7.Location = new System.Drawing.Point(20, 150); this.label7.Text = "Từ ngày";
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpTuNgay.Location = new System.Drawing.Point(80, 147); this.dtpTuNgay.Size = new System.Drawing.Size(120, 23);

            this.label8.AutoSize = true; this.label8.Location = new System.Drawing.Point(220, 150); this.label8.Text = "Đến ngày";
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpDenNgay.Location = new System.Drawing.Point(280, 147); this.dtpDenNgay.Size = new System.Drawing.Size(120, 23);

            this.cmbLocBacSi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbLocBacSi.Location = new System.Drawing.Point(440, 147); this.cmbLocBacSi.Size = new System.Drawing.Size(250, 23);
            this.btnTimKiem.Location = new System.Drawing.Point(710, 146); this.btnTimKiem.Text = "Tìm kiếm"; this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);

            // DataGridView
            this.dgvLichKham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichKham.Location = new System.Drawing.Point(20, 190); this.dgvLichKham.ReadOnly = true; this.dgvLichKham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; this.dgvLichKham.Size = new System.Drawing.Size(950, 300); this.dgvLichKham.SelectionChanged += new System.EventHandler(this.dgvLichKham_SelectionChanged);

            this.ClientSize = new System.Drawing.Size(1000, 520);
            this.Controls.Add(this.dgvLichKham); this.Controls.Add(this.btnTimKiem); this.Controls.Add(this.cmbLocBacSi); this.Controls.Add(this.dtpDenNgay); this.Controls.Add(this.label8); this.Controls.Add(this.dtpTuNgay); this.Controls.Add(this.label7); this.Controls.Add(this.btnLamMoi); this.Controls.Add(this.btnXoa); this.Controls.Add(this.btnSua); this.Controls.Add(this.btnThem); this.Controls.Add(this.cmbTrangThai); this.Controls.Add(this.label6); this.Controls.Add(this.cmbBacSi); this.Controls.Add(this.label5); this.Controls.Add(this.dtpGioKham); this.Controls.Add(this.label4); this.Controls.Add(this.dtpNgayKham); this.Controls.Add(this.label3); this.Controls.Add(this.txtSDT); this.Controls.Add(this.label2); this.Controls.Add(this.txtTenBN); this.Controls.Add(this.label1);
            this.Name = "Form1"; this.Text = "Quản Lý Lịch Khám Bệnh - An Khang Clinic"; this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichKham)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        private System.Windows.Forms.Label label1, label2, label3, label4, label5, label6, label7, label8;
        private System.Windows.Forms.TextBox txtTenBN, txtSDT;
        private System.Windows.Forms.DateTimePicker dtpNgayKham, dtpGioKham, dtpTuNgay, dtpDenNgay;
        private System.Windows.Forms.ComboBox cmbBacSi, cmbTrangThai, cmbLocBacSi;
        private System.Windows.Forms.Button btnThem, btnSua, btnXoa, btnLamMoi, btnTimKiem;
        private System.Windows.Forms.DataGridView dgvLichKham;
    }
}