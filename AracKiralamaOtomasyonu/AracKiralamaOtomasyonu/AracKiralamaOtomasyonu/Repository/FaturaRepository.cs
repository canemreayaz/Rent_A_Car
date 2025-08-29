using AracKiralamaOtomasyonu.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AracKiralamaOtomasyonu.Repository
{
    public class FaturaRepository
    {
        private readonly string _connectionString;

        public FaturaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public FaturaRepository() : base()
        {

        }

        public List<Fatura> FaturalariGetir()
        {
            using (var conn = GetConnection())
            {
                return conn.Query<Fatura>("SELECT * FROM Tbl_Faturalar").ToList();
            }
        }
        private IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Yeni fatura ekler.
        /// </summary>
        public void FaturaEkle(int musteriId, DateTime tarih, int yil, string dosyaYolu)
        {
            using (var conn = GetConnection())
            {
                conn.Execute(@"
                    INSERT INTO Tbl_Faturalar (MusteriID, Tarih, Yil, DosyaYolu) 
                    VALUES (@MusteriID, @Tarih, @Yil, @DosyaYolu)",
                    new { MusteriID = musteriId, Tarih = tarih, Yil = yil, DosyaYolu = dosyaYolu });
            }
        }

        /// <summary>
        /// Yıla göre (ve opsiyonel olarak tarihe göre) faturaları getirir.
        /// Ayrıca müşteri ad + soyad bilgisini de döner.
        /// </summary>
        public List<Fatura> FaturalariGetir(int yil, DateTime? tarih = null)
        {
            using (var conn = GetConnection())
            {
                string sql = @"
                    SELECT f.FaturaID, f.MusteriID, f.Tarih, f.Yil, f.DosyaYolu,
                           m.Adi + ' ' + m.Soyadi AS MusteriAdiSoyadi
                    FROM Tbl_Faturalar f
                    INNER JOIN Tbl_Musteriler m ON f.MusteriID = m.MusteriID
                    WHERE f.Yil = @Yil 
                      AND (@Tarih IS NULL OR CAST(f.Tarih AS DATE) = CAST(@Tarih AS DATE))";

                return conn.Query<Fatura>(sql, new { Yil = yil, Tarih = tarih }).ToList();
            }
        }
    }
}
