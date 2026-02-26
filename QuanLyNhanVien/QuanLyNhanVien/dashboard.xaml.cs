using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace QuanLyNhanVien
{
    public partial class dashboard : Window
    {
        public ObservableCollection<NhanVien> DsNhanVien { get; set; }

        public dashboard()
        {
            InitializeComponent();
            LoadNhanVien();
        }

        private void LoadNhanVien()
        {
            using (var db = new QLNhanVienEntities())
            {
                var list = db.NhanViens.ToList();
                DsNhanVien = new ObservableCollection<NhanVien>(list);
            }
            dgNhanVien.ItemsSource = DsNhanVien;
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            var addForm = new AddForm();
            addForm.OnQuayLai = () =>
            {
                contentArea.Content = null;
                contentArea.Visibility = Visibility.Collapsed;
                pnlDanhSach.Visibility = Visibility.Visible;
                LoadNhanVien();
            };
            contentArea.Content = addForm;
            pnlDanhSach.Visibility = Visibility.Collapsed;
            contentArea.Visibility = Visibility.Visible;
        }

        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            if (dgNhanVien.SelectedItem is NhanVien nv)
            {
                var editForm = new EditForm();
                editForm.LoadNhanVien(nv);
                editForm.OnQuayLai = () =>
                {
                    contentArea.Content = null;
                    contentArea.Visibility = Visibility.Collapsed;
                    pnlDanhSach.Visibility = Visibility.Visible;
                    LoadNhanVien();
                };
                contentArea.Content = editForm;
                pnlDanhSach.Visibility = Visibility.Collapsed;
                contentArea.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Hãy chọn 1 nhân viên để sửa.");
            }
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (dgNhanVien.SelectedItem is NhanVien nv)
            {
                var ok = MessageBox.Show($"Bạn có muốn xóa nhân viên {nv.MaNV} - {nv.Ten} không?",
                                         "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (ok == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (var db = new QLNhanVienEntities())
                        {
                            var entity = db.NhanViens.Find(nv.MaNV);
                            if (entity != null)
                            {
                                db.Luongs.RemoveRange(entity.Luongs);
                                db.TaiKhoans.RemoveRange(entity.TaiKhoans);
                                db.NhanViens.Remove(entity);
                                db.SaveChanges();
                            }
                        }
                        DsNhanVien.Remove(nv);
                        MessageBox.Show("Xóa nhân viên thành công!",
                            "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        MessageBox.Show("Lỗi khi xóa nhân viên:\n" + msg,
                            "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn 1 nhân viên để xóa.");
            }
        }
    }

   
}
