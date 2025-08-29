using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyonu.Models
{
    public class MusteriAracKiralama
    {
        public int MusteriID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
              
        public string Marka { get; set; }
        public string Model { get; set; }
        public DateTime AlisTarihi { get; set; }
        public DateTime TeslimTarihi { get; set; }



    }

}
