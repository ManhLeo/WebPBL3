create table KhachHang (
    IDKH int primary key,
    HoTen nvarchar(MAX),
    CCCD varchar(12),
    SDT varchar(10),
    Email varchar(MAX),
    GioiTinh bit,
    DiaChi nvarchar(MAX)
);

create table LoaiPhong (
    IDLoaiPhong int primary key,
    TenLoaiPhong nvarchar(50),
    HinhAnh image,
    DonGia float,
    SoNguoi int,
    SoGiuong int
);

create table Phong (
    IDPHG int primary key,
    IDLoaiPhong int,
    TenPHG nvarchar(50),
    TrangThai nvarchar(50),
    foreign key (IDLoaiPhong) references LoaiPhong(IDLoaiPhong)
);

create table DatPhong (
    IDDatPhong int primary key,
    IDKH int,
    IDPHG int,
    NgayDat date,
    NgayTra date,
    SoNgayThue int,
    TrangThai nvarchar(50),
    foreign key (IDKH) references KhachHang(IDKH),
    foreign key (IDPHG) references Phong(IDPHG)
);

create table LoaiDV (
    IDLoaiDV int primary key,
    TenLoaiDV nvarchar(50),
    DonGia float,
    SoNguoi int 
);

create table DichVu (
    IDDV int primary key,
    TenDV nvarchar(50),
    IDLoaiDV int,
    foreign key (IDLoaiDV) references LoaiDV(IDLoaiDV)
);

create table DatDichVu (
    IDDatDV int primary key,
    IDKH int,
    IDDV int,
    NgaySuDung date,
    foreign key (IDKH) references KhachHang(IDKH),
    foreign key (IDDV) references DichVu(IDDV)
);

create table DatDichVuChiTiet (
    IDDatDVChiTiet int primary key,
    IDDatDV int,
    IDDV int,
    SoLuong int,
    GiaTien float,
    foreign key (IDDatDV) references DatDichVu(IDDatDV),
    foreign key (IDDV) references DichVu(IDDV)
);

create table Account (
    UserName nvarchar(MAX),
    Pass nvarchar(MAX),
    PhanQuyen nvarchar
);



create table HoaDon (
    IDHD int primary key,
    IDKH int,
    IDHDChiTiet int,
    NgayTaoHD date,
    TongDonGia float,
    foreign key (IDKH) references KhachHang(IDKH)
);

create table HoaDonChiTiet (
    IDHDChiTiet int primary key,
    IDHD int,
    IDDatDVChiTiet int,
    NgayDat date,
    NgayTra date,
    foreign key (IDHD) references HoaDon(IDHD),
    foreign key (IDDatDVChiTiet) references DatDichVuChiTiet(IDDatDVChiTiet)
);
