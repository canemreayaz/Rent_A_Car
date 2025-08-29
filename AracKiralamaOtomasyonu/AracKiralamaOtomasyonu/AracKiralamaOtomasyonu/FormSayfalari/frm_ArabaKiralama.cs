using AracKiralamaOtomasyonu.Models;
using AracKiralamaOtomasyonu.Repository;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Build.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Transactions;
using System.Windows.Forms;
using static DevExpress.Utils.Svg.CommonSvgImages;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;




namespace AracKiralamaOtomasyonu.FormSayfalari 
{
    public partial class frm_ArabaKiralama : Form
    {
        List<Kiralama> kiralama = new List<Kiralama>();
        List<Arac> arac = new List<Arac>();
        List<Musteri> musteri = new List<Musteri>();

        KiralamaRepository repo = new KiralamaRepository();
        ArabalarRepository aracRepo = new ArabalarRepository();
        MusteriRepository musteriRepo = new MusteriRepository();

        public frm_ArabaKiralama()
        {
            InitializeComponent();
        }

        private void btn_yenile_Click(object sender, EventArgs e)
        {
            // 1️⃣ Araçları güncelle
            arac = aracRepo.GetAll();
            gridControl1.DataSource = null;
            gridControl1.DataSource = arac;
            gridView1.RefreshData();

            // 2️⃣ Müşterileri güncelle
            musteri = musteriRepo.GetAll();
            gridControl1.DataSource = null;
            gridControl1.DataSource = musteri;
            gridView1.RefreshData();

            // 3️⃣ Kiralamaları güncelle
            kiralama = repo.GetAllWithMusteri();
            gridControl1.DataSource = null;
            gridControl1.DataSource = kiralama;
            gridView1.RefreshData();
        }


        // Kiralama listesini DataGrid'e yükler

        void Arackiralama()
        {
            try
            {
                var kiralamaListesi = repo.GetAllWithMusteri();
                gridControl1.DataSource = null;
                gridControl1.DataSource = kiralamaListesi;
                gridControl1.RefreshDataSource();
              //  gridView1.PopulateColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri listesi yüklenirken hata oluştu: " + ex.Message);
            }
        }

        // Araba listesini gridLookUpEdit'e yükler
        void AraclariGetir()
        {
            try
            {
                var araclar = aracRepo.GetAll();
                grd_AracSec.Properties.DataSource = araclar.ToList();
                grd_AracSec.Properties.DisplayMember = "Plaka";
                grd_AracSec.Properties.ValueMember = "AracID";
                grd_AracSec.Properties.PopupFormSize = new Size(800, 400);

                var view = grd_AracSec.Properties.PopupView as GridView;
                if (view != null)
                {
                    view.PopulateColumns();

                    // Kolon varlığını kontrol ederek ayarla
                    SetColumnWidthIfExists(view, "AracID", 60);
                    SetColumnWidthIfExists(view, "Plaka", 80);
                    SetColumnWidthIfExists(view, "Marka", 90);
                    // Diğer kolonlar...

                    view.OptionsView.ColumnAutoWidth = false;
                    view.Appearance.Row.Font = new Font("Tahoma", 8.5f);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SetColumnWidthIfExists(GridView view, string columnName, int width)
        {
            var column = view.Columns[columnName];
            if (column != null)
            {
                column.Width = width;
            }
        }

        void MusterileriGetir()
        {
            try
            {
                if (musteriRepo == null)
                    throw new Exception("Repository başlatılmadı");

                var musteriler = musteriRepo.GetAll().ToList();
                if (!musteriler.Any())
                    return;

                // GridView oluştur
                GridView view = new GridView();
                view.OptionsView.ShowAutoFilterRow = true;
                view.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                view.OptionsSelection.EnableAppearanceFocusedCell = false;
                view.OptionsView.ShowGroupPanel = false;

                // Kolonlar
                view.Columns.AddVisible("MusteriID", "ID");
                view.Columns.AddVisible("TC", "TC Kimlik No");
                view.Columns.AddVisible("Ad", "Ad");
                view.Columns.AddVisible("Soyad", "Soyad");
                view.Columns.AddVisible("Telefon", "Telefon");
                view.Columns.AddVisible("Email", "E-Posta");
                view.Columns.AddVisible("EhliyetNo", "Ehliyet No");
                view.Columns.AddVisible("EhliyetTarihi", "Ehliyet Tarihi");
                view.Columns.AddVisible("MusteriDurum", "Durum");

                // Grid'i bağla
                grd_MusteriSec.Properties.View = view;
                grd_MusteriSec.Properties.DataSource = musteriler;
                grd_MusteriSec.Properties.DisplayMember = "AdSoyad";
                grd_MusteriSec.Properties.ValueMember = "MusteriID";

                // Popup boyutu
                grd_MusteriSec.Properties.PopupFormSize = new Size(800, 400);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void temizle()
        {
            try
            {
                txt_kiralamaID.Text = "";
                grd_AracSec.EditValue = null;
                grd_MusteriSec.EditValue = null;
                date_alisTarihi.DateTime = DateTime.Now;
                date_teslimTarihi.DateTime = DateTime.Now.AddDays(1);
                txt_gunlukUcret.Text = "";
                txt_toplamUcret.Text = "";
                pictureBox1.Text = "";
                comboBox_teslimDurumu.SelectedIndex = 0;
            }
            catch
            {
                // Hata durumunda sessizce devam et
            }
        }

        // Form yüklendiğinde çağrılır
        private bool formYukleniyor = true; // Yeni değişken

        private void frm_ArabaKiralama_Load(object sender, EventArgs e)
        {
            formYukleniyor = true;

            // Teslim durumu seçenekleri
            comboBox_teslimDurumu.Properties.Items.AddRange(new string[]
            {
                "Kiralık",
                "Teslim Edildi",
                "İptal Edildi",
                "Gecikmeli"
            });

        }

        // Listele butonu
        private void btn_listele_Click(object sender, EventArgs e)
        {
            Arackiralama();
            MusterileriGetir();

        }

        private void frm_ArabaKiralama_Shown(object sender, EventArgs e)
        {
            try
            {
               Arackiralama();
                AraclariGetir();
               MusterileriGetir();

                formYukleniyor = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Form yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void btn_ekle_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Zorunlu alan kontrolleri
                if (grd_MusteriSec.EditValue == null)
                {
                    XtraMessageBox.Show("Müşteri seçmelisiniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (grd_AracSec.EditValue == null)
                {
                    XtraMessageBox.Show("Araç seçmelisiniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int musteriId = Convert.ToInt32(grd_MusteriSec.EditValue);
                int aracId = Convert.ToInt32(grd_AracSec.EditValue);

                // Müşteri kontrolü
                if (repo.MusterininAktifKiralamasiVar(musteriId))
                {
                    XtraMessageBox.Show("Bu müşterinin zaten aktif kiralaması var!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Araç durum kontrolü
                var arac = aracRepo.GetById(aracId);
                if (arac.Durum != "Boş")
                {
                    XtraMessageBox.Show("Seçilen araç kiralanamaz durumda!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tarih kontrolleri
                if (date_alisTarihi.DateTime >= date_teslimTarihi.DateTime)
                {
                    XtraMessageBox.Show("Teslim tarihi alış tarihinden sonra olmalıdır!", "Geçersiz Tarih", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ücret kontrolleri
                if (!decimal.TryParse(txt_gunlukUcret.Text, out decimal gunlukUcret) || gunlukUcret <= 0)
                {
                    XtraMessageBox.Show("Geçerli bir günlük ücret giriniz!", "Geçersiz Ücret", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiralama nesnesi oluştur
                Kiralama yeniKiralama = new Kiralama
                {
                    AracID = aracId,
                    MusteriID = musteriId,
                    AlisTarihi = date_alisTarihi.DateTime,
                    TeslimTarihi = date_teslimTarihi.DateTime,
                    GunlukUcret = gunlukUcret,
                    ToplamUcret = (date_teslimTarihi.DateTime - date_alisTarihi.DateTime).Days * gunlukUcret,
                    Gorsel = Convert.ToString(pictureBox1.Tag),
                    TeslimDurumu = comboBox_teslimDurumu.SelectedItem?.ToString() ?? "Kiralık",
                    MusteriDurum = true
                };

                // Veritabanı işlemleri
                repo.Add(yeniKiralama);
                aracRepo.AracDurumGuncelle(aracId, "Dolu");

                XtraMessageBox.Show("Kiralama başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Listeleri yenile
                Arackiralama();
                AraclariGetir();
                MusterileriGetir();
                temizle();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void sil()
        {

            if (gridView1.GetFocusedRowCellValue("KiralamaID") == null)
            {
                XtraMessageBox.Show("Lütfen bir müşteri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int musteriId = Convert.ToInt32(gridView1.GetFocusedRowCellValue("KiralamaID"));

            DialogResult result = XtraMessageBox.Show("Bu müşteriyi pasif hale getirmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            try
            {

                repo.KiralamaPasifYap(musteriId); // MusteriDurum = false yapan Dapper metodu

                XtraMessageBox.Show("Müşteri pasif hale getirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Arackiralama(); // grid yeniden yüklenir ve tik kalkar
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hata: " + ex.Message);
            }
        }
        private void btn_sil_Click(object sender, EventArgs e)
        {
            sil();

        }
       
       
        private bool gridTiklamaModu = false; // Yeni kontrol değişkeni
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridTiklamaModu = true; // Grid'den tıklama yapıldığını işaretle

            try
            {
                if (e.FocusedRowHandle >= 0 && !formYukleniyor)
                {
                    var row = gridView1.GetRow(e.FocusedRowHandle) as Kiralama;
                    if (row != null)
                    {
                        
                        txt_kiralamaID.Text = row.KiralamaID.ToString();
                        
                        grd_AracSec.EditValue = row.AracID;
                        grd_MusteriSec.EditValue = row.MusteriID;
                        date_alisTarihi.DateTime = row.AlisTarihi;
                        date_teslimTarihi.DateTime = row.TeslimTarihi;
                        txt_gunlukUcret.Text = row.GunlukUcret.ToString("0.00");
                        txt_toplamUcret.Text = row.ToplamUcret.ToString("0.00");
                        pictureBox1.Text = row.Gorsel;
                        
                        comboBox_teslimDurumu.SelectedItem = row.TeslimDurumu;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Satır değişikliği sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                gridTiklamaModu = false; // İşlem tamamlandı
            }

        }
       
        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_kiralamaID.Text))
            {
                XtraMessageBox.Show("Lütfen güncellenecek bir kayıt seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Kiralama kiralama = new Kiralama();
            kiralama.KiralamaID = Convert.ToInt32(txt_kiralamaID.Text);
            kiralama.AracID = Convert.ToInt32(grd_AracSec.EditValue);
            kiralama.MusteriID = Convert.ToInt32(grd_MusteriSec.EditValue);
            kiralama.AlisTarihi = date_alisTarihi.DateTime;
            kiralama.TeslimTarihi = date_teslimTarihi.DateTime;
            kiralama.GunlukUcret = decimal.Parse(txt_gunlukUcret.Text);
            kiralama.ToplamUcret = decimal.Parse(txt_toplamUcret.Text.Replace("₺", "").Replace(".", ""));
            kiralama.Gorsel = pictureBox1.Text;
            kiralama.TeslimDurumu = comboBox_teslimDurumu.SelectedItem?.ToString();

            try
            {
                // Transaction başlat
                using (var transaction = new TransactionScope())
                {
                    // Teslim Durumuna göre araç durumu güncelle
                    if (kiralama.TeslimDurumu == "Teslim Edildi")
                    {
                        aracRepo.AracDurumGuncelle(kiralama.AracID, "Boş");
                    }
                    else if (kiralama.TeslimDurumu == "Kiralık")
                    {
                        aracRepo.AracDurumGuncelle(kiralama.AracID, "Kiralandı");
                    }

                    // Kiralama güncelle
                    repo.Update(kiralama);

                    // Müşteri durumunu güncelle (mevcut teslim durumunu da gönder)
                    musteriRepo.MusteriDurumGuncelle(kiralama.MusteriID, kiralama.TeslimDurumu);

                    transaction.Complete();
                }

                XtraMessageBox.Show("Kiralama bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Arackiralama();
                AraclariGetir();
                MusterileriGetir();
                temizle();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Güncelleme sırasında hata oluştu: " + ex.Message,
                                  "Hata",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
            }
        }
        private void btn_temizle_Click(object sender, EventArgs e)
        {
            temizle();
            
        }


        private void HesaplaToplamUcret()
        {
            // 1. Tarih kontrolleri
            if (date_alisTarihi.EditValue == null || date_teslimTarihi.EditValue == null)
            {
                txt_toplamUcret.Text = "Tarih seçiniz";
                return;
            }

            // 2. Gün sayısı hesapla
            DateTime alisTarihi = date_alisTarihi.DateTime.Date;
            DateTime teslimTarihi = date_teslimTarihi.DateTime.Date;
            int gunSayisi = (teslimTarihi - alisTarihi).Days;

            if (gunSayisi <= 0)
            {
                txt_toplamUcret.Text = "Teslim tarihi alıştan sonra olmalı";
                return;
            }

            // 3. Günlük ücreti oku (tüm formatlara uyumlu)
            string ucretText = txt_gunlukUcret.Text
                .Replace(",", ".")
                .Replace(" TL", "")
                .Replace("$", "")
                .Replace("€", "")
                .Trim();

            if (decimal.TryParse(ucretText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal gunlukUcret))
            {
                // 4. Hesapla ve formatla
                decimal toplam = gunSayisi * gunlukUcret;
                txt_toplamUcret.Text = toplam.ToString("C2", new CultureInfo("tr-TR"));
            }
            else
            {
                txt_toplamUcret.Text = "Geçerli ücret girin (Örn: 250.50)";
            }
        }
        private void date_alisTarihi_EditValueChanged(object sender, EventArgs e)
        {
            HesaplaToplamUcret();
        }
        private void txt_toplamUcret_EditValueChanged(object sender, EventArgs e)
        {
           // HesaplaToplamUcret();
        }
        private void date_teslimTarihi_EditValueChanged(object sender, EventArgs e)
        {
            HesaplaToplamUcret();
        }

       

        private void gridLookUpEdit1View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;

            try
            {
                // 1. Günlük ücreti güvenli şekilde al
                object ucretDegeri = gridView1.GetFocusedRowCellValue("GunlukUcret");

                // 2. Decimal'e çevirme (tüm formatları destekleyecek şekilde)
                string ucretString = ucretDegeri?.ToString()
                    .Replace(",", ".")  // Virgülleri noktaya çevir
                    .Replace(" ", "");  // Boşlukları temizle

                if (decimal.TryParse(ucretString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal gunlukUcret))
                {
                    // 3. TextBox'a kültürden bağımsız formatla yaz
                    txt_gunlukUcret.Text = gunlukUcret.ToString("0.00", CultureInfo.InvariantCulture);

                    // 4. Diğer alanları doldur
                    date_alisTarihi.DateTime = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("AlisTarihi"));
                    date_teslimTarihi.DateTime = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("TeslimTarihi"));

                    // 5. Hesaplamayı tetikle
                    HesaplaToplamUcret();
                }
                else
                {
                    throw new Exception($"Geçersiz ücret formatı: {ucretDegeri}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
                txt_gunlukUcret.Text = "0.00";
            }
        }

        private void grd_AracSec_EditValueChanged(object sender, EventArgs e)
        {
            if (grd_AracSec.EditValue == null)
                return;

            try
            {
                int aracId = Convert.ToInt32(grd_AracSec.EditValue);
                var arac = aracRepo.GetById(aracId);

                if (arac == null)
                {
                    XtraMessageBox.Show("Araç bilgisi alınamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (arac.Durum != "Boş")
                {
                    XtraMessageBox.Show("Bu araç kiralanamaz! Durum: " + arac.Durum,
                                      "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    /*
                    grd_AracSec.EditValue = null;
                    pictureBox1.Image = null;
                    */
                    return;
                }

                // Görseli yükle
                string gorselYolu = arac.ArabaGorsel;
                txt_gunlukUcret.Text = Convert.ToString(arac.GunlukUcret);
                if (!string.IsNullOrEmpty(gorselYolu))
                {
                    if (File.Exists(gorselYolu))
                    {
                        pictureBox1.Image = Image.FromFile(gorselYolu);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Tag = gorselYolu; // Gerekirse kullanmak için
                    }
                    else
                    {
                        pictureBox1.Image = null;
                        XtraMessageBox.Show("Görsel bulunamadı: " + gorselYolu, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Araç seçimi sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void grd_MusteriSec_EditValueChanged(object sender, EventArgs e)
        {
            // gridTiklamaModu ||
            if ( formYukleniyor || grd_MusteriSec.EditValue == null)
                return;

            try
            {
                int musteriId = Convert.ToInt32(grd_MusteriSec.EditValue);

                // 2. Sadece yeni kiralama yaparken kontrol et (güncelleme yaparken değil)
                if (string.IsNullOrEmpty(txt_kiralamaID.Text))
                {
                    bool aktifKiralamaVar = repo.MusterininAktifKiralamasiVar(musteriId);

                    if (aktifKiralamaVar)
                    {
                        XtraMessageBox.Show("Bu müşterinin zaten aktif bir kiralaması var!",
                                          "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        grd_MusteriSec.EditValue = null;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Müşteri kontrolü sırasında hata: {ex.Message}",
                                  "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
      

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                var selectedRow = gridView1.GetFocusedRow() as Kiralama;
                if (selectedRow != null)
                {
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Satır seçilirken hata oluştu: " + ex.Message);
            }
        }

      
    }
}
