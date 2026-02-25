using System.Collections.ObjectModel;
using System.Windows;

namespace QuanLyNhanVien
{
    public partial class dashboard : Window
    {
        public ObservableCollection<NhanVien> DsNhanVien { get; set; }

        public dashboard()
        {
            InitializeComponent();

            // Có thể để rỗng => vẫn hiện 3 nút Thêm/Sửa/Xóa bình thường
            DsNhanVien = new ObservableCollection<NhanVien>();

            // Nếu muốn test nhanh thì mở comment:
            /*
            DsNhanVien.Add(new NhanVien { MaNV="NV001", Ten="An", Tuoi=20, GioiTinh="Nam", Email="an@gmail.com", Sdt="0901234567" });
            DsNhanVien.Add(new NhanVien { MaNV="NV002", Ten="Bình", Tuoi=21, GioiTinh="Nữ", Email="binh@gmail.com", Sdt="0912345678" });
            */
            dgNhanVien.ItemsSource = DsNhanVien;
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bấm THÊM");
            // TODO: mở form thêm nhân viên
        }

        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            // Sửa theo dòng đang chọn
            if (dgNhanVien.SelectedItem is NhanVien nv)
                MessageBox.Show($"Bấm SỬA: {nv.MaNV} - {nv.Ten}");
            else
                MessageBox.Show("Hãy chọn 1 nhân viên để sửa.");
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            // Xóa theo dòng đang chọn
            if (dgNhanVien.SelectedItem is NhanVien nv)
            {
                var ok = MessageBox.Show($"Xóa nhân viên {nv.MaNV} - {nv.Ten} ?",
                                         "Xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (ok == MessageBoxResult.Yes)
                    DsNhanVien.Remove(nv);
            }
            else
            {
                MessageBox.Show("Hãy chọn 1 nhân viên để xóa.");
            }
        }
    }

   
}
