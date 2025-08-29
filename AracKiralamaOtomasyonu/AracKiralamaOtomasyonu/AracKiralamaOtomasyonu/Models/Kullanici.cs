using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyonu.Models
{
    public class Kullanici
    {
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Yetki { get; set; }
        public string EmailAlani { get; set; }

        public string GuvenlikSorusu { get; set; }
        public string GuvenlikCevabi { get; set; }
    }
}
