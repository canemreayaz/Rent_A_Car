using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyonu.Models
{
    public class Fatura
    {
       public int FaturaID { get; set; }
    public int MusteriID { get; set; }
    public DateTime Tarih { get; set; }  // tabloyla aynı olsun
    public int Yil { get; set; }
    public string DosyaYolu { get; set; } // tabloyla aynı olsun
    }

}
