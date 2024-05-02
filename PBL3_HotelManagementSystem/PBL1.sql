create table Account (
	IDAccount nvarchar(10) primary key,
    UserName nvarchar(MAX),
    Pass nvarchar(MAX),
    PhanQuyen nvarchar(10)
);
insert into Account(IDAccount, UserName, Pass, PhanQuyen) Values ('AD', 'Admin', '1', 'admin')
insert into Account(IDAccount, UserName, Pass, PhanQuyen) Values ('QL01', 'Quanly01', '1', 'Quản Lý')
insert into Account(IDAccount, UserName, Pass, PhanQuyen) Values ('QL02', 'QuanLy02', '1', 'Quản Lý')
insert into Account(IDAccount, UserName, Pass, PhanQuyen) Values ('Acc01', 'KhachHang01', '1', 'Khách Hàng')



create table KhachHang (
    IDKH nvarchar(10) primary key,
    HoTen nvarchar(MAX),
    CCCD varchar(12),
    SDT varchar(10),
    Email varchar(MAX),
    GioiTinh bit,
    DiaChi nvarchar(MAX)
);

insert into KhachHang(IDKH, HoTen, CCCD, SDT, Email, GioiTinh, DiaChi) Values ('KH01', 'Nguyễn A', '012345678911', '0123456781', 'A@gmail.com','true','Sgon')
insert into KhachHang(IDKH, HoTen, CCCD, SDT, Email, GioiTinh, DiaChi) Values ('KH02', 'Nguyễn B', '012345678912', '0123456782', 'B@gmail.com','true','HaNoi')
insert into KhachHang(IDKH, HoTen, CCCD, SDT, Email, GioiTinh, DiaChi) Values ('KH03', 'Nguyễn C', '012345678913', '0123456783', 'C@gmail.com','false','DaNang')


create table LoaiPhong (
    IDLoaiPhong nvarchar(10) primary key,
    TenLoaiPhong nvarchar(50),
    HinhAnh image,
    DonGia float,
    SoNguoi int,
    SoGiuong int
);
insert into LoaiPhong(IDLoaiPhong, TenLoaiPhong, HinhAnh, DonGia, SoNguoi, SoGiuong) values ('LP02', 'VIP2', 'Room1_1.png', 1200000, 2, 1 )

create table Phong (
    IDPHG nvarchar(10) primary key,
    IDLoaiPhong nvarchar(10),
    TenPHG nvarchar(50),
    TrangThai nvarchar(50),
    foreign key (IDLoaiPhong) references LoaiPhong(IDLoaiPhong)
);

create table DatPhong (
    IDDatPhong nvarchar(10) primary key,
    IDKH nvarchar(10),
    IDPHG nvarchar(10),
    NgayDat date,
    NgayTra date,
    SoNgayThue int,
    TrangThai nvarchar(50),
    foreign key (IDKH) references KhachHang(IDKH),
    foreign key (IDPHG) references Phong(IDPHG)
);

create table LoaiDV (
    IDLoaiDV nvarchar(10) primary key,
    TenLoaiDV nvarchar(50),
    DonGia float,
    SoNguoi int 
);

create table DichVu (
    IDDV nvarchar(10) primary key,
    TenDV nvarchar(50),
    IDLoaiDV nvarchar(10),
    foreign key (IDLoaiDV) references LoaiDV(IDLoaiDV)
);

create table DatDichVu (
    IDDatDV nvarchar(10) primary key,
    IDKH nvarchar(10),
    IDDV nvarchar(10),
    NgaySuDung date,
    foreign key (IDKH) references KhachHang(IDKH),
    foreign key (IDDV) references DichVu(IDDV)
);

create table DatDichVuChiTiet (
    IDDatDVChiTiet nvarchar(10) primary key,
    IDDatDV nvarchar(10),
    IDDV nvarchar(10),
    SoLuong int,
    GiaTien float,
    foreign key (IDDatDV) references DatDichVu(IDDatDV),
    foreign key (IDDV) references DichVu(IDDV)
);




create table HoaDon (
    IDHD nvarchar(10) primary key,
    IDKH nvarchar(10),
    IDHDChiTiet nvarchar(10),
    NgayTaoHD date,
    TongDonGia float,
    foreign key (IDKH) references KhachHang(IDKH)
);

create table HoaDonChiTiet (
    IDHDChiTiet nvarchar(10) primary key,
    IDHD nvarchar(10),
    IDDatDVChiTiet nvarchar(10),
    NgayDat date,
    NgayTra date,
    foreign key (IDHD) references HoaDon(IDHD),
    foreign key (IDDatDVChiTiet) references DatDichVuChiTiet(IDDatDVChiTiet)
);
