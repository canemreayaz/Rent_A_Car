using AracKiralamaOtomasyonu.Repository;
using Dapper;
using DevExpress.XtraBars.Ribbon;
using System.Data;

namespace AracKiralamaOtomasyonu.Models
{
    public class KullaniciKontrol : KullaniciRepository
    {
        public GirisDurumlari GirisYap(string kullaniciAdi, string sifre)
        {
            if (kullaniciAdi != null && sifre != null)
            {
                GirisDurumlari girisDurumlari = base.GirisYap(kullaniciAdi, sifre);

                return girisDurumlari;
            }
            else
            {
                return GirisDurumlari.eksikParametre; // Kullanıcı adı veya şifre boş ise başarısız olarak döndür
            }
        }
    }
}