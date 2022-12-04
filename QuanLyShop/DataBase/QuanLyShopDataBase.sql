CREATE DATABASE QUANLYSHOP

CREATE TABLE NHANVIEN
(
	ID int identity(1,1) Primary key not null,
	hoTen nvarchar(30) null,
	gioiTinh nvarchar(10) null,
	ngaySinh Date null,
	SDT varchar(13) null,
	email varchar(30) null,
	diaChi nvarchar(50) not null,
	avatar varchar(200) null,
	chucVu nvarchar(30) null,
	tenDangNhap varchar(50),
	matKhau varchar(50),
)

CREATE TABLE KHACHHANG
(
	ID int identity(1,1) Primary Key not null,
	hoTen nvarchar(30) null,
	gioiTinh nvarchar(10) null,
	ngaySinh Date  null,
	SDT varchar(13) null,
	email varchar(30) null,
	diaChi nvarchar(50) null,
	avatar varchar(200) null,
	ranks nvarchar(30) null,
	diemTL int,
)

CREATE TABLE SANPHAM
(
	ID int identity(1,1) Primary key not null,
	IDLoai int not null,
	tenSP nvarchar(50) not null,
	giaSP float null,
	soLuong int null,
	kichCo nvarchar(20) null,
	mieuTaSP nvarchar(200) null,
	imageSP nvarchar(200) null,
	Foreign key(IDLoai) references THELOAI(ID) ON DELETE CASCADE ON UPDATE CASCADE,
)

CREATE TABLE THELOAI
(
	ID int identity(1,1) Primary key not null,
	tenTL nvarchar(50) not null,
)

CREATE TABLE NHAPHANG
(
	ID int identity(1,1) primary key not null,
	IDNhanVien int,
	maSanPham varchar(50) null,
	tenTheLoai nvarchar(30) null, 
	tenSanPham nvarchar(50) null,
	size varchar(30) null,
	soLuongNhap int null,
	giaNhap float null,
	ngayNhap Date not null,
	tongTien float null,
	foreign key(IDNhanVien) references NHANVIEN(ID) ON DELETE CASCADE,
)

CREATE TABLE HOADON
(
	ID int identity(1,1) primary key not null,
	IDNhanVien int,
	IDKhachHang int,
	ngayDat Date,
	trangThai nvarchar(30),
	foreign key(IDKhachHang) references KHACHHANG(ID) ON DELETE CASCADE,
	foreign key(IDNhanVien) references NHANVIEN(ID) ON DELETE CASCADE,
)

CREATE TABLE CTHD
(
	ID INT IDENTITY(1,1) primary key not null,
	IDHD int,
	IDSP int,
	soLuongDat int,
	thanhTien float,
	foreign key(IDHD) references HOADON(ID),
	foreign key(IDSP) references SANPHAM(ID) on update cascade on delete cascade,
)


Insert KHACHHANG 
values (N'Nguyễn Quốc Anh',N'Nam','11-03-2002','0389660305','bikunte@gmail.com',N'96/7 Nguyễn Huệ','D:\QuanLyShop\QuanLyShop\Image\Icon\2.jpg',N'Vàng',3)
Insert KHACHHANG 
values (N'Nguyễn Quốc Em',N'Nam','11-03-2002','0389660305','bikunte@gmail.com',N'96/7 Nguyễn Huệ','D:\QuanLyShop\QuanLyShop\Image\Icon\2.jpg',N'Vàng',6)

Insert NHANVIEN
values (N'Nguyễn Quốc Anh',N'Nam','11-03-2002','0389660305','bikunte@gmail.com',N'96/7 Nguyễn Huệ','D:\QuanLyShop\QuanLyShop\Image\Icon\2.jpg',N'Quản Lý','admin','admin',N'Vàng')

INSERT THELOAI
values(N'Tất Cả')

INSERT THELOAI
values(N'Áo')

INSERT THELOAI
values(N'Quần')

INSERT THELOAI
values(N'Giày')

INSERT THELOAI
values(N'Ba Lô')

INSERT THELOAI
values(N'Áo Khoác')

INSERT THELOAI
values(N'Mũ')

INSERT THELOAI
values('Phụ Kiện')

Insert SANPHAM
values(2,N'Áo Phông ',60000,5,'M',N'Mang thoải mái mát','D:\QuanLyShop\QuanLyShop\Image\Ao\AoBestFriend_1.jpg')

Insert SANPHAM
values(2,N'Áo Phông Vãi',75.000,120,'M',N'Mang thoải mái mát','D:\QuanLyShop\QuanLyShop\Image\Ao\AoThun1_1.jpg')

Insert SANPHAM
values(2,N'Áo Phông Ba',60.000,5,'M',N'Mang thoải mái mát','D:\QuanLyShop\QuanLyShop\Image\Ao\AoThun_1.jpg')

Insert SANPHAM
values(2,N'Áo Phông Ba',50000,5,'M',N'Mang thoải mái mát','D:\QuanLyShop\QuanLyShop\Image\Ao\AoThun_1.jpg')

Insert SANPHAM
values(4,N'Super-Fast Delivery',800000,5,'M',N'Mang thoải mái mát','D:\QuanLyShop\QuanLyShop\Image\Giay\1.png')

Insert SANPHAM
values(4,N'Super-Fast Delivery',50000,5,'M',N'Mang thoải mái mát','D:\QuanLyShop\QuanLyShop\Image\Giay\2.png')

Insert SANPHAM
values(4,N'Super-Fast Delivery',50000,5,'M',N'Mang thoải mái mát','D:\QuanLyShop\QuanLyShop\Image\Giay\3.png')

Insert SANPHAM
values(4,N'Super-Fast Delivery',50000,5,'M',N'Mang thoải mái mát ẤM CHÂN dịu chống nước khó hư bền lâu','D:\QuanLyShop\QuanLyShop\Image\Giay\4.png')

insert NHAPHANG
values(1,2,'Phụ Kiện',N'Đồng Hồ Rolex','S',5,1400000000,'11/03/2002',1400000000)

insert NHAPHANG
values(1,2,'Phụ Kiện',N'Đồng Hồ Vàng','S',5,150000,'11/03/2002',1400000000 + 150000)

insert NHAPHANG
values(1,2,'Phụ Kiện',N'Đồng Hồ Bạc','S',5,120000,'11/03/2002',120000)

insert NHAPHANG
values(1,2,'Phụ Kiện',N'Đồng Hồ Bạc','S',5,120000,'11/03/2002',120000)

insert NHAPHANG
values(1,3,'Phụ Kiện',N'Đồng Hồ Bạc','S',5,120000,'11/03/2002',120000)

insert CTHD
VALUES(1,2,5,50000)

insert CTHD
VALUES(1,3,5,50000)

insert CTHD
VALUES(2,2,5,50000)

insert CTHD
VALUES(2,5,5,50000)

insert CTHD
VALUES(1,4,2,50000)


Insert HOADON
VALUES(1,1,'11-03-2002',N'Đã Thanh Toán')

Insert HOADON
VALUES(1,1,'11-03-2002',N'Đã Thanh Toán')

Insert HOADON
VALUES(1,1,'11-03-2002',N'Đã Thanh Toán')


select top(5) ct.IDSP, sum(soLuongDat) from CTHD as ct 
group by ct.IDSP

DECLARE @i int = 0

while @i < 100
BEGIN
	
Insert KHACHHANG 
values (N'Nguyễn Quốc Anh',N'Nam','11-03-2002','0389660305','bikunte@gmail.com',N'96/7 Nguyễn Huệ','D:\QuanLyShop\QuanLyShop\Image\Icon\2.jpg',N'Vàng',3)
SET @i = @i + 1
END

select * from CTHD
