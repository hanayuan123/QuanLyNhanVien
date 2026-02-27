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
        QuanLyNhanVien.QLNhanVienEntities2 db = new QuanLyNhanVien.QLNhanVienEntities2();

        public class TaiKhoanView
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Role { get; set; }
            public string TenNhanVien { get; set; }
        }
        public QuanLyTaiKhoan_Main()
        {
            InitializeComponent();
            LoadData();
        }

        private TaiKhoanView GetSelected()
        {
            return AccountDataGrid.SelectedItem as TaiKhoanView;
        }

        private void LoadData()
        {
            var data = db.TaiKhoans
                         .Select(t => new TaiKhoanView
                         {
                             Id = t.Id,
                             Username = t.Username,
                             Role = t.Role,
                             TenNhanVien = t.NhanVien.Ten
                         })
                         .ToList();

            AccountDataGrid.ItemsSource = data;
        }



        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var form = new QuanLyTaiKhoan_Them();

            form.OnSaved += () =>
            {
                LoadData();              // reload DataGrid
                FormContent.Content = null;  // đóng form
            };

            FormContent.Content = form;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selected = GetSelected();

            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!");
                return;
            }

            var form = new QuanLyTaiKhoan_Sua(selected.Id);

            form.OnSaved += () =>
            {
                LoadData();
                FormContent.Content = null;
            };

            FormContent.Content = form;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selected = GetSelected();

            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!");
                return;
            }

            var tk = db.TaiKhoans.Find(selected.Id);

            db.TaiKhoans.Remove(tk);
            db.SaveChanges();

            LoadData();
        }
    }
}
