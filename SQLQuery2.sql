/* ========= 1. TẠO CƠ SỞ DỮ LIỆU ========= */
CREATE DATABASE HY_QL_NhanSu;
GO
USE HY_QL_NhanSu;
GO

/* ========= 2. TẠO BẢNG DEPARMENT (PHÒNG BAN) ========= */
CREATE TABLE tbl_Deparment (
    DeptId INT PRIMARY KEY,
    Name NVARCHAR(100)
);
GO

INSERT INTO tbl_Deparment (DeptId, Name) VALUES
(1, N'Khoa CNTT'),
(2, N'Khoa Ngoại Ngữ'),
(3, N'Khoa Tài Chính'),
(4, N'Khoa Thực Phẩm'),
(5, N'Phòng Đào Tạo');
GO

/* ========= 3. TẠO BẢNG EMPLOYEE (NHÂN VIÊN) ========= */
CREATE TABLE tbl_Employee (
    Id INT PRIMARY KEY, -- Không dùng IDENTITY để khớp với dữ liệu mẫu
    Name NVARCHAR(100),
    Gender NVARCHAR(10),
    City NVARCHAR(100),
    Image NVARCHAR(255), -- Cột này được thêm vào dựa trên giao diện
    DeptId INT,
    FOREIGN KEY (DeptId) REFERENCES tbl_Deparment(DeptId)
);
GO

/* ========= 4. CHÈN DỮ LIỆU MẪU ========= */
-- (Tên ảnh là ví dụ, bạn cần chuẩn bị các file ảnh này)
INSERT INTO tbl_Employee (Id, Name, Gender, City, Image, DeptId) VALUES
(1, N'Nguyễn Hải Yến', N'Nữ', N'Đà Lạt', N'nv1.jpg', 1),
(2, N'Trương Mạnh Hồng', N'Nam', N'TP.HCM', N'nv2.jpg', 1),
(3, N'Đinh Duy Mạnh', N'Nam', N'Thái Bình', N'nv3.jpg', 2),
(4, N'Ngô Thị Nguyệt', N'Nữ', N'Long An', N'nv4.jpg', 2),
(5, N'Đào Minh Châu', N'Nữ', N'Bạc Liêu', N'nv5.jpg', 3),
(14, N'Phan Thị Ngọc Mai', N'Nữ', N'Bến Tre', N'nv14.jpg', 3),
(15, N'Trương Nguyễn Quỳnh Anh', N'Nữ', N'TP.HCM', N'nv15.jpg', 4),
(16, N'Lê Thanh Liêm', N'Nam', N'TP.HCM', N'nv16.jpg', 4),
(17, N'bbb', N'Nữ', N'TP.HCM', N'nv17.jpg', 5);
GO

PRINT N'CSDL HY_QL_NhanSu đã được tạo thành công!';