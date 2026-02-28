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
    /// <summary>
    /// Interaction logic for QuanLyTaiKhoan_Them.xaml
    /// </summary>
    public partial class QuanLyTaiKhoan_Them : UserControl
    {
        public event Action OnSaved;
        private QuanLyNhanVien.QLNhanVienEntities db = new QuanLyNhanVien.QLNhanVienEntities();
        public QuanLyTaiKhoan_Them()
        {
            InitializeComponent();
            LoadNhanVien();
        }

        private void LoadNhanVien()
        {
            ManvBox.ItemsSource = db.NhanViens.Select(nv => nv.MaNV).ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra dữ liệu cơ bản
            if (string.IsNullOrWhiteSpace(UsernameBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password) ||
                ManvBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            string maNV = ManvBox.SelectedItem.ToString();

            TaiKhoan tk = new TaiKhoan
            {
                Username = UsernameBox.Text,
                Password = PasswordBox.Password,
                Role = RoleBox.Text,
                MaNV = maNV
            };

            db.TaiKhoans.Add(tk);
            db.SaveChanges();

            MessageBox.Show("Thêm tài khoản thành công!");

            OnSaved?.Invoke();   // báo về Main
        }
    }
}
