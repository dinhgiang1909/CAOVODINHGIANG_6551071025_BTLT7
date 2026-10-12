using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using CAU4.Models;

namespace CAU4
{
    public partial class Form1 : Form
    {
        private AnKhangClinicContext _context;
        private int _maLichDangChon = -1;

        public Form1()
        {
            InitializeComponent();
            var options = new DbContextOptionsBuilder<AnKhangClinicContext>();
            // Sử dụng đúng tên Server trên máy bạn
            options.UseSqlServer("Data Source=laptop-d5101ujq;Initial Catalog=AnKhangClinic;Integrated Security=True;TrustServerCertificate=True");
            _context = new AnKhangClinicContext(options.Options);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadData();
        }

        private void LoadComboBoxes()
        {
            var dsBacSi = _context.BacSis.Select(b => new
            {
                MaBs = b.MaBs,
                HienThi = $"BS. {b.HoTen} - {b.ChuyenKhoa}"
            }).ToList();

            cmbBacSi.DataSource = dsBacSi.ToList();
            cmbBacSi.DisplayMember = "HienThi";
            cmbBacSi.ValueMember = "MaBs";

            var dsLocBacSi = dsBacSi.ToList();
            dsLocBacSi.Insert(0, new { MaBs = 0, HienThi = "Tất cả bác sĩ" });
            cmbLocBacSi.DataSource = dsLocBacSi;
            cmbLocBacSi.DisplayMember = "HienThi";
            cmbLocBacSi.ValueMember = "MaBs";

            cmbTrangThai.SelectedIndex = 0;
            cmbLocBacSi.SelectedIndex = 0;
        }

        private void LoadData()
        {
            var query = _context.LichKhams.Include(l => l.MaBsNavigation).ToList();

            var dsLich = query.Select(l => new
            {
                MaLich = l.MaLich,
                TenBenhNhan = l.TenBenhNhan,
                SDT = l.Sdt,
                NgayKham = l.NgayKham.HasValue ? l.NgayKham.Value.ToString("dd/MM/yyyy") : "",
                GioKham = l.GioKham.HasValue ? l.GioKham.Value.ToString(@"HH\:mm") : "",
                BacSi = $"BS. {l.MaBsNavigation?.HoTen} - {l.MaBsNavigation?.ChuyenKhoa}",
                ChuyenKhoa = l.MaBsNavigation?.ChuyenKhoa,
                TrangThai = l.TrangThai,
                MaBS_Goc = l.MaBs
            }).ToList();

            dgvLichKham.DataSource = dsLich;
            dgvLichKham.Columns["MaBS_Goc"].Visible = false;
            dgvLichKham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvLichKham_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLichKham.CurrentRow != null && dgvLichKham.CurrentRow.Index >= 0)
            {
                var row = dgvLichKham.CurrentRow;
                _maLichDangChon = Convert.ToInt32(row.Cells["MaLich"].Value);

                txtTenBN.Text = row.Cells["TenBenhNhan"].Value?.ToString();
                txtSDT.Text = row.Cells["SDT"].Value?.ToString();
                cmbTrangThai.Text = row.Cells["TrangThai"].Value?.ToString();

                if (row.Cells["MaBS_Goc"].Value != null)
                {
                    cmbBacSi.SelectedValue = Convert.ToInt32(row.Cells["MaBS_Goc"].Value);
                }

                if (DateTime.TryParse(row.Cells["NgayKham"].Value?.ToString(), out DateTime ngay))
                    dtpNgayKham.Value = ngay;

                // Parse TimeOnly để tránh lỗi chuyển đổi ngược từ lưới dữ liệu
                if (TimeOnly.TryParse(row.Cells["GioKham"].Value?.ToString(), out TimeOnly gio))
                    dtpGioKham.Value = DateTime.Today.Add(gio.ToTimeSpan());
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenBN.Text))
            {
                MessageBox.Show("Tên bệnh nhân không được để trống!");
                return false;
            }
            if (dtpNgayKham.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Không thể đặt lịch khám vào ngày trong quá khứ!");
                return false;
            }
            if (cmbBacSi.SelectedValue == null || (int)cmbBacSi.SelectedValue == 0)
            {
                MessageBox.Show("Vui lòng chọn bác sĩ!");
                return false;
            }
            return true;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var lichMoi = new LichKham
            {
                TenBenhNhan = txtTenBN.Text.Trim(),
                Sdt = txtSDT.Text.Trim(),
                NgayKham = DateOnly.FromDateTime(dtpNgayKham.Value),
                GioKham = TimeOnly.FromDateTime(dtpGioKham.Value), // Sử dụng TimeOnly
                MaBs = (int)cmbBacSi.SelectedValue,
                TrangThai = cmbTrangThai.SelectedItem.ToString()
            };

            _context.LichKhams.Add(lichMoi);
            await _context.SaveChangesAsync();
            btnLamMoi_Click(sender, e);
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (_maLichDangChon == -1) return;
            if (!ValidateInput()) return;

            var lichSua = await _context.LichKhams.FindAsync(_maLichDangChon);
            if (lichSua != null)
            {
                lichSua.TenBenhNhan = txtTenBN.Text.Trim();
                lichSua.Sdt = txtSDT.Text.Trim();
                lichSua.NgayKham = DateOnly.FromDateTime(dtpNgayKham.Value);
                lichSua.GioKham = TimeOnly.FromDateTime(dtpGioKham.Value); // Sử dụng TimeOnly
                lichSua.MaBs = (int)cmbBacSi.SelectedValue;
                lichSua.TrangThai = cmbTrangThai.SelectedItem.ToString();

                await _context.SaveChangesAsync();
                LoadData();
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (_maLichDangChon == -1) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa lịch khám này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var lichXoa = await _context.LichKhams.FindAsync(_maLichDangChon);
                if (lichXoa != null)
                {
                    _context.LichKhams.Remove(lichXoa);
                    await _context.SaveChangesAsync();
                    btnLamMoi_Click(sender, e);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenBN.Clear();
            txtSDT.Clear();
            dtpNgayKham.Value = DateTime.Now;
            dtpGioKham.Value = DateTime.Now;
            if (cmbBacSi.Items.Count > 0) cmbBacSi.SelectedIndex = 0;
            cmbTrangThai.SelectedIndex = 0;

            _maLichDangChon = -1;
            dtpTuNgay.Value = DateTime.Now.AddDays(-7);
            dtpDenNgay.Value = DateTime.Now.AddDays(7);
            cmbLocBacSi.SelectedIndex = 0;
            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int maBsLoc = (int)cmbLocBacSi.SelectedValue;
            DateOnly tuNgay = DateOnly.FromDateTime(dtpTuNgay.Value);
            DateOnly denNgay = DateOnly.FromDateTime(dtpDenNgay.Value);

            var query = _context.LichKhams.Include(l => l.MaBsNavigation).AsQueryable();

            query = query.Where(l => l.NgayKham >= tuNgay && l.NgayKham <= denNgay);

            if (maBsLoc > 0)
            {
                query = query.Where(l => l.MaBs == maBsLoc);
            }

            var dsLoc = query.ToList().Select(l => new
            {
                MaLich = l.MaLich,
                TenBenhNhan = l.TenBenhNhan,
                SDT = l.Sdt,
                NgayKham = l.NgayKham.HasValue ? l.NgayKham.Value.ToString("dd/MM/yyyy") : "",
                GioKham = l.GioKham.HasValue ? l.GioKham.Value.ToString(@"HH\:mm") : "",
                BacSi = $"BS. {l.MaBsNavigation?.HoTen} - {l.MaBsNavigation?.ChuyenKhoa}",
                ChuyenKhoa = l.MaBsNavigation?.ChuyenKhoa,
                TrangThai = l.TrangThai,
                MaBS_Goc = l.MaBs
            }).ToList();

            dgvLichKham.DataSource = dsLoc;
        }
    }
}