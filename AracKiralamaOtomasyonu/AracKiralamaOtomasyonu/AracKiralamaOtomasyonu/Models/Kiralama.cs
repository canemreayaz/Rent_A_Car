using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyonu.Models
{
    public class Kiralama
    {
        public int KiralamaID { get; set; }
        public int AracID { get; set; }
        public int MusteriID { get; set; }
        public DateTime AlisTarihi { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public decimal GunlukUcret { get; set; }
        public decimal ToplamUcret { get; set; }
        public string Gorsel { get; set; }           // DİKKAT: büyük harfli eşleşme
        public string TeslimDurumu { get; set; }
        public bool MusteriDurum { get; set; }       // SQL'de bit -> bool
        public string AdSoyad { get; set; }          // Ek alan (Ad + Soyad)
    }

}
