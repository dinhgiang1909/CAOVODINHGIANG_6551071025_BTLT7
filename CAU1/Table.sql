CREATE DATABASE TriThucBooks;
GO
USE TriThucBooks;
GO

CREATE TABLE TheLoaiSach (
    MaTL INT IDENTITY(1,1) PRIMARY KEY,
    TenTheLoai NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(255) NULL,
    SoLuongSach INT DEFAULT 0,
    NgayTao DATETIME DEFAULT GETDATE()
);

INSERT INTO TheLoaiSach (TenTheLoai, MoTa, SoLuongSach) VALUES 
(N'Tiểu thuyết', N'Thần tiểu thuyết', 1),
(N'Kỹ năng sống', N'Kỹ năng sống năng sống', 15),
(N'Thiếu nhi', N'Thiếu nhi củ cỏ', 2);