CREATE DATABASE SunriseHomestay;
GO
USE SunriseHomestay;
GO


CREATE TABLE LoaiPhong (
    MaLoai INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL,
    GiaMoiDem DECIMAL(18,2),
    MoTa NVARCHAR(255)
);
GO


CREATE TABLE Phong (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    SoPhong VARCHAR(10) NOT NULL,
    TangSo INT,
    TinhTrang NVARCHAR(20), 
    HinhAnh NVARCHAR(255), 
    MaLoai INT,
    FOREIGN KEY (MaLoai) REFERENCES LoaiPhong(MaLoai)
);
GO


INSERT INTO LoaiPhong (TenLoai, GiaMoiDem, MoTa) VALUES 
(N'Phòng Đơn', 350000, N'Phòng 1 giường cho 1 người'),
(N'Phòng Đôi', 500000, N'Phòng 2 giường cho 2 người'),
(N'Phòng VIP', 800000, N'Phòng cao cấp');