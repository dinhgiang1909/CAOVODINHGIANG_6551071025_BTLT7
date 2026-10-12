using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
// Khai báo thư mục Models trong project CAU1 của bạn
using CAU1.Models;

namespace CAU1
{
    public partial class Form1 : Form
    {
        // Khai báo biến context để tương tác với Database
        private TriThucBooksContext _context;

        public Form1()
        {
            InitializeComponent();
            // Khởi tạo DbContextOptionsBuilder
            var optionsBuilder = new DbContextOptionsBuilder<TriThucBooksContext>();
            optionsBuilder.UseSqlServer("Data Source=laptop-d5101ujq;Initial Catalog=TriThucBooks;Integrated Security=True;TrustServerCertificate=True");
            _context = new TriThucBooksContext(optionsBuilder.Options);
        }

        // Sự kiện Load Form (Click đúp vào tiêu đề Form để sinh ra sự kiện này)
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Hàm Tải dữ liệu lên DataGridView
        private void LoadData()
        {
            var danhSach = _context.TheLoaiSaches
                .Select(tl => new
                {
                    tl.MaTl, // EF Core thường gen MaTL trong SQL thành MaTl
                    tl.TenTheLoai,
                    tl.MoTa,
                    tl.SoLuongSach,
                    tl.NgayTao
                }).ToList();

            dgvTheLoai.DataSource = danhSach;

            // Format DataGridView giống ảnh image_a703fd.png
            if (dgvTheLoai.Columns.Count > 0)
            {
                dgvTheLoai.Columns["MaTl"].HeaderText = "Mã TL";
                dgvTheLoai.Columns["TenTheLoai"].HeaderText = "Tên thể loại";
                dgvTheLoai.Columns["MoTa"].HeaderText = "Mô tả";
                dgvTheLoai.Columns["SoLuongSach"].HeaderText = "Số lượng sách";
                dgvTheLoai.Columns["NgayTao"].HeaderText = "Ngày tạo";

                dgvTheLoai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // Sự kiện SelectionChanged của DataGridView (Click đúp vào event SelectionChanged trong Properties)
        private void dgvTheLoai_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTheLoai.CurrentRow != null && dgvTheLoai.CurrentRow.Index >= 0)
            {
                DataGridViewRow row = dgvTheLoai.CurrentRow;
                txtMaTL.Text = row.Cells["MaTl"].Value?.ToString();
                txtTenTL.Text = row.Cells["TenTheLoai"].Value?.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();
            }
        }

        // Nút LÀM MỚI
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaTL.Clear();
            txtTenTL.Clear();
            txtMoTa.Clear();
            txtTimKiem.Clear();
            LoadData();
        }

        // Nút THÊM
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Validate: Không được bỏ trống Tên thể loại
            if (string.IsNullOrWhiteSpace(txtTenTL.Text))
            {
                MessageBox.Show("Tên thể loại không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate: Không trùng Tên thể loại
            string tenMoi = txtTenTL.Text.Trim();
            bool isExist = _context.TheLoaiSaches.Any(tl => tl.TenTheLoai.ToLower() == tenMoi.ToLower());
            if (isExist)
            {
                MessageBox.Show("Tên thể loại này đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thêm mới
            var theLoaiMoi = new TheLoaiSach
            {
                TenTheLoai = tenMoi,
                MoTa = txtMoTa.Text.Trim(),
                SoLuongSach = 0,
                NgayTao = DateTime.Now
            };

            _context.TheLoaiSaches.Add(theLoaiMoi);
            _context.SaveChanges();

            MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLamMoi_Click(sender, e); // Vừa làm sạch form vừa load lại data
        }

        // Nút SỬA
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTL.Text))
            {
                MessageBox.Show("Vui lòng chọn một dòng thể loại trên lưới để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenTL.Text))
            {
                MessageBox.Show("Tên thể loại không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int id = int.Parse(txtMaTL.Text);
            var theLoaiSua = _context.TheLoaiSaches.Find(id);

            if (theLoaiSua != null)
            {
                string tenMoi = txtTenTL.Text.Trim();
                // Kiểm tra trùng tên với các thể loại khác (nhưng bỏ qua chính nó)
                bool isExist = _context.TheLoaiSaches.Any(tl => tl.TenTheLoai.ToLower() == tenMoi.ToLower() && tl.MaTl != id);

                if (isExist)
                {
                    MessageBox.Show("Tên thể loại này đã bị trùng với một bản ghi khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                theLoaiSua.TenTheLoai = tenMoi;
                theLoaiSua.MoTa = txtMoTa.Text.Trim();

                _context.SaveChanges();
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // Cập nhật lại Grid
            }
        }

        // Nút XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTL.Text))
            {
                MessageBox.Show("Vui lòng chọn một dòng thể loại để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa thể loại '{txtTenTL.Text}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    int id = int.Parse(txtMaTL.Text);
                    var theLoaiXoa = _context.TheLoaiSaches.Find(id);

                    if (theLoaiXoa != null)
                    {
                        _context.TheLoaiSaches.Remove(theLoaiXoa);
                        _context.SaveChanges();

                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnLamMoi_Click(sender, e);
                    }
                }
                catch (DbUpdateException)
                {
                    // Bắt lỗi ràng buộc khóa ngoại (Rất quan trọng trong Database First)
                    MessageBox.Show("Không thể xóa thể loại này vì đang có sách tham chiếu đến nó!", "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Nút TÌM KIẾM
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();

            // LINQ (Where + Contains)
            var danhSachLoc = _context.TheLoaiSaches
                .Where(tl => tl.TenTheLoai.ToLower().Contains(tuKhoa))
                .Select(tl => new
                {
                    tl.MaTl,
                    tl.TenTheLoai,
                    tl.MoTa,
                    tl.SoLuongSach,
                    tl.NgayTao
                }).ToList();

            dgvTheLoai.DataSource = danhSachLoc;
        }
    }
}