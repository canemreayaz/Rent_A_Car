using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyonu.Models
{
    public class Musteri
    {
        public int MusteriID { get; set; }
        public string TC { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string EhliyetNo { get; set; }
        public DateTime EhliyetTarihi { get; set; }
       
        public string MusteriDurum { get; set; } // Yeni eklenen property
        public string AdSoyad => Ad + " " + Soyad;

    }
}
