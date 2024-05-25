
create table KhachHang (
    IDKH nvarchar(10) primary key,
    HoTen nvarchar(MAX),
    CCCD varchar(12),
    SDT varchar(10),
    Email varchar(MAX) not null,
    GioiTinh bit,
    DiaChi nvarchar(MAX)
);

insert into KhachHang(IDKH, HoTen, CCCD, SDT, Email, GioiTinh, DiaChi) Values ('KH01', 'Nguyễn A', '012345678911', '0123456781', 'A@gmail.com','true','Sgon')
insert into KhachHang(IDKH, HoTen, CCCD, SDT, Email, GioiTinh, DiaChi) Values ('KH02', 'Nguyễn B', '012345678912', '0123456782', 'B@gmail.com','true','HaNoi')
insert into KhachHang(IDKH, HoTen, CCCD, SDT, Email, GioiTinh, DiaChi) Values ('KH03', 'Nguyễn C', '012345678913', '0123456783', 'C@gmail.com','false','DaNang')
insert into KhachHang(IDKH, HoTen, CCCD, SDT, Email, GioiTinh, DiaChi) Values ('KH04', 'Lê Bảo', '012345678914', '0123456784', 'bao@gmail.com','true','Vinh')
insert into KhachHang(IDKH, HoTen, CCCD, SDT, Email, GioiTinh, DiaChi) Values ('KH05', 'Hoàng Mạnh', '012345678915', '0123456785', 'Manh@gmail.com','true','HaNoi')
insert into KhachHang(IDKH, HoTen, CCCD, SDT, Email, GioiTinh, DiaChi) Values ('KH06', 'Nguyễn Cường', '012345678916', '0123456786', 'Cuong@gmail.com','true','DaNang')

create table Account (
	IDAccount nvarchar(10) primary key,
    UserName nvarchar(MAX),
	Email nvarchar(MAX) not null,
    Pass nvarchar(MAX),
    PhanQuyen nvarchar(10)
);
insert into Account(IDAccount, UserName,Email, Pass, PhanQuyen) Values ('AD', 'Admin','ad@gmail.com' ,'1', 'admin')
insert into Account(IDAccount, UserName,Email, Pass, PhanQuyen) Values ('QL01', 'Quanly01','quanly1@gmail.com', '1', 'Quản Lý')
insert into Account(IDAccount, UserName,Email, Pass, PhanQuyen) Values ('QL02', 'QuanLy02','quanly2@gmail.com', '1', 'Quản Lý')
insert into Account(IDAccount, UserName,Email, Pass, PhanQuyen) Values ('KH01', 'KhachHang01','A@gmail.com', '1', 'Khách Hàng')
insert into Account(IDAccount, UserName,Email, Pass, PhanQuyen) Values ('KH02', 'KhachHang02','B@gmail.com', '1', 'Khách Hàng')
insert into Account(IDAccount, UserName,Email, Pass, PhanQuyen) Values ('KH03', 'KhachHang03', 'C@gmail.com', '1', 'Khách Hàng')
insert into Account(IDAccount, UserName,Email, Pass, PhanQuyen) Values ('KH04', 'KhachHang04', 'bao@gmail.com','1', 'Khách Hàng')
insert into Account(IDAccount, UserName,Email, Pass, PhanQuyen) Values ('KH05', 'KhachHang05','Manh@gmail.com', '1', 'Khách Hàng')
insert into Account(IDAccount, UserName,Email, Pass, PhanQuyen) Values ('KH06', 'KhachHang06','Cuong@gmail.com','1', 'Khách Hàng')
Update Account
SET Email = (SELECT Email FROM KhachHang WHERE KhachHang.IDKH = Account.IDAccount);

create table LoaiPhong (
    IDLoaiPhong nvarchar(10) primary key,
    TenLoaiPhong nvarchar(50),
    HinhAnh varchar(50),
    DonGia float,
    SoNguoi int,
    SoGiuong int
);
insert into LoaiPhong(IDLoaiPhong, TenLoaiPhong, HinhAnh, DonGia, SoNguoi, SoGiuong) values ('LP02', 'VIP2', 'Room1_1.png', 1200000, 2, 1 )
insert into LoaiPhong(IDLoaiPhong, TenLoaiPhong, HinhAnh, DonGia, SoNguoi, SoGiuong) values ('LP01', 'VIP1', 'Room1_2.png', 1000000, 2, 1 )
insert into LoaiPhong(IDLoaiPhong, TenLoaiPhong, HinhAnh, DonGia, SoNguoi, SoGiuong) values ('LP03', 'Thuong', 'Room1_3.png', 500000, 2, 2 )
insert into LoaiPhong(IDLoaiPhong, TenLoaiPhong, HinhAnh, DonGia, SoNguoi, SoGiuong) values ('LP04', 'VIP1', 'Room2_1.png', 1000000, 2, 1 )
insert into LoaiPhong(IDLoaiPhong, TenLoaiPhong, HinhAnh, DonGia, SoNguoi, SoGiuong) values ('LP05', 'VIP2', 'Room3_1.png', 1200000, 2, 1 )


create table Phong (
    IDPHG nvarchar(10) primary key,
    IDLoaiPhong nvarchar(10),
    TenPHG nvarchar(50),
    TrangThai nvarchar(50),
    foreign key (IDLoaiPhong) references LoaiPhong(IDLoaiPhong)
);

insert into Phong(IDPHG, IDLoaiPhong, TenPHG, TrangThai) values('PHG01', 'LP03', 'P101', 'Trống')
insert into Phong(IDPHG, IDLoaiPhong, TenPHG, TrangThai) values('PHG02', 'LP03', 'P102', 'Trống')
insert into Phong(IDPHG, IDLoaiPhong, TenPHG, TrangThai) values('PHG03', 'LP01', 'P103', 'Bận')
insert into Phong(IDPHG, IDLoaiPhong, TenPHG, TrangThai) values('PHG04', 'LP01', 'P104', 'Bận')
insert into Phong(IDPHG, IDLoaiPhong, TenPHG, TrangThai) values('PHG05', 'LP05', 'P201', 'Trống')
insert into Phong(IDPHG, IDLoaiPhong, TenPHG, TrangThai) values('PHG0', 'LP04', 'P202', 'Bận')

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
    SoLuong int,
    GiaTien float,
    foreign key (IDDatDV) references DatDichVu(IDDatDV),

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


