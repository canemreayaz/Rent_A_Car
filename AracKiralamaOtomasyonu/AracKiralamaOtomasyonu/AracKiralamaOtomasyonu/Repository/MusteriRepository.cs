using AracKiralamaOtomasyonu.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaOtomasyonu.Repository
{
    public class MusteriRepository : repository
    {
        // Tbl_Musteriler tablosundaki tüm satırları, C# sınıfına dönüştürerek döner
        public List<Musteri> GetAll()
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT 
            MusteriID, 
            TC, 
            Ad, 
            Soyad, 
            Telefon, 
            Email, 
            EhliyetNo, 
            EhliyetTarihi,
            ISNULL(MusteriDurum, '1') AS MusteriDurum
          FROM Tbl_Musteriler";
                return conn.Query<Musteri>(sql).ToList();
            }
        }

        public void Add(Musteri musteri)
        {
            using (var conn = GetConnection())
            {
                string sql = @"INSERT INTO Tbl_Musteriler 
               (TC, Ad, Soyad, Telefon, Email, EhliyetNo, EhliyetTarihi)
               VALUES (@TC, @Ad, @Soyad, @Telefon, @Email, @EhliyetNo, @EhliyetTarihi)";
                conn.Execute(sql, musteri);
            }
        }
        
        public void Delete(int musteriId)
        {
            using (var conn = GetConnection())
            {
                string sql = "DELETE FROM Tbl_Musteriler WHERE MusteriID = @Id";
                conn.Execute(sql, new { Id = musteriId });
            }
        }
        

        public void Update(Musteri musteri)
        {
            using (var conn = GetConnection())
            {
                string sql = @"UPDATE Tbl_Musteriler SET 
                   TC = @TC,
                   Ad = @Ad,
                   Soyad = @Soyad,
                   Telefon = @Telefon,
                   Email = @Email,
                   EhliyetNo = @EhliyetNo,
                   EhliyetTarihi = @EhliyetTarihi
                  
               WHERE MusteriID = @MusteriID";
                conn.Execute(sql, musteri);
            }
        }

        public List<MusteriAracKiralama> GetMusteriAracKiralamaListesi()
        {
            using (var conn = GetConnection())
            {
                string sql = @"
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
                    ORDER BY m.Ad, k.AlisTarihi DESC";

                return conn.Query<MusteriAracKiralama>(sql).ToList();
            }
        }


        public List<RaporModel> GetMusteriAracKiralamaListesiByID(int musteriId)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
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
                    WHERE m.MusteriID = @musteriId
                    ORDER BY k.AlisTarihi DESC";


                return conn.Query<RaporModel>(sql, new { musteriId }).ToList();
            }
        }


        public List<Musteri> GetLookUpMusteriler()
        {
            using (var conn = GetConnection())
            {
                string sql = "SELECT *  FROM Tbl_Musteriler";
                return conn.Query<Musteri>(sql).ToList();
            }
        }


        public Musteri GetMusteriByID(int musteriID)
        {
            using (var conn = GetConnection())
            {
                string sql = "SELECT MusteriID, Ad, Soyad, Telefon, EMail FROM Tbl_Musteriler WHERE MusteriID = @ID";
                return conn.QueryFirstOrDefault<Musteri>(sql, new { ID = musteriID });
            }
        }

        public void MusteriDurumGuncelle(int musteriId, string currentTeslimDurumu = null)
        {
            using (var conn = GetConnection())
            {
                // Tüm aktif kiralamaları getir (Teslim Edilmemiş olanlar)
                var kiralamalar = conn.Query<string>(
                    @"SELECT TeslimDurumu FROM Tbl_Kiralama 
              WHERE MusteriID = @MusteriID 
              AND TeslimDurumu != 'Teslim Edildi'",
                    new { MusteriID = musteriId }).ToList();

                // Eğer geçerli bir durum belirtilmişse ve Teslim Edilmemişse listeye ekle
                if (!string.IsNullOrEmpty(currentTeslimDurumu) && currentTeslimDurumu != "Teslim Edildi")
                {
                    kiralamalar.Add(currentTeslimDurumu);
                }

                string yeniDurum = "Uygun"; // Varsayılan durum

                // Öncelik sırası: Gecikmeli > Kiralık > İptal Edildi
                if (kiralamalar.Contains("Gecikmeli"))
                {
                    yeniDurum = "Gecikti";
                }
                else if (kiralamalar.Contains("Kiralık"))
                {
                    yeniDurum = "Aktif Kiralama Var";
                }
                else if (kiralamalar.Contains("İptal Edildi"))
                {
                    yeniDurum = "Sipariş İptal Edildi";
                }

                conn.Execute(
                    @"UPDATE Tbl_Musteriler 
              SET MusteriDurum = @Durum 
              WHERE MusteriID = @MusteriID",
                    new { Durum = yeniDurum, MusteriID = musteriId });
            }
        }




    }
}
