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
    /// Interaction logic for QuanLyTaiKhoan_Sua.xaml
    /// </summary>
    public partial class QuanLyTaiKhoan_Sua : UserControl
    {
        public event Action OnSaved;
        private QuanLyNhanVien.QLNhanVienEntities2 db = new QuanLyNhanVien.QLNhanVienEntities2();
        private int _id;
        public QuanLyTaiKhoan_Sua(int id)
        {
            InitializeComponent();
            _id = id;
            LoadData();
        }

        private void LoadData()
        {
            var tk = db.TaiKhoans.Find(_id);

            if (tk != null)
            {
                IdBox.Text = tk.Id.ToString();
                UsernameBox.Text = tk.Username;
                PasswordBox.Password = tk.Password;
                RoleBox.Text = tk.Role;
                ManvBox.Text = tk.MaNV;
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var tk = db.TaiKhoans.Find(_id);

            if (tk != null)
            {
                tk.Username = UsernameBox.Text;
                tk.Password = PasswordBox.Password;
                tk.Role = RoleBox.Text;
                tk.MaNV = ManvBox.Text;

                db.SaveChanges();

                MessageBox.Show("Cập nhật thành công!");
                OnSaved?.Invoke();
            }
        }
    }
}
