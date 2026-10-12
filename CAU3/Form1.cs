using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using CAU3.Models; // Thay bằng namespace model thực tế của bạn

namespace CAU3
{
    public partial class Form1 : Form
    {
        private SunriseHomestayContext _context;
        private string _thuMucAnh;
        private string _tenFileAnhChon = "";
        private string _duongDanAnhGoc = "";
        private int _maPhongDangChon = -1;

        public Form1()
        {
            InitializeComponent();

            // Khởi tạo DbContextOptionsBuilder và cấu hình chuỗi kết nối
            var optionsBuilder = new DbContextOptionsBuilder<SunriseHomestayContext>();
            optionsBuilder.UseSqlServer("Data Source=laptop-d5101ujq;Initial Catalog=SunriseHomestay;Integrated Security=True;TrustServerCertificate=True");

            _context = new SunriseHomestayContext(optionsBuilder.Options);

            _thuMucAnh = Path.Combine(Application.StartupPath, "Images");
            if (!Directory.Exists(_thuMucAnh))
            {
                Directory.CreateDirectory(_thuMucAnh);
            }
        }

        private void FormChinh_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadData();
        }

        private void LoadComboBoxes()
        {
            // Nạp dữ liệu Loại Phòng cho ComboBox Nhập liệu
            var dsLoaiPhong = _context.LoaiPhongs.ToList();
            cmbLoaiPhong.DataSource = dsLoaiPhong;
            cmbLoaiPhong.DisplayMember = "TenLoai";
            cmbLoaiPhong.ValueMember = "MaLoai";

            // Nạp dữ liệu Loại Phòng cho ComboBox Lọc (Thêm "Tất cả")
            var dsLocLoai = dsLoaiPhong.ToList();
            dsLocLoai.Insert(0, new LoaiPhong { MaLoai = 0, TenLoai = "Tất cả" });
            cmbLocLoaiPhong.DataSource = dsLocLoai;
            cmbLocLoaiPhong.DisplayMember = "TenLoai";
            cmbLocLoaiPhong.ValueMember = "MaLoai";

            cmbTinhTrang.SelectedIndex = 0;
            cmbLocTinhTrang.SelectedIndex = 0;
        }

        // Hàm đọc ảnh từ file thành đối tượng Image (Chống lỗi khóa file khi Xóa/Ghi đè)
        private Image LayAnhTuFile(string tenFile)
        {
            if (string.IsNullOrEmpty(tenFile)) return null;
            string duongDan = Path.Combine(_thuMucAnh, tenFile);

            if (File.Exists(duongDan))
            {
                using (var stream = new MemoryStream(File.ReadAllBytes(duongDan)))
                {
                    return Image.FromStream(stream);
                }
            }
            return null;
        }

        private void LoadData()
        {
            // Dùng Eager Loading (Include) để lấy thông tin bảng LoaiPhong
            var query = _context.Phongs.Include(p => p.MaLoaiNavigation).ToList();

            var dsPhong = query.Select(p => new
            {
                MaPhong = p.MaPhong,
                Anh = LayAnhTuFile(p.HinhAnh), // Cột này sẽ tự động thành DataGridViewImageColumn
                SoPhong = p.SoPhong,
                Tang = p.TangSo,
                LoaiPhong = p.MaLoaiNavigation?.TenLoai,
                GiaDem = p.MaLoaiNavigation?.GiaMoiDem,
                TinhTrang = p.TinhTrang,
                TenFileAnh = p.HinhAnh // Cột ẩn dùng để xử lý dữ liệu
            }).ToList();

            dgvPhong.DataSource = dsPhong;

            if (dgvPhong.Columns.Count > 0)
            {
                dgvPhong.Columns["TenFileAnh"].Visible = false;
                dgvPhong.Columns["MaPhong"].HeaderText = "Mã";
                dgvPhong.Columns["MaPhong"].Width = 40;

                // Định dạng cột Ảnh
                var colAnh = (DataGridViewImageColumn)dgvPhong.Columns["Anh"];
                colAnh.ImageLayout = DataGridViewImageCellLayout.Zoom;
                colAnh.Width = 100;

                dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPhong.RowTemplate.Height = 80;
            }
        }

        private void dgvPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhong.CurrentRow != null && dgvPhong.CurrentRow.Index >= 0)
            {
                var row = dgvPhong.CurrentRow;
                _maPhongDangChon = Convert.ToInt32(row.Cells["MaPhong"].Value);

                txtSoPhong.Text = row.Cells["SoPhong"].Value?.ToString();
                numTangSo.Value = Convert.ToDecimal(row.Cells["Tang"].Value);
                cmbLoaiPhong.Text = row.Cells["LoaiPhong"].Value?.ToString();
                cmbTinhTrang.Text = row.Cells["TinhTrang"].Value?.ToString();

                _tenFileAnhChon = row.Cells["TenFileAnh"].Value?.ToString() ?? "";
                picHinhAnh.Image = LayAnhTuFile(_tenFileAnhChon);
                _duongDanAnhGoc = ""; // Reset đường dẫn file khi chọn dòng có sẵn
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png",
                Title = "Chọn ảnh phòng"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _duongDanAnhGoc = ofd.FileName;
                _tenFileAnhChon = Path.GetFileName(_duongDanAnhGoc);
                picHinhAnh.Image = Image.FromFile(_duongDanAnhGoc);
            }
        }

        // Hàm hỗ trợ copy ảnh vào thư mục hệ thống
        private void LuuFileAnh()
        {
            if (!string.IsNullOrEmpty(_duongDanAnhGoc) && File.Exists(_duongDanAnhGoc))
            {
                string duongDanMoi = Path.Combine(_thuMucAnh, _tenFileAnhChon);
                // Nếu file đã tồn tại thì ghi đè
                if (_duongDanAnhGoc != duongDanMoi)
                {
                    File.Copy(_duongDanAnhGoc, duongDanMoi, true);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoPhong.Text)) return;

            LuuFileAnh(); // Copy ảnh vào thư mục Images

            var phongMoi = new Phong
            {
                SoPhong = txtSoPhong.Text.Trim(),
                TangSo = (int)numTangSo.Value,
                TinhTrang = cmbTinhTrang.SelectedItem.ToString(),
                HinhAnh = _tenFileAnhChon,
                MaLoai = (int)cmbLoaiPhong.SelectedValue
            };

            _context.Phongs.Add(phongMoi);
            _context.SaveChanges();

            MessageBox.Show("Thêm phòng thành công!");
            btnLamMoi_Click(sender, e);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_maPhongDangChon == -1) return;

            var phongSua = _context.Phongs.Find(_maPhongDangChon);
            if (phongSua != null)
            {
                LuuFileAnh();

                phongSua.SoPhong = txtSoPhong.Text.Trim();
                phongSua.TangSo = (int)numTangSo.Value;
                phongSua.TinhTrang = cmbTinhTrang.SelectedItem.ToString();
                phongSua.HinhAnh = _tenFileAnhChon;
                phongSua.MaLoai = (int)cmbLoaiPhong.SelectedValue;

                _context.SaveChanges();
                MessageBox.Show("Cập nhật phòng thành công!");
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_maPhongDangChon == -1) return;

            if (MessageBox.Show("Xóa phòng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var phongXoa = _context.Phongs.Find(_maPhongDangChon);
                if (phongXoa != null)
                {
                    _context.Phongs.Remove(phongXoa);
                    _context.SaveChanges();
                    btnLamMoi_Click(sender, e);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSoPhong.Clear();
            numTangSo.Value = 1;
            cmbLoaiPhong.SelectedIndex = 0;
            cmbTinhTrang.SelectedIndex = 0;

            picHinhAnh.Image = null;
            _tenFileAnhChon = "";
            _duongDanAnhGoc = "";
            _maPhongDangChon = -1;

            cmbLocLoaiPhong.SelectedIndex = 0;
            cmbLocTinhTrang.SelectedIndex = 0;

            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            int maLoaiLoc = (int)cmbLocLoaiPhong.SelectedValue;
            string tinhTrangLoc = cmbLocTinhTrang.SelectedItem?.ToString();

            // Kết hợp nhiều điều kiện với Include
            var query = _context.Phongs.Include(p => p.MaLoaiNavigation).AsQueryable();

            if (maLoaiLoc > 0)
            {
                query = query.Where(p => p.MaLoai == maLoaiLoc);
            }

            if (tinhTrangLoc != "Tất cả" && !string.IsNullOrEmpty(tinhTrangLoc))
            {
                query = query.Where(p => p.TinhTrang == tinhTrangLoc);
            }

            var dsLoc = query.ToList().Select(p => new
            {
                MaPhong = p.MaPhong,
                Anh = LayAnhTuFile(p.HinhAnh),
                SoPhong = p.SoPhong,
                Tang = p.TangSo,
                LoaiPhong = p.MaLoaiNavigation?.TenLoai,
                GiaDem = p.MaLoaiNavigation?.GiaMoiDem,
                TinhTrang = p.TinhTrang,
                TenFileAnh = p.HinhAnh
            }).ToList();

            dgvPhong.DataSource = dsLoc;
        }
    }
}