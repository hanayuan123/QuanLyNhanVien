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
    /// Interaction logic for QuanLyTaiKhoan_Main.xaml
    /// </summary>
    public partial class QuanLyTaiKhoan_Main : Window
    {
        public QuanLyTaiKhoan_Main()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new QuanLyTaiKhoan_Them();
            addWindow.Show();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
