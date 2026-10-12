namespace CAU1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new System.Windows.Forms.Label();
            txtMaTL = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            txtTenTL = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            txtMoTa = new System.Windows.Forms.TextBox();
            btnThem = new System.Windows.Forms.Button();
            btnSua = new System.Windows.Forms.Button();
            btnXoa = new System.Windows.Forms.Button();
            btnLamMoi = new System.Windows.Forms.Button();
            txtTimKiem = new System.Windows.Forms.TextBox();
            btnTimKiem = new System.Windows.Forms.Button();
            dgvTheLoai = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvTheLoai).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(30, 30);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(68, 15);
            label1.TabIndex = 0;
            label1.Text = "Mã thể loại:";
            // 
            // txtMaTL
            // 
            txtMaTL.Location = new System.Drawing.Point(110, 27);
            txtMaTL.Name = "txtMaTL";
            txtMaTL.ReadOnly = true;
            txtMaTL.Size = new System.Drawing.Size(250, 23);
            txtMaTL.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(30, 70);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(69, 15);
            label2.TabIndex = 2;
            label2.Text = "Tên thể loại:";
            // 
            // txtTenTL
            // 
            txtTenTL.Location = new System.Drawing.Point(110, 67);
            txtTenTL.Name = "txtTenTL";
            txtTenTL.Size = new System.Drawing.Size(250, 23);
            txtTenTL.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(30, 110);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(41, 15);
            label3.TabIndex = 4;
            label3.Text = "Mô tả:";
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new System.Drawing.Point(110, 107);
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new System.Drawing.Size(250, 80);
            txtMoTa.TabIndex = 5;
            // 
            // btnThem
            // 
            btnThem.Location = new System.Drawing.Point(400, 27);
            btnThem.Name = "btnThem";
            btnThem.Size = new System.Drawing.Size(80, 30);
            btnThem.TabIndex = 6;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new System.Drawing.Point(500, 27);
            btnSua.Name = "btnSua";
            btnSua.Size = new System.Drawing.Size(80, 30);
            btnSua.TabIndex = 7;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new System.Drawing.Point(600, 27);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new System.Drawing.Size(80, 30);
            btnXoa.TabIndex = 8;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new System.Drawing.Point(700, 27);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new System.Drawing.Size(80, 30);
            btnLamMoi.TabIndex = 9;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new System.Drawing.Point(30, 212);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new System.Drawing.Size(330, 23);
            txtTimKiem.TabIndex = 10;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new System.Drawing.Point(380, 208);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new System.Drawing.Size(100, 30);
            btnTimKiem.TabIndex = 11;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // dgvTheLoai
            // 
            dgvTheLoai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTheLoai.Location = new System.Drawing.Point(30, 260);
            dgvTheLoai.Name = "dgvTheLoai";
            dgvTheLoai.ReadOnly = true;
            dgvTheLoai.RowTemplate.Height = 25;
            dgvTheLoai.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvTheLoai.Size = new System.Drawing.Size(750, 250);
            dgvTheLoai.TabIndex = 12;
            dgvTheLoai.SelectionChanged += dgvTheLoai_SelectionChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(820, 540);
            Controls.Add(dgvTheLoai);
            Controls.Add(btnTimKiem);
            Controls.Add(txtTimKiem);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(txtMoTa);
            Controls.Add(label3);
            Controls.Add(txtTenTL);
            Controls.Add(label2);
            Controls.Add(txtMaTL);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Quản Lý Thể Loại Sách - Tri Thức Books";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTheLoai).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaTL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenTL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvTheLoai;
    }
}