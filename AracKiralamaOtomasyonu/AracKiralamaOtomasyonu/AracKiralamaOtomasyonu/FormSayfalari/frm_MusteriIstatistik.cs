using AracKiralamaOtomasyonu.Models;
using AracKiralamaOtomasyonu.Repository;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
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
    public partial class frm_MusteriIstatistik : Form
    {
        private MusteriRepository repo;
        public frm_MusteriIstatistik()
        {
            InitializeComponent();
            repo = new MusteriRepository();
        }

        private void frm_MusteriIstatistik_Load(object sender, EventArgs e)
        {
            // Veriyi çek
            var liste = repo.GetMusteriAracKiralamaListesi();

            // GridControl'e ata
            gridControl_Kiralama.DataSource = liste;
            gridView_Kiralama.BestFitColumns();

            // Grafik yükle
            LoadChartData(liste);

            MusteriSecimListesiYukle();

        }

        private void LoadChartData(List<MusteriAracKiralama> liste)
        {
            var data = liste
                .GroupBy(x => $"{x.Ad}-{x.MusteriID}")
                .Select(g => new { Musteri = g.Key, KiralamaSayisi = g.Count() })
                .ToList();

            chartControl_MarkaAnaliz.Series.Clear();

            var series = new DevExpress.XtraCharts.Series("Kiralama Sayısı", ViewType.Bar);

            series.ArgumentDataMember = "Musteri";
            series.ValueDataMembers.AddRange(new[] { "KiralamaSayisi" });

            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series.Label.TextPattern = "{V:N0}"; // küsüratsız gösterim

            chartControl_MarkaAnaliz.Series.Add(series);
            chartControl_MarkaAnaliz.DataSource = data;
            chartControl_MarkaAnaliz.RefreshData();
        }


        private void btn_IstatistikGetir_Click(object sender, EventArgs e)
        {
            if (glue_MusteriSecim.EditValue == null)
            {
                MessageBox.Show("Lütfen bir müşteri seçiniz.");
                return;
            }

            int secilenMusteriID = Convert.ToInt32(glue_MusteriSecim.EditValue);
            var liste = repo.GetMusteriAracKiralamaListesi()
                            .Where(x => x.MusteriID == secilenMusteriID)
                            .ToList();

            gridControl_Kiralama.DataSource = liste;
            gridView_Kiralama.BestFitColumns();
            LoadChartData(liste);

            string adSoyad = liste.FirstOrDefault()?.Ad + " " + liste.FirstOrDefault()?.Soyad + " - " + liste.FirstOrDefault()?.MusteriID;
            lbl_Durum.Text = $"{adSoyad} Kişisinin Verileri Gösteriliyor...";
        }

        private void MusteriSecimListesiYukle()
        {
            var liste = repo.GetAll();
            glue_MusteriSecim.Properties.DataSource = liste;
            glue_MusteriSecim.Properties.DisplayMember = "Ad";
            glue_MusteriSecim.Properties.ValueMember = "MusteriID";
            glue_MusteriSecim.Properties.NullText = "Müşteri Seçin...";

            glue_MusteriSecim.Properties.PopupView.Columns.Clear();
            glue_MusteriSecim.Properties.PopupView.Columns.AddVisible("MusteriID", "ID");
            glue_MusteriSecim.Properties.PopupView.Columns.AddVisible("Ad", "Ad");
            glue_MusteriSecim.Properties.PopupView.Columns.AddVisible("Soyad", "Soyad");
        }

        private void btn_TumunuGoster_Click(object sender, EventArgs e)
        {
            var tumListe = repo.GetMusteriAracKiralamaListesi();
            gridControl_Kiralama.DataSource = tumListe;
            gridView_Kiralama.BestFitColumns();
            LoadChartData(tumListe);

            glue_MusteriSecim.EditValue = null;
            lbl_Durum.Text = "Tüm Müşteriler Listeleniyor...";
        }
   
    
    }
}
