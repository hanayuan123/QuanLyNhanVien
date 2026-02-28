using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyNhanVien
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnViTriPhongBan_Click(object sender, RoutedEventArgs e)
        {
            LoadWindowContent(new ViTriPhongBanWindow());
        }

        private void BtnQuanLyTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            LoadWindowContent(new QuanLyTaiKhoan_Main());
        }

        private void BtnQuanLyPhongBan_Click(object sender, RoutedEventArgs e)
        {
            LoadWindowContent(new QuanLyPhongBan());
        }

        private void BtnQuanLyNhanVien_Click(object sender, RoutedEventArgs e)
        {
            LoadWindowContent(new dashboard());
        }

        private void LoadWindowContent(Window childWindow)
        {
            var content = childWindow.Content as UIElement;

            if (content == null)
            {
                return;
            }

            MainContent.Resources = new ResourceDictionary();

            foreach (DictionaryEntry resource in childWindow.Resources)
            {
                MainContent.Resources[resource.Key] = resource.Value;
            }

            foreach (var mergedDictionary in childWindow.Resources.MergedDictionaries)
            {
                MainContent.Resources.MergedDictionaries.Add(mergedDictionary);
            }

            childWindow.Content = null;
            MainContent.Content = content;
        }
    }
}
