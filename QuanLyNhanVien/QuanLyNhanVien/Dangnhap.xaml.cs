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
    /// Interaction logic for Dangnhap.xaml
    /// </summary>
    public partial class Dangnhap : Window
    {
        private const string DemoUsername = "123456";
        private const string DemoPassword = "123456";

        public Dangnhap()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text == DemoUsername && txtPass.Password == DemoPassword)
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
                return;
            }

            MessageBox.Show("Sai tài khoản hoặc mật khẩu.", "Đăng nhập", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
