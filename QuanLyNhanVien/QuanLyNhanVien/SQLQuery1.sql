-- 1. Tạo bảng Chức vụ (ChucVu)
CREATE TABLE ChucVu (
    MaCV NVARCHAR(20) PRIMARY KEY,
    TenCV NVARCHAR(100),
    MoTa NVARCHAR(255)
);

-- 2. Tạo bảng Phòng ban (PhongBan)
CREATE TABLE PhongBan (
    MaPB NVARCHAR(20) PRIMARY KEY,
    TenPB NVARCHAR(100),
    MoTa NVARCHAR(255)
);

-- 3. Tạo bảng Vị trí phòng ban (ViTriPhongBan)
CREATE TABLE ViTriPhongBan (
    MaVT NVARCHAR(20) PRIMARY KEY,
    TenVT NVARCHAR(100),
    MaPB NVARCHAR(20),
    CONSTRAINT FK_ViTri_PhongBan FOREIGN KEY (MaPB) REFERENCES PhongBan(MaPB)
);

-- 4. Tạo bảng Nhân viên (NhanVien)
CREATE TABLE NhanVien (
    MaNV NVARCHAR(20) PRIMARY KEY,
    Ten NVARCHAR(100),
    Tuoi INT,
    GioiTinh NVARCHAR(10),
    Email NVARCHAR(100),
    SDT NVARCHAR(15),
    MaPB NVARCHAR(20),
    MaCV NVARCHAR(20),
    MaVT NVARCHAR(20),
    CONSTRAINT FK_NV_PhongBan FOREIGN KEY (MaPB) REFERENCES PhongBan(MaPB),
    CONSTRAINT FK_NV_ChucVu FOREIGN KEY (MaCV) REFERENCES ChucVu(MaCV),
    CONSTRAINT FK_NV_ViTri FOREIGN KEY (MaVT) REFERENCES ViTriPhongBan(MaVT)
);

-- 5. Tạo bảng Lương (Luong)
CREATE TABLE Luong (
    MaLuong INT IDENTITY(1,1) PRIMARY KEY,
    MaNV NVARCHAR(20),
    LuongCoBan DECIMAL(15,2),
    PhuCap DECIMAL(15,2),
    Thuong DECIMAL(15,2),
    KhauTru DECIMAL(15,2),
    TongLuong DECIMAL(15,2),
    CONSTRAINT FK_Luong_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

-- 6. Tạo bảng Tài khoản (TaiKhoan)
CREATE TABLE TaiKhoan (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE,
    Password NVARCHAR(100),
    Role NVARCHAR(20),
    MaNV NVARCHAR(20),
    CONSTRAINT FK_TaiKhoan_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO

USE QLNhanVien;
GO
-- 1. Thêm 5 dòng dữ liệu cho bảng Phòng ban (PhongBan)
INSERT INTO PhongBan (MaPB, TenPB, MoTa) VALUES
('PB01', N'Phòng Nhân sự', N'Quản lý nhân sự và tuyển dụng'),
('PB02', N'Phòng Kế toán', N'Quản lý thu chi, lương thưởng'),
('PB03', N'Phòng IT', N'Phát triển phần mềm và hệ thống'),
('PB04', N'Phòng Kinh doanh', N'Tìm kiếm khách hàng và bán hàng'),
('PB05', N'Phòng Marketing', N'Lên chiến dịch quảng cáo và PR');

-- * Bổ sung dữ liệu cho Chức vụ (để Nhân Viên có thể tham chiếu đến)
INSERT INTO ChucVu (MaCV, TenCV, MoTa) VALUES
('CV01', N'Giám đốc', N'Quản lý điều hành chung'),
('CV02', N'Trưởng phòng', N'Quản lý hoạt động của phòng ban'),
('CV03', N'Chuyên viên', N'Chuyên viên cấp cao'),
('CV04', N'Nhân viên', N'Nhân viên chính thức'),
('CV05', N'Thực tập sinh', N'Nhân viên đang học việc');

-- * Bổ sung dữ liệu cho Vị trí phòng ban (để Nhân Viên có thể tham chiếu đến)
INSERT INTO ViTriPhongBan (MaVT, TenVT, MaPB) VALUES
('VT01', N'Trưởng phòng Nhân sự', 'PB01'),
('VT02', N'Kế toán trưởng', 'PB02'),
('VT03', N'Lập trình viên Senior', 'PB03'),
('VT04', N'Nhân viên Sale', 'PB04'),
('VT05', N'Thực tập sinh Marketing', 'PB05');

-- 2. Thêm 5 dòng dữ liệu cho bảng Nhân viên (NhanVien)
-- Lưu ý: Các mã MaPB, MaCV, MaVT phải khớp với dữ liệu đã nhập ở trên
INSERT INTO NhanVien (MaNV, Ten, Tuoi, GioiTinh, Email, SDT, MaPB, MaCV, MaVT) VALUES
('NV01', N'Nguyễn Văn An', 35, N'Nam', 'an.nv@gmail.com', '0901234567', 'PB01', 'CV02', 'VT01'),
('NV02', N'Trần Thị Bích', 32, N'Nữ', 'bich.tt@gmail.com', '0912345678', 'PB02', 'CV02', 'VT02'),
('NV03', N'Lê Hoàng Cường', 28, N'Nam', 'cuong.lh@gmail.com', '0923456789', 'PB03', 'CV03', 'VT03'),
('NV04', N'Phạm Mai Dung', 25, N'Nữ', 'dung.pm@gmail.com', '0934567890', 'PB04', 'CV04', 'VT04'),
('NV05', N'Hoàng Tuấn Em', 22, N'Nam', 'em.ht@gmail.com', '0945678901', 'PB05', 'CV05', 'VT05');

-- 3. Thêm 5 dòng dữ liệu cho bảng Tài khoản (TaiKhoan)
-- Lưu ý: Cột 'Id' được thiết lập IDENTITY(1,1) nên sẽ tự động tăng, ta KHÔNG điền cột Id vào câu lệnh INSERT
INSERT INTO TaiKhoan (Username, Password, Role, MaNV) VALUES
('admin_an', 'hashed_pass_123', 'Admin', 'NV01'),
('ketoan_bich', 'hashed_pass_456', 'Accountant', 'NV02'),
('dev_cuong', 'hashed_pass_789', 'User', 'NV03'),
('sale_dung', 'hashed_pass_abc', 'User', 'NV04'),
('intern_em', 'hashed_pass_xyz', 'User', 'NV05');