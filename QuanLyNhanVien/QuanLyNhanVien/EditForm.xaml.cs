using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyNhanVien
{
    public partial class EditForm : UserControl
    {
        public Action OnQuayLai { get; set; }
        private string _maNV;

        public EditForm()
        {
            InitializeComponent();
        }

        public void LoadNhanVien(NhanVien nv)
        {
            _maNV = nv.MaNV;
            txtMaNV.Text = nv.MaNV;
            txtTen.Text = nv.Ten;
            txtTuoi.Text = nv.Tuoi.HasValue ? nv.Tuoi.Value.ToString() : "";
            txtEmail.Text = nv.Email;
            txtSDT.Text = nv.SDT;

            if (nv.GioiTinh == "Nam")
                rbNam.IsChecked = true;
            else if (nv.GioiTinh == "Nữ")
                rbNu.IsChecked = true;
        }

        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn lưu thông tin nhân viên không?",
                "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result != MessageBoxResult.OK)
                return;

            if (string.IsNullOrWhiteSpace(txtTen.Text) ||
                string.IsNullOrWhiteSpace(txtTuoi.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                (rbNam.IsChecked != true && rbNu.IsChecked != true))
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin nhân viên",
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var db = new QLNhanVienEntities())
                {
                    var entity = db.NhanViens.Find(_maNV);
                    if (entity != null)
                    {
                        entity.Ten = txtTen.Text.Trim();
                        entity.Tuoi = int.TryParse(txtTuoi.Text.Trim(), out int tuoi) ? tuoi : (int?)null;
                        entity.GioiTinh = rbNam.IsChecked == true ? "Nam" : "Nữ";
                        entity.Email = txtEmail.Text.Trim();
                        entity.SDT = txtSDT.Text.Trim();
                        db.SaveChanges();
                    }
                }

                MessageBox.Show("Cập nhật nhân viên thành công!",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                OnQuayLai?.Invoke();
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show("Lỗi khi cập nhật CSDL:\n" + msg,
                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnXoaForm_Click(object sender, RoutedEventArgs e)
        {
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
