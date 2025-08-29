using AracKiralamaOtomasyonu.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyonu.Repository
{
    public class KiralamaRepository : repository
    {
        // Tbl_Musteriler tablosundaki tüm satırları, C# sınıfına dönüştürerek döner
        public List<Kiralama> GetAll()
        {
            using (var conn = GetConnection())
            {
                string sql = "SELECT * FROM Tbl_Kiralama";
                return conn.Query<Kiralama>(sql).ToList();
            }
        }
        // Add metodu (MusteriDurum = 1 olarak ayarlanmış şekilde)
        public void Add(Kiralama kiralama)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Kiralama ekle (MusteriDurum = 1 olarak ayarla)
                        conn.Execute(@"INSERT INTO Tbl_Kiralama 
                    (AracID, MusteriID, AlisTarihi, TeslimTarihi, 
                     GunlukUcret, ToplamUcret, Gorsel, TeslimDurumu, MusteriDurum)
                    VALUES (@AracID, @MusteriID, @AlisTarihi, @TeslimTarihi, 
                            @GunlukUcret, @ToplamUcret, @Gorsel, @TeslimDurumu, 1)",
                                kiralama, transaction);

                        // Araç durumunu güncelle
                        conn.Execute(@"UPDATE Tbl_Araclar SET Durum = 'Dolu' 
                    WHERE AracID = @AracID",
                                new { AracID = kiralama.AracID }, transaction);

                        // Müşteri durumunu güncelle
                        conn.Execute(@"UPDATE Tbl_Musteriler SET MusteriDurum = 'Aktif Kiralama Var' 
                    WHERE MusteriID = @MusteriID",
                                new { MusteriID = kiralama.MusteriID }, transaction);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void KiralamaPasifYap(int kiralamaId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Kiralama kaydını pasif yap (MusteriDurum = 0)
                        conn.Execute(@"UPDATE Tbl_Kiralama SET MusteriDurum = 0 
                    WHERE KiralamaID = @KiralamaID",
                            new { KiralamaID = kiralamaId }, transaction);

                        // 2. Müşteri ID'sini al
                        var musteriId = conn.ExecuteScalar<int>(
                            "SELECT MusteriID FROM Tbl_Kiralama WHERE KiralamaID = @KiralamaID",
                            new { KiralamaID = kiralamaId }, transaction);

                        // 3. Arac ID'sini al
                        var aracId = conn.ExecuteScalar<int>(
                            "SELECT AracID FROM Tbl_Kiralama WHERE KiralamaID = @KiralamaID",
                            new { KiralamaID = kiralamaId }, transaction);

                        // 4. Müşteri durumunu güncelle (aktif kiralama var mı kontrol et)
                        bool aktifKiralamaVar = conn.ExecuteScalar<int>(
                            @"SELECT COUNT(*) FROM Tbl_Kiralama 
                    WHERE MusteriID = @MusteriID 
                    AND MusteriDurum = 1
                    AND TeslimDurumu NOT IN ('Teslim Edildi', 'İptal Edildi')",
                            new { MusteriID = musteriId }, transaction) > 0;

                        string yeniMusteriDurum = aktifKiralamaVar ? "Aktif Kiralama Var" : "Uygun";

                        conn.Execute(@"UPDATE Tbl_Musteriler SET MusteriDurum = @Durum 
                    WHERE MusteriID = @MusteriID",
                            new { Durum = yeniMusteriDurum, MusteriID = musteriId }, transaction);

                        // 5. Aracı boşa al
                        conn.Execute(@"UPDATE Tbl_Araclar SET Durum = 'Boş' 
                    WHERE AracID = @AracID",
                            new { AracID = aracId }, transaction);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
       
        public void Update(Kiralama kiralama)
        {
            using (var conn = GetConnection())
            {
                string sql = @"UPDATE Tbl_Kiralama SET
                        AracID = @AracID,
                        MusteriID = @MusteriID,
                        AlisTarihi = @AlisTarihi,
                        TeslimTarihi = @TeslimTarihi,
                        GunlukUcret = @GunlukUcret,
                        ToplamUcret = @ToplamUcret,
                        Gorsel = @Gorsel,
                        TeslimDurumu = @TeslimDurumu
                    WHERE KiralamaID = @KiralamaID";

                conn.Execute(sql, kiralama);
            }
        }

        public List<MarkaKiralamaIstatisk> GetMarkaBazliKiralamaIstatistik()
        {
            using (var conn = GetConnection())
            {
                string sql = @"
            SELECT A.Marka, COUNT(*) AS KiralanmaSayisi
            FROM Tbl_Kiralama K
            JOIN Tbl_Araclar A ON K.AracID = A.AracID
            GROUP BY A.Marka
            ORDER BY KiralanmaSayisi DESC";

                return conn.Query<MarkaKiralamaIstatisk>(sql).ToList();
            }
        }

        public List<TeslimDurumuIstatistik> GetTeslimDurumIstatistik()
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT TeslimDurumu, COUNT(*) AS Adet
                       FROM Tbl_Kiralama
                       GROUP BY TeslimDurumu";
                return conn.Query<TeslimDurumuIstatistik>(sql).ToList();
            }
        }
        public List<Kiralama> GetAllWithMusteri()
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT 
                K.KiralamaID,
                K.AracID,
                K.MusteriID,
                K.AlisTarihi,
                K.TeslimTarihi,
                K.GunlukUcret,
                K.ToplamUcret,
                K.Gorsel,
                K.TeslimDurumu,
                K.MusteriDurum,
                (M.Ad + ' ' + M.Soyad) AS AdSoyad
             FROM Tbl_Kiralama K
             INNER JOIN Tbl_Musteriler M ON K.MusteriID = M.MusteriID";


                return conn.Query<Kiralama>(sql).ToList();
            }
        }

        public bool MusterininAktifKiralamasiVar(int musteriId)
        {
            using (var conn = GetConnection())
            {
                // Önce müşteri aktif/pasif kontrolü (Tbl_Kiralama üzerinden)
                string kiralamaDurumSql = "SELECT MusteriDurum FROM Tbl_Kiralama WHERE MusteriID = @MusteriID";
                string kiralamaDurum = conn.ExecuteScalar<string>(kiralamaDurumSql, new { MusteriID = musteriId });

                if (kiralamaDurum == "Pasif")
                {
                    return true; // müşteri pasif → kiralama engellensin
                }

                // Eğer müşteri aktif → Tbl_Musteriler üzerinden uygunluk kontrolü
                string musteriDurumSql = "SELECT MusteriDurum FROM Tbl_Musteriler WHERE MusteriID = @MusteriID";
                string musteriDurum = conn.ExecuteScalar<string>(musteriDurumSql, new { MusteriID = musteriId });

                if (musteriDurum == "Aktif Kiralama Var")
                {
                    return true; // müşteri zaten araç kiralamış → yeni kiralama yapılamaz
                }

                // Eğer "Uygun" ise → sorun yok, yeni kiralama yapılabilir
                return false;
            }
        }




        public void KiralamaDurumGuncelle(int kiralamaId, string yeniDurum)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Kiralama durumunu güncelle
                        conn.Execute(@"UPDATE Tbl_Kiralama SET TeslimDurumu = @Durum 
                    WHERE KiralamaID = @KiralamaID",
                            new { Durum = yeniDurum, KiralamaID = kiralamaId }, transaction);

                        // Müşteri ID'sini al
                        var musteriId = conn.ExecuteScalar<int>(
                            "SELECT MusteriID FROM Tbl_Kiralama WHERE KiralamaID = @KiralamaID",
                            new { KiralamaID = kiralamaId }, transaction);

                        // Müşteri durumunu güncelle
                        bool aktifKiralamaVar = conn.ExecuteScalar<int>(
                            @"SELECT COUNT(*) FROM Tbl_Kiralama 
                    WHERE MusteriID = @MusteriID 
                    AND TeslimDurumu NOT IN ('Teslim Edildi', 'İptal Edildi')",
                            new { MusteriID = musteriId }, transaction) > 0;

                        string yeniMusteriDurum = aktifKiralamaVar ? "Aktif Kiralama Var" : "Uygun";

                        conn.Execute(@"UPDATE Tbl_Musteriler SET MusteriDurum = @Durum 
                    WHERE MusteriID = @MusteriID",
                            new { Durum = yeniMusteriDurum, MusteriID = musteriId }, transaction);

                        // Eğer teslim edildiyse aracı boşa al
                        if (yeniDurum == "Teslim Edildi")
                        {
                            var aracId = conn.ExecuteScalar<int>(
                                "SELECT AracID FROM Tbl_Kiralama WHERE KiralamaID = @KiralamaID",
                                new { KiralamaID = kiralamaId }, transaction);

                            conn.Execute(@"UPDATE Tbl_Araclar SET Durum = 'Boş' 
                        WHERE AracID = @AracID",
                                new { AracID = aracId }, transaction);
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

    }
}
