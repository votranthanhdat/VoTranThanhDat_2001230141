USE master;
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'BookStore')
    DROP DATABASE BookStore;
GO

CREATE DATABASE BookStore;
GO
USE BookStore;
GO

/* ==================== BẢNG CHỦ ĐỀ ==================== */
CREATE TABLE CHUDE (
    MaCD INT PRIMARY KEY IDENTITY(1,1),
    TenChuDe NVARCHAR(100)
);

INSERT INTO CHUDE (TenChuDe) VALUES
(N'Tiểu thuyết'),
(N'Công nghệ'),
(N'Kinh tế'),
(N'Thiếu nhi');

/* ==================== BẢNG TÁC GIẢ ==================== */
CREATE TABLE TACGIA (
    MaTG INT PRIMARY KEY IDENTITY(1,1),
    TenTG NVARCHAR(100),
    DiaChi NVARCHAR(200),
    DienThoai NVARCHAR(20)
);

INSERT INTO TACGIA (TenTG, DiaChi, DienThoai) VALUES
(N'Nguyễn Nhật Ánh', N'TP.HCM', N'090900001'),
(N'Trần Đình Hòa', N'Hà Nội', N'091200002'),
(N'Lê Minh Quân', N'Đà Nẵng', N'090300003');

/* ==================== BẢNG NHÀ XUẤT BẢN ==================== */
CREATE TABLE NHAXUATBAN (
    MaNXB INT PRIMARY KEY IDENTITY(1,1),
    TenNXB NVARCHAR(100),
    DiaChi NVARCHAR(200),
    DienThoai NVARCHAR(20)
);

INSERT INTO NHAXUATBAN (TenNXB, DiaChi, DienThoai) VALUES
(N'NXB Kim Đồng', N'60 Trần Hưng Đạo, Hà Nội', N'02438222222'),
(N'NXB Trẻ', N'161B Lý Chính Thắng, TP.HCM', N'02839312222');

/* ==================== BẢNG SÁCH (Không có AnhBia lúc tạo) ==================== */
CREATE TABLE SACH (
    MaSach INT PRIMARY KEY IDENTITY(1,1),
    TenSach NVARCHAR(150),
    DonViTinh NVARCHAR(50),
    DonGia DECIMAL(18,2),
    SoTrang INT,
    MaCD INT,
    MaTG INT,
    MaNXB INT,
    FOREIGN KEY (MaCD) REFERENCES CHUDE(MaCD),
    FOREIGN KEY (MaTG) REFERENCES TACGIA(MaTG),
    FOREIGN KEY (MaNXB) REFERENCES NHAXUATBAN(MaNXB)
);

INSERT INTO SACH (TenSach, DonViTinh, DonGia, SoTrang, MaCD, MaTG, MaNXB) VALUES
(N'Mắt Biếc', N'Quyển', 95000, 250, 1, 1, 2),
(N'Lập trình C cơ bản', N'Quyển', 120000, 300, 2, 2, 2),
(N'Tư duy tích cực', N'Quyển', 78000, 180, 3, 3, 1);

/* ==================== BẢNG KHÁCH HÀNG ==================== */
CREATE TABLE KHACHHANG (
    MaKH INT PRIMARY KEY IDENTITY(1,1),
    HoTenKH NVARCHAR(100),
    DiaChi NVARCHAR(200),
    DienThoai NVARCHAR(20),
    Email NVARCHAR(100)
);

INSERT INTO KHACHHANG (HoTenKH, DiaChi, DienThoai, Email) VALUES
(N'Lê Thị Hoa', N'Hà Nội', N'091800004', N'hoale@gmail.com'),
(N'Phạm Văn Nam', N'Đà Nẵng', N'0901234567', N'nampham@gmail.com');

/* ==================== BẢNG ĐƠN ĐẶT HÀNG ==================== */
CREATE TABLE DONDATHANG (
    MaDH INT PRIMARY KEY IDENTITY(1,1),
    NgayDat DATE,
    NgayGiaoHang DATE,
    MaKH INT,
    FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH)
);

INSERT INTO DONDATHANG (NgayDat, NgayGiaoHang, MaKH) VALUES
('2025-10-20', '2025-10-25', 1),
('2025-10-22', '2025-10-27', 2);

/* ==================== BẢNG CHI TIẾT ĐƠN HÀNG ==================== */
CREATE TABLE CHITIETDONTHANG (
    MaDH INT,
    MaSach INT,
    SoLuong INT,
    DonGia DECIMAL(18,2),
    PRIMARY KEY (MaDH, MaSach),
    FOREIGN KEY (MaDH) REFERENCES DONDATHANG(MaDH),
    FOREIGN KEY (MaSach) REFERENCES SACH(MaSach)
);

INSERT INTO CHITIETDONTHANG (MaDH, MaSach, SoLuong, DonGia) VALUES
(1, 1, 2, 95000),
(1, 2, 1, 120000),
(2, 3, 3, 78000);
ALTER TABLE SACH
ADD Mota NVARCHAR(MAX); 



ALTER TABLE SACH
ADD AnhBia VARCHAR(255); 
GO

UPDATE SACH
SET AnhBia = 'mat biec.jpg'
WHERE MaSach = 1;

UPDATE SACH
SET AnhBia = 'lap trinh c.jpg'
WHERE MaSach = 2;

UPDATE SACH
SET AnhBia = 'tu duy tich cuc.jpg'
WHERE MaSach = 3;
GO

PRINT N'Cơ sở dữ liệu BookStore đã được tạo thành công!';
GO