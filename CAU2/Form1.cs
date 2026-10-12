using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using CAU2.Models; // Đảm bảo đúng namespace sinh ra từ EF Core

namespace CAU2
{
    public partial class Form1 : Form
    {
        private FitZoneContext _context;
        private int _maHoiVienDangChon = -1;

        public Form1()
        {
            InitializeComponent();

            var optionsBuilder = new DbContextOptionsBuilder<FitZoneContext>();
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-D5101UJQ;Initial Catalog=FitZone;Integrated Security=True;TrustServerCertificate=True");
            _context = new FitZoneContext(optionsBuilder.Options);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbHangThanhVien.SelectedIndex = 0;
            cmbTimKiemHang.SelectedIndex = 0;
            LoadData();
        }

        private void LoadData()
        {
            var dsHoiVien = _context.HoiViens.Select(hv => new
            {
                hv.MaHv,
                hv.HoTen,
                GioiTinh = hv.GioiTinh == true ? "Nam" : "Nữ",
                NgaySinh = hv.NgaySinh.HasValue ? hv.NgaySinh.Value.ToString("dd/MM/yyyy") : "",
                hv.Sdt,
                hv.Email,
                hv.HangThanhVien,
                TrangThai = hv.TrangThai == true ? "Đang hoạt động" : "Tạm ngưng"
            }).ToList();

            dgvHoiVien.DataSource = dsHoiVien;

            if (dgvHoiVien.Columns.Count > 0)
            {
                dgvHoiVien.Columns["MaHv"].Visible = false; // Ẩn cột ID
                dgvHoiVien.Columns["HoTen"].HeaderText = "Họ tên";
                dgvHoiVien.Columns["GioiTinh"].HeaderText = "Giới tính";
                dgvHoiVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                dgvHoiVien.Columns["Sdt"].HeaderText = "SĐT";
                dgvHoiVien.Columns["HangThanhVien"].HeaderText = "Hạng thành viên";
                dgvHoiVien.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvHoiVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void dgvHoiVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoiVien.CurrentRow != null && dgvHoiVien.CurrentRow.Index >= 0)
            {
                _maHoiVienDangChon = Convert.ToInt32(dgvHoiVien.CurrentRow.Cells["MaHv"].Value);
                var hv = _context.HoiViens.Find(_maHoiVienDangChon);

                if (hv != null)
                {
                    txtHoTen.Text = hv.HoTen;
                    txtSDT.Text = hv.Sdt;
                    txtEmail.Text = hv.Email;

                    if (hv.GioiTinh == true) radNam.Checked = true;
                    else radNu.Checked = true;

                    if (hv.NgaySinh.HasValue) dtpNgaySinh.Value = hv.NgaySinh.Value.ToDateTime(TimeOnly.MinValue);

                    cmbHangThanhVien.SelectedItem = hv.HangThanhVien;
                    chkTrangThai.Checked = hv.TrangThai ?? false;
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.IsMatch(txtSDT.Text.Trim(), @"^\d{9,11}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ (phải là số và từ 9-11 ký tự)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email phải chứa ký tự '@'!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int age = DateTime.Now.Year - dtpNgaySinh.Value.Year;
            if (dtpNgaySinh.Value.Date > DateTime.Now.AddYears(-age)) age--; // Trừ 1 nếu chưa tới sinh nhật trong năm nay

            if (age < 15)
            {
                MessageBox.Show("Hội viên phải từ 15 tuổi trở lên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var hvMoi = new HoiVien
            {
                HoTen = txtHoTen.Text.Trim(),
                GioiTinh = radNam.Checked,
                NgaySinh = DateOnly.FromDateTime(dtpNgaySinh.Value),
                Sdt = txtSDT.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                HangThanhVien = cmbHangThanhVien.SelectedItem.ToString(),
                TrangThai = chkTrangThai.Checked,
                NgayDangKy = DateTime.Now
            };

            _context.HoiViens.Add(hvMoi);
            await _context.SaveChangesAsync();

            MessageBox.Show("Thêm hội viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLamMoi_Click(sender, e);
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (_maHoiVienDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn hội viên cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            var hvSua = await _context.HoiViens.FindAsync(_maHoiVienDangChon);
            if (hvSua != null)
            {
                hvSua.HoTen = txtHoTen.Text.Trim();
                hvSua.GioiTinh = radNam.Checked;
                hvSua.NgaySinh = DateOnly.FromDateTime(dtpNgaySinh.Value);
                hvSua.Sdt = txtSDT.Text.Trim();
                hvSua.Email = txtEmail.Text.Trim();
                hvSua.HangThanhVien = cmbHangThanhVien.SelectedItem.ToString();
                hvSua.TrangThai = chkTrangThai.Checked;

                await _context.SaveChangesAsync();
                MessageBox.Show("Cập nhật hội viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (_maHoiVienDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn hội viên cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dialogResult = MessageBox.Show($"Bạn có chắc muốn xóa hội viên '{txtHoTen.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                var hvXoa = await _context.HoiViens.FindAsync(_maHoiVienDangChon);
                if (hvXoa != null)
                {
                    _context.HoiViens.Remove(hvXoa);
                    await _context.SaveChangesAsync();

                    MessageBox.Show("Đã xóa hội viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLamMoi_Click(sender, e);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            radNam.Checked = true;
            dtpNgaySinh.Value = DateTime.Now.AddYears(-15);
            cmbHangThanhVien.SelectedIndex = 0;
            chkTrangThai.Checked = true;

            txtTimKiemTen.Clear();
            cmbTimKiemHang.SelectedIndex = 0;
            _maHoiVienDangChon = -1;

            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiemTen.Text.Trim().ToLower();
            string hangChon = cmbTimKiemHang.SelectedItem?.ToString();

            // Lọc nhiều điều kiện kết hợp toán tử &&
            var query = _context.HoiViens.AsQueryable();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(hv => hv.HoTen.ToLower().Contains(tuKhoa));
            }

            if (hangChon != "Tất cả" && !string.IsNullOrEmpty(hangChon))
            {
                query = query.Where(hv => hv.HangThanhVien == hangChon);
            }

            var danhSachLoc = query.Select(hv => new
            {
                hv.MaHv,
                hv.HoTen,
                GioiTinh = hv.GioiTinh == true ? "Nam" : "Nữ",
                NgaySinh = hv.NgaySinh.HasValue ? hv.NgaySinh.Value.ToString("dd/MM/yyyy") : "",
                hv.Sdt,
                hv.Email,
                hv.HangThanhVien,
                TrangThai = hv.TrangThai == true ? "Đang hoạt động" : "Tạm ngưng"
            }).ToList();

            dgvHoiVien.DataSource = danhSachLoc;
        }
    }
}