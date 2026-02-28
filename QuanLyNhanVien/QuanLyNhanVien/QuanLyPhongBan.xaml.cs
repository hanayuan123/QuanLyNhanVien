using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyNhanVien
{
    public partial class QuanLyPhongBan : Window
    {
        // ⚠️ Dùng đúng tên DbContext của bạn
        QLNhanVienEntities db = new QLNhanVienEntities();

        public QuanLyPhongBan()
        {
            InitializeComponent();
            LoadData();
        }

        // ================= LOAD DATA =================
        private void LoadData()
        {
            dgPhongBan.ItemsSource = db.PhongBans.ToList();
        }

        // ================= THÊM =================
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPB.Text) ||
                string.IsNullOrWhiteSpace(txtTenPB.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            var check = db.PhongBans.Find(txtMaPB.Text);
            if (check != null)
            {
                MessageBox.Show("Mã phòng ban đã tồn tại!");
                return;
            }

            PhongBan pb = new PhongBan
            {
                MaPB = txtMaPB.Text,
                TenPB = txtTenPB.Text,
                MoTa = txtMoTa.Text
            };

            db.PhongBans.Add(pb);
            db.SaveChanges();

            LoadData();
            ClearForm();

            MessageBox.Show("Thêm thành công!");
        }

        // ================= SỬA =================
        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            var pb = db.PhongBans.Find(txtMaPB.Text);

            if (pb == null)
            {
                MessageBox.Show("Không tìm thấy phòng ban!");
                return;
            }

            pb.TenPB = txtTenPB.Text;
            pb.MoTa = txtMoTa.Text;

            db.SaveChanges();
            LoadData();

            MessageBox.Show("Cập nhật thành công!");
        }

        // ================= XÓA =================
        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            var pb = db.PhongBans.Find(txtMaPB.Text);

            if (pb == null)
            {
                MessageBox.Show("Không tìm thấy phòng ban!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.PhongBans.Remove(pb);
                db.SaveChanges();
                LoadData();
                ClearForm();

                MessageBox.Show("Xóa thành công!");
            }
        }

        // ================= CHỌN DÒNG =================
        private void dgPhongBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPhongBan.SelectedItem is PhongBan pb)
            {
                txtMaPB.Text = pb.MaPB;
                txtTenPB.Text = pb.TenPB;
                txtMoTa.Text = pb.MoTa;
            }
        }

        // ================= CLEAR =================
        private void ClearForm()
        {
            txtMaPB.Clear();
            txtTenPB.Clear();
            txtMoTa.Clear();
        }
    }
}