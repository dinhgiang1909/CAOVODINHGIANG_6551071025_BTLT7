CREATE DATABASE FitZone;
GO
USE FitZone;
GO

CREATE TABLE HoiVien (
    MaHV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh BIT, -- 1: Nam, 0: Nữ
    NgaySinh DATE,
    SDT VARCHAR(15),
    Email VARCHAR(100),
    HangThanhVien NVARCHAR(20),
    NgayDangKy DATETIME DEFAULT GETDATE(),
    TrangThai BIT -- 1: Đang hoạt động, 0: Tạm ngưng
);
GO

-- Thêm dữ liệu mẫu tương tự giao diện
INSERT INTO HoiVien (HoTen, GioiTinh, NgaySinh, SDT, Email, HangThanhVien, TrangThai)
VALUES 
(N'Họ tên Vốn', 1, '1999-11-07', '07382735879', 'von@gmail.com', 'Basic', 1),
(N'Nguyễn Ninh', 1, '1999-11-10', '07382731034', 'nnh@gmail.com', 'Premium', 1);