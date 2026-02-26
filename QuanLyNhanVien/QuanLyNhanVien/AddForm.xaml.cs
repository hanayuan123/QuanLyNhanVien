using System;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyNhanVien
{
    public partial class AddForm : UserControl
    {
        public Action OnQuayLai { get; set; }

        public AddForm()
        {
            InitializeComponent();
        }

        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn lưu thông tin nhân viên không?",
                "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result != MessageBoxResult.OK)
                return;

            if (string.IsNullOrWhiteSpace(txtMaNV.Text) ||
                string.IsNullOrWhiteSpace(txtTen.Text) ||
                string.IsNullOrWhiteSpace(txtTuoi.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                (rbNam.IsChecked != true && rbNu.IsChecked != true))
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin nhân viên",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var nv = new NhanVien
            {
                MaNV = txtMaNV.Text.Trim(),
                Ten = txtTen.Text.Trim(),
                Tuoi = int.TryParse(txtTuoi.Text.Trim(), out int tuoi) ? tuoi : (int?)null,
                GioiTinh = rbNam.IsChecked == true ? "Nam" : "Nữ",
                Email = txtEmail.Text.Trim(),
                SDT = txtSDT.Text.Trim()
            };

            try
            {
                using (var db = new QLNhanVienEntities())
                {
                    db.NhanViens.Add(nv);
                    db.SaveChanges();
                }

                MessageBox.Show("Thêm nhân viên thành công!",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                OnQuayLai?.Invoke();
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show("Lỗi khi lưu vào CSDL:\n" + msg,
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            txtMaNV.Text = "";
            txtTen.Text = "";
            txtTuoi.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            rbNam.IsChecked = false;
            rbNu.IsChecked = false;
        }

        private void BtnQuayLai_Click(object sender, RoutedEventArgs e)
        {
            OnQuayLai?.Invoke();
        }
    }
}
