using AracKiralamaOtomasyonu.Models;
using DevExpress.XtraEditors;
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
    public partial class frm_ArabaListesi : Form
    {
        Repository.ArabalarRepository repo = new Repository.ArabalarRepository();
        List<Arac> arac = new List<Arac>();
        public frm_ArabaListesi()
        {
            InitializeComponent();
        }

        private void frm_ArabaListesi_Load(object sender, EventArgs e)
        {
            grd_ArabaListesi.DataSource = repo.GetAll();

        }
        void AracGetir()
        {
            arac = repo.GetAll();
            grd_ArabaListesi.DataSource = null;
            grd_ArabaListesi.DataSource = arac;
        }
        void temizle()
        {
            txt_ArackiralamaID.Text = "";
            txt_AracPlaka.Text = "";
            txt_ArabaMarka.Text = null;
            txt_Arabamodel.Text = null;
            txt_ArabaGorsel.Text = null;
            dateEdit_Yil.DateTime = DateTime.MinValue;
            txt_renk.Text = "";
            txt_gunlukUcret.Text = "";
            comboBox_DoluBosDurumu.Text = null;
            
        }


        void ekle()
        {
            if (!decimal.TryParse(txt_gunlukUcret.Text, out decimal ucret))
            {
                XtraMessageBox.Show("Geçerli bir ücret giriniz.");
                return;
            }

            Arac arac = new Arac
            {
                Plaka = txt_AracPlaka.Text,
                Marka = txt_ArabaMarka.Text,
                Model = txt_Arabamodel.Text,
                ArabaGorsel = string.IsNullOrWhiteSpace(txt_ArabaGorsel.Text) ? "YOK" : txt_ArabaGorsel.Text,
                Yil = dateEdit_Yil.DateTime.Year,
                Renk = txt_renk.Text,
                GunlukUcret = ucret,
                Durum = comboBox_DoluBosDurumu.Text
            };

            try
            {
                repo.Add(arac);
                XtraMessageBox.Show("Araç başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AracGetir();
                temizle();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            
            ekle();
            temizle();
        }

        void sil()
        {
            // ID boşsa işlem yapma
            if (string.IsNullOrWhiteSpace(txt_ArackiralamaID.Text))
            {
                XtraMessageBox.Show("Lütfen silinecek aracı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int aracID = Convert.ToInt32(txt_ArackiralamaID.Text);

            DialogResult result = XtraMessageBox.Show("Bu aracı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    repo.Delete(aracID);
                    XtraMessageBox.Show("Araç başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AracGetir(); // Grid'i yenile
                    temizle();   // Textboxları temizle
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Silme işleminde hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btn_sil_Click(object sender, EventArgs e)
        {
            sil();
            temizle();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            // Seçilen satırın indexi
            int selectedRow = gridView1.FocusedRowHandle;

            if (selectedRow >= 0)
            {
                txt_AracPlaka.Text = gridView1.GetRowCellValue(selectedRow, "Plaka")?.ToString();
                txt_ArabaMarka.Text = gridView1.GetRowCellValue(selectedRow, "Marka")?.ToString();
                txt_Arabamodel.Text = gridView1.GetRowCellValue(selectedRow, "Model")?.ToString();
                txt_ArabaGorsel.Text = gridView1.GetRowCellValue(selectedRow, "ArabaGorsel")?.ToString();
                txt_renk.Text = gridView1.GetRowCellValue(selectedRow, "Renk")?.ToString();
                txt_gunlukUcret.Text = gridView1.GetRowCellValue(selectedRow, "GunlukUcret")?.ToString();
                comboBox_DoluBosDurumu.Text = gridView1.GetRowCellValue(selectedRow, "Durum")?.ToString();

                // Yıl için DateEdit'e sadece yıl değeri ver
                if (int.TryParse(gridView1.GetRowCellValue(selectedRow, "Yil")?.ToString(), out int yil))
                {
                    dateEdit_Yil.DateTime = new DateTime(yil, 1, 1);
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Seçilen satırın indexi
            int selectedRow = gridView1.FocusedRowHandle;

            if (selectedRow >= 0)
            {
                txt_ArackiralamaID.Text = gridView1.GetRowCellValue(selectedRow, "AracID")?.ToString();
                txt_AracPlaka.Text = gridView1.GetRowCellValue(selectedRow, "Plaka")?.ToString();
                txt_ArabaMarka.Text = gridView1.GetRowCellValue(selectedRow, "Marka")?.ToString();
                txt_Arabamodel.Text = gridView1.GetRowCellValue(selectedRow, "Model")?.ToString();
                txt_ArabaGorsel.Text = gridView1.GetRowCellValue(selectedRow, "ArabaGorsel")?.ToString();
                txt_renk.Text = gridView1.GetRowCellValue(selectedRow, "Renk")?.ToString();
                txt_gunlukUcret.Text = gridView1.GetRowCellValue(selectedRow, "GunlukUcret")?.ToString();
                comboBox_DoluBosDurumu.Text = gridView1.GetRowCellValue(selectedRow, "Durum")?.ToString();

                // Yıl için DateEdit'e sadece yıl değeri ver
                if (int.TryParse(gridView1.GetRowCellValue(selectedRow, "Yil")?.ToString(), out int yil))
                {
                    dateEdit_Yil.DateTime = new DateTime(yil, 1, 1);
                }
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_ArackiralamaID.Text))
            {
                XtraMessageBox.Show("Lütfen güncellenecek aracı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Arac arac = new Arac();
            arac.AracID = Convert.ToInt32(txt_ArackiralamaID.Text);
            arac.Plaka = txt_AracPlaka.Text;
            arac.Marka = txt_ArabaMarka.Text;
            arac.Model = txt_Arabamodel.Text;
            arac.ArabaGorsel = txt_ArabaGorsel.Text;
            arac.Yil = dateEdit_Yil.DateTime.Year;
            arac.Renk = txt_renk.Text;
            arac.GunlukUcret = Convert.ToDecimal(txt_gunlukUcret.Text);
            arac.Durum = comboBox_DoluBosDurumu.Text;

            try
            {
                repo.Update(arac);
                XtraMessageBox.Show("Araç bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AracGetir();  // Grid'i yenile
                temizle();    // Textboxları temizle
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Güncelleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            AracGetir();
        }
    }
}
