using AracKiralamaOtomasyonu.Models;
using AracKiralamaOtomasyonu.Repository;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaOtomasyonu.FormSayfalari
{
    public partial class frm_ToplamArabaIstatiskleri : Form
    {
        public frm_ToplamArabaIstatiskleri()
        {
            InitializeComponent();
        }
        ArabalarRepository repo   = new ArabalarRepository();
        private void frm_ToplamArabaIstatiskleri_Load(object sender, EventArgs e)
        {
            AracMarkaGrafik();
            DurumSayilariniYukle();
        }

        void AracMarkaGrafik()
        {
            List<MarkaIstatistik> markaIstatistikler = repo.GetMarkaIstatistik();

            chartControl1.Series.Clear(); // Önce grafik temizlenir
            Series seri = new Series("Markalara Göre Araç Sayısı", ViewType.Bar); // İstersen ViewType.Pie da kullanabilirsin

            foreach (var item in markaIstatistikler)
            {
                seri.Points.Add(new SeriesPoint(item.Marka, item.Adet));
            }

            chartControl1.Series.Add(seri);
            chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True; // İsteğe bağlı
        }

        private void DurumSayilariniYukle()
            {
           
                var durumlar = repo.GetDurumIstatistikleri();

                // Label'ları sıfırla
                lbl_doluAracSayisi.Text = "0";
                lbl_bosAracSayisi.Text = "0";
                lbl_arizaliAracSayisi.Text = "0";

                // Gelen değerlere göre güncelle
                foreach (var d in durumlar)
                {
                    switch (d.Durum)
                    {
                        case "Dolu":
                            lbl_doluAracSayisi.Text = d.DurumSayisi.ToString();
                            break;
                        case "Boş":
                            lbl_bosAracSayisi.Text = d.DurumSayisi.ToString();
                            break;
                        case "Arızalı":
                            lbl_arizaliAracSayisi.Text = d.DurumSayisi.ToString();
                        break;
                    }      
                }
            }
        
    
    }
}
