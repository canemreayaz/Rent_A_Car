create table Kullanicilar
(
	KullaniciID int primary key identity(1,1),
	KullaniciAdi nvarchar(50)not null,
	Sifre nvarchar(100) not null,
	Yetki nvarchar(20) not null
)

create table Musteriler
(
MusteriID int primary key identity(1,1),
TC nvarchar(11) not null unique,
Ad nvarchar(50),
Soyad nvarchar(50),
Telefon nvarchar(15),
Email nvarchar(100),
EhliyetNo nvarchar(20),
EhliyetTarihi date
)

create table Araclar
(
AracID int primary key identity(1,1),
Plaka nvarchar(20) unique,
Marka nvarchar(50),
Model nvarchar(50),
Yil int,
Renk nvarchar(20),
GunlukUcret decimal(10,2),
Durum nvarchar(10)
)

create table Kiralama
(
KiralamaID int primary key identity(1,1),
AracID int,
MusteriID int,
AlisTarihi date,
TeslimTarihi date,
GunlukUcret decimal(10,2),
ToplamUcret decimal(10,2),
TeslimDurumu bit default 0,
foreign key (AracID) references Araclar(AracID),
foreign key (MusteriID) references Musteriler(MusteriID)
)

alter table Kullanicilar add unique (KullaniciAdi)

insert into Kullanicilar (KullaniciAdi, Sifre, Yetki) values('emre', '123', 'admin')

insert into Musteriler (TC, Ad, Soyad, Telefon, Email, EhliyetNo, EhliyetTarihi) 
values ('11111111111', 'Orhan', 'Kaya', '05427151617', 'demirorhan791@gmail.com', '123456', '2025-10-07')

insert into Araclar (Plaka, Marka, Model, Yil, Renk, GunlukUcret, Durum)
values('16ABC2870', 'Renault', 'Clio', 2020, 'Gri', 500.00, 'Boþ')






ALTER TABLE Tbl_Kiralama
ADD CONSTRAINT FK_Kiralama_Arac
FOREIGN KEY (AracID) REFERENCES Tbl_Araclar(AracID);



SELECT A.Marka, COUNT(*) AS KiralanmaSayisi
FROM Tbl_Kiralama K
JOIN Tbl_Araclar A ON K.AracID = A.AracID
GROUP BY A.Marka
ORDER BY KiralanmaSayisi DESC;


SELECT Durum, COUNT(*) AS DurumSayisi
FROM Tbl_Araclar
GROUP BY Durum




SELECT TeslimDurumu, COUNT(*) AS Adet
FROM Tbl_Kiralama
GROUP BY TeslimDurumu

SELECT Marka, COUNT(*) AS Adet
FROM Tbl_Araclar
GROUP BY Marka
ORDER BY Adet DESC


create proc sp_kullanicilar
(
@KullaniciAdi nvarchar(50),
@Sifre nvarchar(50)
)
as
begin
	select COUNT(*) from Tbl_Kullanicilar where @KullaniciAdi = KullaniciAdi and @Sifre = Sifre
end


create proc guvenlikSorusu_sp
as
begin
	select * from Tbl_Kullanicilar
end


alter table Tbl_Kullanicilar add constraint unique_user unique(KullaniciAdi)



SELECT 
    m.MusteriID, 
    m.Ad, 
    m.Soyad, 
    a.Marka, 
    a.Model, 
    k.AlisTarihi, 
    k.TeslimTarihi
FROM Tbl_Kiralama k
INNER JOIN Tbl_Musteriler m ON k.MusteriID = m.MusteriID
INNER JOIN Tbl_Araclar a ON k.AracID = a.AracID
ORDER BY m.Ad, k.AlisTarihi DESC


 SELECT 
 m.MusteriID, 
 m.Ad, 
 m.Soyad, 
 m.Telefon,         
 m.EMail,            
 a.Marka, 
 a.Model, 
 k.AlisTarihi, 
 k.TeslimTarihi,
 k.ToplamUcret
 FROM Tbl_Kiralama k
     INNER JOIN Tbl_Musteriler m ON k.MusteriID = m.MusteriID
     INNER JOIN Tbl_Araclar a ON k.AracID = a.AracID
         WHERE m.MusteriID = 1 -- örnek müþteri ID
         ORDER BY k.AlisTarihi DESC;


SELECT 
  m.MusteriID, 
  m.Ad, 
  m.Soyad, 
  m.Telefon,         
  m.Email AS EMail,         
  a.Marka, 
  a.Model, 
  k.AlisTarihi, 
  k.TeslimTarihi,
  k.ToplamUcret
FROM Tbl_Kiralama k
INNER JOIN Tbl_Musteriler m ON k.MusteriID = m.MusteriID
INNER JOIN Tbl_Araclar a ON k.AracID = a.AracID
WHERE m.MusteriID = 1 --örnek müþteri id
ORDER BY k.AlisTarihi DESC;



SELECT K.*, (M.Ad + ' ' + M.Soyad) AS AdSoyad
FROM Tbl_Kiralama K
INNER JOIN Tbl_Musteriler M ON K.MusteriID = M.MusteriID


ALTER TABLE Tbl_Musteriler
ADD MusteriDurum VARCHAR(20) DEFAULT 'Uygun' NOT NULL;

ALTER TABLE Tbl_Kiralama
ALTER COLUMN MusteriDurum bit;

UPDATE Tbl_Kiralama
SET MusteriDurum = 1 -- veya 0, duruma göre
WHERE MusteriDurum = 'Uygun'

UPDATE Tbl_Kiralama SET MusteriDurum = 1;


SELECT COLUMN_NAME, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Tbl_Kiralama' AND COLUMN_NAME = 'MusteriDurum';


-- Önce tüm deðerleri 1 yap (varsayýlan olarak)
UPDATE Tbl_Kiralama SET MusteriDurum = '1' WHERE MusteriDurum IS NOT NULL;

-- Sonra ALTER iþlemini yap
ALTER TABLE Tbl_Kiralama
ALTER COLUMN MusteriDurum bit;


UPDATE Tbl_Araclar 
SET ArabaGorsel = 'C:\Users\emrea\Desktop\AracKiralamaOtomasyonu\AracKiralamaOtomasyonu\AracKiralamaOtomasyonu\AracGorselleri\toyota-corolla-2023-siyah.jpg' 
WHERE AracID = 22;


SELECT COLUMN_NAME, DATA_TYPE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Kiralama' AND COLUMN_NAME = 'Gorsel'


CREATE TABLE Tbl_Faturalar (
    FaturaID INT PRIMARY KEY IDENTITY,
    MusteriID INT NOT NULL,
    Tarih DATE NOT NULL,
    Yil INT NOT NULL,
    DosyaYolu NVARCHAR(500) NOT NULL, -- PDF yolunu sakla
    FOREIGN KEY (MusteriID) REFERENCES Tbl_Musteriler(MusteriID)
);

update Tbl_Araclar set Durum = 'Boþ'
update Tbl_Musteriler set MusteriDurum = 'Uygun'
update Tbl_Kiralama set TeslimDurumu = 'Boþ'

