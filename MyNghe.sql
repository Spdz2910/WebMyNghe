
CREATE TABLE Device (
    MaDevice INT PRIMARY KEY,
    TenDevice VARCHAR(255) NOT NULL,
    GiaBan DECIMAL(10, 2),
    MoTa TEXT,
    AnhBia VARCHAR(255),
    NgayCapNhat DATE,
    SoLuongTon INT,
    MaCD INT,
    MaNSX INT,
    FOREIGN KEY (MaCD) REFERENCES ChuDe(MaCD),
    FOREIGN KEY (MaNSX) REFERENCES Nha_San_Xuat(MaNSX)
);

CREATE TABLE ChuDe (
    MaCD INT PRIMARY KEY,
    TenChuDE VARCHAR(255)
);
CREATE TABLE Nha_San_Xuat (
    MaNSX INT PRIMARY KEY,
    TenNSX VARCHAR(255)
);
CREATE TABLE ChiTiet (
    ID INT PRIMARY KEY,
    MaDev INT,
    DacDiem TEXT,
    IMG VARCHAR(255),
    FOREIGN KEY (MaDev) REFERENCES Device(MaDevice)
);

CREATE TABLE ICON (
    MaSp INT PRIMARY KEY,
    IMG_ICON VARCHAR(255),
    MACD INT,
    Name VARCHAR(255),
    FOREIGN KEY (MACD) REFERENCES ChuDe(MaCD)
);

CREATE TABLE titleCD (
    ID INT PRIMARY KEY,
    MaCD INT,
    Title VARCHAR(255),
    IMG VARCHAR(255),
    FOREIGN KEY (MaCD) REFERENCES ChuDe(MaCD)
);

CREATE TABLE ADMIN (
    User_Admin VARCHAR(255) PRIMARY KEY,
    password VARCHAR(255),
    ho_ten VARCHAR(255)
);

CREATE TABLE User1 (
    MaUser INT PRIMARY KEY,
    HoTen VARCHAR(255),
    TaiKhoan VARCHAR(255),
    MatKhau VARCHAR(255),
    Email VARCHAR(255),
    DiaChi TEXT,
    DienThoai VARCHAR(20),
    NgaySinh DATE
);

CREATE TABLE Khach_Hang (
    MaKH INT PRIMARY KEY,
    TaiKhoan VARCHAR(255),
    MatKhau VARCHAR(255),
    Email VARCHAR(255),
    DiaChi TEXT,
    DienThoai VARCHAR(20),
    NgaySinh DATE
);


CREATE TABLE Don_Dat_Hang (
    MaDH INT PRIMARY KEY,
    MaKH INT,
    NgayDH DATE,
    NgayGiao DATE,
    DaThanhToan BIT,
    TinhTrangGiaoHang VARCHAR(255),
    Phone VARCHAR(20),
    Email VARCHAR(255),
    Address TEXT,
    Name VARCHAR(255),
    FOREIGN KEY (MaKH) REFERENCES Khach_Hang(MaKH)
);

CREATE TABLE CT_DonHang (
    MaCT INT PRIMARY KEY,
    MaDH INT,
    MaDevice INT,
    SoLuong INT,
    DonGia DECIMAL(10, 2),
    FOREIGN KEY (MaDH) REFERENCES Don_Dat_Hang(MaDH),
    FOREIGN KEY (MaDevice) REFERENCES Device(MaDevice)
);

-- Tạo bảng Sheet1
-- Xóa bảng Sheet1
DROP TABLE Sheet1;

CREATE TABLE Sheet1 (
    MaDev INT,
    MaChiTiet INT,
    ChatLieu VARCHAR(255),
    KichThuoc VARCHAR(50),
    DacDiem TEXT,
    MauSac VARCHAR(50),
    Gia DECIMAL(10, 2),
);
-- Thêm khóa ngoại MaDev liên kết bảng Sheet1 với bảng Device
ALTER TABLE Sheet1
ADD CONSTRAINT FK_Sheet1_Device
FOREIGN KEY (MaDev) REFERENCES Device(MaDevice);

Select * from Sheet1

-- Thiết lập cơ sở dữ liệu và bảng để hỗ trợ ngôn ngữ tiếng Việt (nếu chưa thiết lập)

--Thêm dữ liệu
INSERT INTO ChuDe (MaCD, TenChuDE)
VALUES (1, 'Tượng gỗ mỹ nghệ');
INSERT INTO ChuDe (MaCD, TenChuDE)
VALUES (2, 'Lục bình gỗ mỹ nghệ');
INSERT INTO ChuDe (MaCD, TenChuDE)
VALUES (3, 'Tượng con vật phong thủy');
INSERT INTO ChuDe (MaCD, TenChuDE)
VALUES (4, 'Mỹ nghệ trang trí');
INSERT INTO ChuDe (MaCD, TenChuDE)
VALUES (5, 'Mỹ nghệ gia dụng');
INSERT INTO ChuDe (MaCD, TenChuDE)
VALUES (6, 'Quà tặng mỹ nghệ');
INSERT INTO ChuDe (MaCD, TenChuDE)
VALUES (7, 'Mỹ nghệ trị liệu');
INSERT INTO ChuDe (MaCD, TenChuDE)
VALUES (8, 'Đồ thờ bằng gỗ');
Select *from ChuDe


--Them du lieu nha san xuat--
INSERT INTO Nha_San_Xuat (MaNSX, TenNSX)
VALUES (1, N'Nhà sản xuất Bình Thuận');
INSERT INTO Nha_San_Xuat (MaNSX, TenNSX)
VALUES (2, N'Nhà sản xuất Bến Tre');
INSERT INTO Nha_San_Xuat (MaNSX, TenNSX)
VALUES (3, N'Nhà sản xuất Sài Gòn');
INSERT INTO Nha_San_Xuat (MaNSX, TenNSX)
VALUES (4, N'Nhà sản xuất Ninh Thuận');
INSERT INTO Nha_San_Xuat (MaNSX, TenNSX)
VALUES (5, N'Nhà sản xuất Vũng Tàu');
INSERT INTO Nha_San_Xuat (MaNSX, TenNSX)
VALUES (6, N'Nhà sản xuất Bình Dương');
Select * from Nha_San_Xuat


--Them du lieu san pham
INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (1,N'Ong di lặc ngồi ', 52000000, N'Màu gỗ và vân gỗ hoàn toàn tự nhiên', '1.jpg', '2023-08-01', 10, 1, 1);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (2,N'Ong di lặc đa sắc 1  ', 20000000, N'Tượng nguyên khối, sắc sảo,màu sắc tự nhiên. có rất nhiều màu sắc ', '2.jpg', '2023-10-01', 10, 1, 3);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (3,N'Cặp ống gỗ thủy tùng KT ', 5500000, N'Hàng liền khối, vân đẹp mỹ mãn', '3.jpg', '2023-08-10', 10, 2, 1);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (4,N'Lọ hoa kiểu lục bình gỗ Thủy Tùng KT ', 4500000, N'Hàng tiện nguyên khối, siêu vân và đa sắc màu rất đặc biệt', '4.jpg', '2023-08-10', 10, 2, 4);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (5,N'Dê Tài lộc ôm đá gỗ hương 03 ', 4800000, N'Tượng được chạm khắc thủ công, đường nét hài hòa, mềm mại, thần thái có hồn.Đặc biệt gốc có ôm đá tự nhiên.Hàng độc đáo, có 1 không 2.', '5.jpg', '2023-08-10', 10, 3, 1);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (6,N'Bộ 12 con giáp gỗ hương ', 3200000, N'12 con giáp là biểu tượng của 12 bổn mạng . Từ xa xưa con người đã dựa theo tử vi phong thủy, để tính ngày tháng năm sinh cho mình', '6.jpg', '2023-08-10', 10, 3, 5);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (7,N'Khay trà Nu xá xị mẫu 01 ', 3500000, N'Hàng liền khối, dày, nặng chắc đẹp, nu bám đều 90%', '7.jpg', '2023-08-10', 10, 4, 2);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (8,N'Gạt tàn đôi chim sẻ 05 ', 1300000, N'Hàng chạm thủ công, liền khối, sắc nét, mềm mại...!!!', '8.jpg', '2023-08-12', 10, 4, 5);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (9,N'Hộp bánh kẹo kiểu Trống đồng ', 7500000, N'Hàng chạm tinh xảo, sang trọng, nổi bật.
Đựng bánh mứt, làm quà tặng ngày tết.Món quà ý nghĩa và giá trị cao!', '9.jpg', '2023-08-29', 10, 5, 2);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (10,N'Bàn ô sin gỗ hương 01 ', 2500000 , N'Bàn được đóng mộng chắc chắn, khuôn 4 ván 2cm. ', '10.jpg', '2023-08-22', 10, 5, 5);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (11,N'Thuyền buồm Hạ long ', 8000000, N'Trang trí, trưng bày, làm quà tặng tân gia, quà tặng khai trương.', '11.jpg', '2023-11-29', 10, 6, 3);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (12,N'Hộp đựng trang sức Thông Cò ', 3000000 , N'Được làm từ gỗ Hương, chạm nổi Thông cò ', '12.jpg', '2023-08-29', 10, 6, 5);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (13,N'MÁT XA CHÂN GỖ THƠM NHỎ 01 ', 3000000, N'Được làm từ gỗ thơm tự nhiên nguyên khối,tác dụng trị liệu tốt.', '14.jpg', '2023-08-09', 10, 7, 2);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (14,N'CÂY GẬY TRẮC DÂY ', 380000  , N'Được thiết kế dành cho người Gìa, người bệnh ', '13.jpg', '2023-08-22', 10, 7, 5);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (15,N'Sập thờ Mai điểu gỗ gụ ', 45000000 , N'Được tiện và chạm công phu từ gỗ gõ đỏ tự nhiên, đường nét tỉ mĩ, sắc nét .', '15.jpg', '2023-08-11', 10, 8, 2);

INSERT INTO Device (MaDevice,TenDevice, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaCD, MaNSX)
VALUES (16,N'Tháp văn xương gỗ hương 02 ', 8000000 , N'Tháp tiện liền khối, gồm có 9 tầng, tầng trên cùng có tháp đồng xông trầm. ', '16.jpg', '2023-08-25', 10, 8, 5);

--Thêm dữ liệu vào sheet1 
INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES
    (1, 1002, N'Kim loại', '15x15x10', 'Nhẹ và chắc chắn', 'Đen', 75.25),
    (2, 1005, N'Gỗ thủy tùng', '80cm x 70cm x  40cm', N'Nguyên khối, không chắp vá, không tỳ vết
', 'Đen', 75.25),
    (3, 1003, 'Nhựa', '25x30x18', 'Dễ dàng vệ sinh', 'Trắng', 45.80),
    (4, 1004, 'Đá', '40x40x30', 'Phù hợp trang trí sân vườn', 'Xám', 200.00);

		
INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES (5, 1005, N'Gỗ', N'30x40x20', N'Đẹp và bền', N'Nâu', 150.50);

INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES (6, 1006, N'Gỗ', N'20x25x15', N'Kiểu dáng độc đáo, màu sắc tự nhiên', N'Nâu đậm', 120.00);

INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES (7, 1007, N'Kim loại', N'15x15x10', N'Nhẹ và chắc chắn, sắc sảo', N'Đen', 75.25);

INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES (8, 1008, N'Gỗ', N'30x50x25', N'Chất liệu tự nhiên, đẹp và bền', N'Nâu', 180.00);

INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES (9, 1009, N'Gỗ', N'20x20x20', N'Chất liệu gỗ tự nhiên, thiết kế tinh xảo', N'Nâu', 100.00);

INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES (10, 1010, N'Nhựa', N'20x30x15', N'Dễ dàng vệ sinh, màu sắc đa dạng', N'Đỏ', 65.50);

INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES (11, 1011, N'Đá', N'30x30x30', N'Phù hợp trang trí nội thất', N'Trắng', 180.00);

INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES (12, 1012, N'Gỗ', N'25x35x15', N'Kiểu dáng độc đáo, màu sắc tự nhiên', N'Nâu đậm', 130.00);

INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES (13, 1013, N'Gỗ', N'30x40x20', N'Đẹp và bền', N'Nâu', 150.50);

INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES (14, 1014, N'Kim loại', N'15x15x10', N'Nhẹ và chắc chắn, sắc sảo, màu sắc tự nhiên', N'Đen', 75.25);
INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES (15, 1015, N'Nhựa', N'25x30x18', N'Dễ dàng vệ sinh, màu sắc đa dạng', N'Xanh', 45.80);

INSERT INTO Sheet1 (MaDev, MaChiTiet, ChatLieu, KichThuoc, DacDiem, MauSac, Gia)
VALUES (16, 1016, N'Đá', N'40x40x30', N'Phù hợp trang trí sân vườn', N'Đen', 200.00);

select * from Device

--Them du lieu vao admin--
INSERT INTO ADMIN (User_Admin, password, ho_ten)
VALUES ('thinh', '123', N'Nguyễn Duy THinh ');

INSERT INTO ADMIN (User_Admin, password, ho_ten)
VALUES ('thuan', '123', N'Nguyễn Duy Thinh ');

