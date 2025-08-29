using AracKiralamaOtomasyonu.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyonu.Repository
{
    public class ArabalarRepository : repository
    {
        public List<Arac> GetAll()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.Query<Arac>("SELECT * FROM Tbl_Araclar").ToList();
                }
            }
            catch
            {
                return new List<Arac>();
            }
        }

        public List<MarkaIstatistik> GetMarkaIstatistik()
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT Marka, COUNT(*) AS Adet 
                       FROM Tbl_Araclar 
                       GROUP BY Marka 
                       ORDER BY Adet DESC";

                return conn.Query<MarkaIstatistik>(sql).ToList();
            }
        }
        public List<DurumIstatistik> GetDurumIstatistikleri()
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT Durum, COUNT(*) AS DurumSayisi
                           FROM Tbl_Araclar
                           GROUP BY Durum";
                return conn.Query<DurumIstatistik>(sql).ToList();
            }
        }
        public int Add(Arac arac)
        {
            using (var conn = GetConnection())
            {
                string sql = @"INSERT INTO Tbl_Araclar (Plaka, Marka, Model, ArabaGorsel, Yil, Renk, GunlukUcret, Durum)
                       VALUES (@Plaka, @Marka, @Model, @ArabaGorsel, @Yil, @Renk, @GunlukUcret, @Durum)";
                return conn.Execute(sql, arac);
            }
        }
        public int Delete(int aracID)
        {
            using (var conn = GetConnection())
            {
                string sql = "DELETE FROM Tbl_Araclar WHERE AracID = @AracID";
                return conn.Execute(sql, new { AracID = aracID });
            }
        }

        public int Update(Arac arac)
        {
            using (var conn = GetConnection())
            {
                string sql = @"UPDATE Tbl_Araclar SET 
                        Plaka = @Plaka,
                        Marka = @Marka,
                        Model = @Model,
                        ArabaGorsel = @ArabaGorsel,
                        Yil = @Yil,
                        Renk = @Renk,
                        GunlukUcret = @GunlukUcret,
                        Durum = @Durum
                       WHERE AracID = @AracID";

                return conn.Execute(sql, arac);
            }
        }
        // Araç durumunu güncelle
        public void AracDurumGuncelle(int aracID, string yeniDurum)
        {
            using (var conn = GetConnection())
            {
                string sql = "UPDATE Tbl_Araclar SET Durum = @Durum WHERE AracID = @AracID";
                conn.Execute(sql, new { Durum = yeniDurum, AracID = aracID });
            }
        }

        // ID'ye göre araç getir
        public Arac GetById(int aracID)
        {
            using (var conn = GetConnection())
            {
                string sql = "SELECT * FROM Tbl_Araclar WHERE AracID = @AracID";
                return conn.QueryFirstOrDefault<Arac>(sql, new { AracID = aracID });
            }
        }



    }
}
