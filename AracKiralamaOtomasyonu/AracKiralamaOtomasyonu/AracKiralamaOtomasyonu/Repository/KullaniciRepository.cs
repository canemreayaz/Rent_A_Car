using AracKiralamaOtomasyonu.Models;
using Dapper;
using DevExpress.Charts.Native;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaOtomasyonu.Repository
{
    public class KullaniciRepository : repository
    {
        public List<Kullanici> GetAll()
        {
            using (var conn = GetConnection())
            {
                string sql = "SELECT * FROM Tbl_Kullanicilar";
                return conn.Query<Kullanici>(sql).ToList();
            }
        }

        public GirisDurumlari GirisYap(string kullaniciAdi, string sifre)
        {
            using (var conn = GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@KullaniciAdi", kullaniciAdi);
                parameters.Add("@Sifre", sifre);
                parameters.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                // Prosedürü hem kontrol hem veri çekmek için çalıştır
                var kullanici = conn.QueryFirstOrDefault<Int32>("sp_kullanicilar",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                //int result = parameters.Get<int>("ReturnValue");

                if (kullanici == 1)
                    return GirisDurumlari.basarili; // Giriş başarılıysa Kullanıcıyı döndür
                else
                    return GirisDurumlari.basarisiz; // Giriş başarısızsa null döndür
            }
        }

        public List<Kullanici> getGirisTablosu()
        {
            using (var conn = GetConnection())
            {
                string sql = "guvenlikSorusu_sp";

                var girisTablosuListesi = conn.Query<Kullanici>(
                    sql,
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return girisTablosuListesi;
            }
        }

        public GirisDurumlari KullaniciDogrula(string kullaniciAdi, string guvenlikSorusu, string guvenlikCevabi)
        {
            using (var conn = GetConnection())
            {
                string sql = @"SELECT COUNT(*) 
                       FROM Tbl_Kullanicilar
                       WHERE KullaniciAdi = @kullaniciAdi 
                         AND GuvenlikSorusu = @guvenlikSorusu 
                         AND GuvenlikCevabi = @guvenlikCevabi";

                int result = conn.ExecuteScalar<int>(sql, new
                {
                    kullaniciAdi,
                    guvenlikSorusu,
                    guvenlikCevabi
                });

                if (result == 1)
                    return GirisDurumlari.basarili;
                else
                    return GirisDurumlari.basarisiz;
            }
        }

        public string EmailAdresKontrolEt(string kullaniciAdi)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    string sql = "SELECT EmailAlani FROM Tbl_Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";
                    return conn.QueryFirstOrDefault<string>(sql, new { KullaniciAdi = kullaniciAdi });
                }
            }
            catch (Exception ex)
            {
                // Hata loglamak istiyorsan burada yap
                throw new Exception("E-posta kontrolü yapılırken hata oluştu", ex);
            }
        }

        public GirisDurumlari SifreDegistir(string kullaniciAdi, string yeniSifre)
        {
            using (var con =  GetConnection()) // Bağlantı stringini kendi projene göre güncelle
            {
                string sql = "UPDATE Tbl_Kullanicilar SET Sifre = @YeniSifre WHERE kullaniciAdi = @KullaniciAdi";
                int etkilenenSatir = con.Execute(sql, new
                {
                    YeniSifre = yeniSifre,
                    KullaniciAdi = kullaniciAdi
                });

                if (etkilenenSatir > 0)
                    return GirisDurumlari.basarili;
                else
                    return GirisDurumlari.basarisiz;
            }
        }

        public void Add(Kullanici kullanici)
        {
            using (var conn = GetConnection())
            {
                string sql = @"INSERT INTO Tbl_Kullanicilar
                       (KullaniciAdi, Sifre, Yetki,  EmailAlani, GuvenlikSorusu, GuvenlikCevabi)
                       VALUES (@KullaniciAdi, @Sifre, @Yetki, @EmailAlani, @GuvenlikSorusu, @GuvenlikCevabi)";

                conn.Execute(sql, kullanici); // Dapper, otomatik olarak eşleştirir
            }
        }


        public void Delete(int kullaniciid)
        {
            using (var conn = GetConnection())
            {
                string sql = "DELETE FROM Tbl_Kullanicilar WHERE KullaniciID = @Id";
                conn.Execute(sql, new { Id = kullaniciid });
            }
        }

        public void Update(Kullanici kullanici)
        {
            using (var conn = GetConnection())
            {
                string sql = @"UPDATE Tbl_Kullanicilar SET 
                           KullaniciAdi = @KullaniciAdi,
                           Sifre = @Sifre,
                           Yetki = @Yetki,
                           EmailAlani = @EmailAlani,
                           GuvenlikSorusu = @GuvenlikSorusu,
                           GuvenlikCevabi = @GuvenlikCevabi
                           
                       WHERE KullaniciID = @KullaniciID";

                conn.Execute(sql, kullanici);
            }
        }

        
        public Kullanici GirisYapanKullanici(string kullaniciAdi, string sifre)
        {
            using (var conn = GetConnection())
            {
                string sql = "SELECT * FROM Tbl_Kullanicilar WHERE KullaniciAdi = @kullaniciAdi AND Sifre = @sifre";
                return conn.QueryFirstOrDefault<Kullanici>(sql, new { kullaniciAdi, sifre });
            }
         }
        

    }
}

