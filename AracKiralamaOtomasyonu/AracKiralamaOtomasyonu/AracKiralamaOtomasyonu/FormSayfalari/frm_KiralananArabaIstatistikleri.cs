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
    public partial class frm_KiralananArabaIstatistikleri : Form
    {
        public frm_KiralananArabaIstatistikleri()
        {
            InitializeComponent();
        }
        KiralamaRepository repo = new KiralamaRepository();
        private void frm_KiralananArabaIstatistikleri_Load(object sender, EventArgs e)
        {
            
            var istatistikler = repo.GetMarkaBazliKiralamaIstatistik();

            chartControl1.Series.Clear();

            Series seri = new Series("Kiralama Dağılımı", ViewType.Pie);

            foreach (var item in istatistikler)
            {
                seri.Points.Add(new SeriesPoint(item.Marka, item.KiralanmaSayisi));
            }

            // Etiketleri göster
            PieSeriesLabel label = (PieSeriesLabel)seri.Label;
            label.TextPattern = "{A}: {V} kez";
            label.Visible = true;
            label.Position = PieSeriesLabelPosition.TwoColumns;

            // Pasta dilimlerini ayır
            PieSeriesView view = (PieSeriesView)seri.View;
            view.RuntimeExploding = true;

            // Grafiğe ekle
            chartControl1.Series.Clear();
            chartControl1.Series.Add(seri);
            chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            TeslimDurumIstatistikYukle(); // Teslim durum istatistiklerini yükle

        }

        void TeslimDurumIstatistikYukle()
        {
            var istatistikler = repo.GetTeslimDurumIstatistik(); // KiralamaRepository içindeki fonksiyon

            // Varsayılan 0 atayalım, sonra döngü ile güncelleyelim
            lbl_teslimEdildi.Text = "0";
            lbl_teslimEdilmedi.Text = "0";
            lbl_gecTeslimEdildi.Text = "0";
            lbl_iptalEdildi.Text = "0";

            foreach (var item in istatistikler)
            {
                switch (item.TeslimDurumu)
                {
                    case "Teslim Edildi":
                        lbl_teslimEdildi.Text = item.Adet.ToString();
                        break;
                    case "Kiralık":
                        lbl_teslimEdilmedi.Text = item.Adet.ToString();
                        break;
                    case "Gecikmeli":
                        lbl_gecTeslimEdildi.Text = item.Adet.ToString();
                        break;
                    case "İptal Edildi":
                        lbl_iptalEdildi.Text = item.Adet.ToString();
                        break;
                }
            }
        }

      
    }
}
