using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyNhanVien
{
    public partial class ViTriPhongBanWindow : Window
    {
        QLNhanVienEntities db = new QLNhanVienEntities();

        public ViTriPhongBanWindow()
        {
            InitializeComponent();
            LoadData();
            dgViTri.SelectionChanged += dgViTri_SelectionChanged;
        }

        void LoadData()
        {
            dgViTri.ItemsSource = db.ViTriPhongBans.ToList();
        }

        private void dgViTri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgViTri.SelectedItem == null) return;

            var vt = dgViTri.SelectedItem as QuanLyNhanVien.ViTriPhongBan;

            if (vt != null)
            {
                txtMaVT.Text = vt.MaVT;
                txtTenVT.Text = vt.TenVT;
                txtMaPB.Text = vt.MaPB;
            }
        }

        private void Them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QuanLyNhanVien.ViTriPhongBan vt = new QuanLyNhanVien.ViTriPhongBan();
                vt.MaVT = txtMaVT.Text.Trim();
                vt.TenVT = txtTenVT.Text.Trim();
                vt.MaPB = txtMaPB.Text.Trim();

                db.ViTriPhongBans.Add(vt);
                db.SaveChanges();

                LoadData();
                MessageBox.Show("Thêm thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Sua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vt = db.ViTriPhongBans.Find(txtMaVT.Text.Trim());
                if (vt == null)
                {
                    MessageBox.Show("Không tìm thấy mã vị trí!");
                    return;
                }

                vt.TenVT = txtTenVT.Text.Trim();
                vt.MaPB = txtMaPB.Text.Trim();

                db.SaveChanges();
                LoadData();

                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Xoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vt = db.ViTriPhongBans.Find(txtMaVT.Text.Trim());
                if (vt == null)
                {
                    MessageBox.Show("Không tìm thấy để xóa!");
                    return;
                }

                db.ViTriPhongBans.Remove(vt);
                db.SaveChanges();

                LoadData();
                MessageBox.Show("Xóa thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa (có thể đang liên kết dữ liệu).");
            }
        }
    }
}
