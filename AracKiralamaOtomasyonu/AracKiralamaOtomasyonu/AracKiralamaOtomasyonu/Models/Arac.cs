using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyonu.Models
{
    public class Arac
    {
        public int  AracID { get; set; }
        public string Plaka { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string ArabaGorsel { get; set; }
        public int Yil { get; set; }
        public string Renk { get; set; }
        public decimal GunlukUcret { get; set; }
        public string Durum { get; set; }
    }
}
