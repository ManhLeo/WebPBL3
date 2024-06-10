
CREATE TABLE KhachHang (
  IDKH  varchar(10) NOT NULL primary key,
  Email varchar(50) NOT NULL,
  HoTen NVARCHAR(50) NOT NULL,
  SDT varchar(10) NOT NULL,
  CCCD varchar(12) NOT NULL,
  DiaChi NVARCHAR(50)
);

CREATE TABLE Account (
  IDAccount  varchar(10) NOT NULL primary key,
  Email varchar(50) NOT NULL,
  Pass varchar(50) not null,
  PhanQuyen nvarchar(20)

);

Create Table LoaiPhong(
	IDLoaiPhong varchar(10) NOT NULL primary key,
	TenLoaiPhong NVARCHAR(50) NOT NULL,
	DonGia int not null
);

Create Table Phong(
	IDPHG varchar(10) not null primary key,
	TenPHG varchar(10) not null,
	IDLoaiPhong varchar(10) NOT NULL,
	SoGiuong int not null,
	TrangThai nvarchar(20),

	FOREIGN KEY (IDLoaiPhong) REFERENCES LoaiPhong(IDLoaiPhong)
);

Create Table DichVu(
	IDDV varchar(10) not null primary key,
	TenDV NVARCHAR(50) NOT NULL,
	DonGia int not null, 
	SoLuongMax int not null,
	Soluong int not null,
	CHECK (Soluong <= SoLuongMax)
)

Create Table HoaDon(
	IDHD varchar(50) not null primary key,
	IDKH varchar(10) NOT NULL,
	DonGia int,
	TrangThai nvarchar(20),
	FOREIGN KEY (IDKH) REFERENCES KhachHang(IDKH)
)

Create Table DatPhong(
	IDDatPHG varchar(50) not null primary key,
	IDHD varchar(50) not null,
	IDPHG varchar(10) not null,
	NgayNhan date not null,
	NgayTra date not null,
	DonGia int,
	CHECK (NgayNhan <= NgayTra),
	FOREIGN KEY (IDPHG) REFERENCES Phong(IDPHG),
	FOREIGN KEY (IDHD) REFERENCES HoaDon(IDHD)
)

Create Table DatDichVu(
	IDDatDV varchar(50) not null primary key,
	IDHD varchar(50) not null,
	IDDV varchar(10) not null,
	NgaySD date,
	SoLuong int,
	DonGia int,
	FOREIGN KEY (IDDV) REFERENCES DichVu(IDDV),
	FOREIGN KEY (IDHD) REFERENCES HoaDon(IDHD)
)
