using AracKiralamaOtomasyonu.Models;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaOtomasyonu.FormSayfalari
{
    public partial class frmAnaEkran : Form
    {
        private Kullanici aktifKullanici;
        public frmAnaEkran(Kullanici kullanici)
        {
            InitializeComponent();
            aktifKullanici = kullanici;
        }


        FormSayfalari.frm_Musteriler frm_Musteriler;
        private void btn_MusteriListesi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frm_Musteriler == null || frm_Musteriler.IsDisposed)
            {
                frm_Musteriler = new FormSayfalari.frm_Musteriler();
                frm_Musteriler.MdiParent = this; // mdi üzerinde açılacak
                frm_Musteriler.FormClosed += (s, args) => frm_Musteriler = null;
                frm_Musteriler.Show();
            }
            else
            {
                frm_Musteriler.BringToFront(); // Zaten açıksa ön plana getir
            }

        }


        frm_ArabaKiralama arabaKiralama;
        private void btn_KiralananArabaListesi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (arabaKiralama == null || arabaKiralama.IsDisposed)
            {
                arabaKiralama = new frm_ArabaKiralama();
                arabaKiralama.MdiParent = this; // mdi üzerinde açılacak
                arabaKiralama.Show();
            }
            else
            {
                arabaKiralama.BringToFront(); // Zaten açıksa ön plana getir
            }
        }

        frm_KiralananArabaIstatistikleri kiralananArabaIstatistikleri;
        private void btn_kiralananArabaIstatiskleri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (kiralananArabaIstatistikleri == null || kiralananArabaIstatistikleri.IsDisposed)
            {
                kiralananArabaIstatistikleri = new frm_KiralananArabaIstatistikleri();
                kiralananArabaIstatistikleri.MdiParent = this; // mdi üzerinde açılacak
                kiralananArabaIstatistikleri.Show();
            }
            else
            {
                kiralananArabaIstatistikleri.BringToFront(); // Zaten açıksa ön plana getir
            }

        }


        frm_ArabaListesi arabaListesi;
        private void btn_ArabaListesi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (arabaListesi == null || arabaListesi.IsDisposed)
            {
                arabaListesi = new frm_ArabaListesi();
                arabaListesi.MdiParent = this; // mdi üzerinde açılacak
                arabaListesi.Show();
            }
            else
            {
                arabaListesi.BringToFront(); // Zaten açıksa ön plana getir
            }

        }


        frm_ToplamArabaIstatiskleri toplamArabaIstatiskleri;
        private void btn_ToplamArabaIstatistik_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (toplamArabaIstatiskleri == null || toplamArabaIstatiskleri.IsDisposed)
            {
                toplamArabaIstatiskleri = new frm_ToplamArabaIstatiskleri();
                toplamArabaIstatiskleri.MdiParent = this; // mdi üzerinde açılacak
                toplamArabaIstatiskleri.Show();
            }
            else
            {
                toplamArabaIstatiskleri.BringToFront(); // Zaten açıksa ön plana getir
            }
        }

        frm_Yetkilendirme yetkilendirme;
        private void btn_yetkilendirme_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {// Kullanıcı yetkisi kontrolü yapıyoruz
            if (aktifKullanici != null && aktifKullanici.KullaniciAdi.ToLower() == "emre") // örnek: sadece 'emre' açabilir
            {
                if (yetkilendirme == null || yetkilendirme.IsDisposed)
                {
                    yetkilendirme = new frm_Yetkilendirme();
                    yetkilendirme.MdiParent = this; // mdi üzerinde açılacak
                    yetkilendirme.Show();
                }
                else
                {
                    yetkilendirme.BringToFront(); // Zaten açıksa ön plana getir
                }
            }
            else
            {
                XtraMessageBox.Show("Bu bölüme erişim yetkiniz yok.", "Yetkisiz Erişim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmAnaEkran_Load(object sender, EventArgs e)
        {
            // Örnek yetki kontrolü
            if (aktifKullanici.KullaniciAdi.ToLower() != "emre")
            {
                btn_yetkilendirme.Enabled = false; // Butonu pasif yap
            }

            // Veya:
            // if (aktifKullanici.Yetki != "Admin") btn_yetkilendirme.Visibility = BarItemVisibility.Never;
        }

       
        private async void btn_GitHub_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // İstediğin URL
                string url = "https://github.com";

                // Tarayıcıda aç
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // çok önemli, yoksa hata verir
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tarayıcıda açılırken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_haberler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // İstediğin URL
                string url = "https://www.haberler.com/";

                // Tarayıcıda aç
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // çok önemli, yoksa hata verir
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tarayıcıda açılırken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_kurlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // İstediğin URL
                string url = "https://www.tcmb.gov.tr/wps/wcm/connect/tr/tcmb+tr/main+page+site+area/bugun";

                // Tarayıcıda aç
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // çok önemli, yoksa hata verir
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tarayıcıda açılırken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_haritalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // İstediğin URL
                string url = "https://www.google.com/maps/@40.2047279,28.9645082,15z?entry=ttu&g_ep=EgoyMDI1MDcyMS4wIKXMDSoASAFQAw%3D%3D";

                // Tarayıcıda aç
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // çok önemli, yoksa hata verir
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tarayıcıda açılırken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_google_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // İstediğin URL
                string url = "https://www.google.com/webhp?hl=tr&sa=X&ved=0ahUKEwjxr4TajtWOAxUcSfEDHdtnIaoQPAgI";

                // Tarayıcıda aç
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // çok önemli, yoksa hata verir
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tarayıcıda açılırken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private frm_Gif gifForm;
        private void btn_AnaSayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gifForm == null || gifForm.IsDisposed)
            {
                gifForm = new frm_Gif();
                gifForm.MdiParent = this; // MDI ana forma bağlanır
                gifForm.WindowState = FormWindowState.Maximized;
                gifForm.Show();
            }
            else
            {
                gifForm.BringToFront();
            }
        }

        private void frmAnaEkran_Shown(object sender, EventArgs e)
        {
            if (gifForm == null || gifForm.IsDisposed)
            {
                gifForm = new frm_Gif();
                gifForm.MdiParent = this; // MDI ana forma bağlanır
                gifForm.WindowState = FormWindowState.Maximized;
                gifForm.Show();
            }
            else
            {
                gifForm.BringToFront();
            }
        }

        private void btn_musteriIstatistik_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Aynı form zaten açıksa tekrar açma
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is frm_MusteriIstatistik)
                {
                    frm.Activate();
                    return;
                }
            }

            frm_MusteriIstatistik musteriIstatistik = new frm_MusteriIstatistik();
            musteriIstatistik.MdiParent = this; // MDI olarak ana forma bağla
            musteriIstatistik.WindowState = FormWindowState.Maximized; // Açıldığında tam ekran yap
            musteriIstatistik.Show();
        }

        private void btn_MusteriRapor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm_MusteriFaturaRapor frm = Application.OpenForms["frm_MusteriFaturaRapor"] as frm_MusteriFaturaRapor;

            if (frm == null)
            {
                frm = new frm_MusteriFaturaRapor();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                frm.Activate();
            }
        }



        private void frmAnaEkran_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // Uygulama kapatıldığında tüm formları kapat   
        }

        private void btn_Rapor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Aynı form zaten açıksa tekrar açma
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is frm_FaturaSihirbazi)
                {
                    frm.Activate();
                    return;
                }
            }
            frm_FaturaSihirbazi rapor = new frm_FaturaSihirbazi();
            rapor.MdiParent = this; // MDI olarak ana forma bağla
            rapor.WindowState = FormWindowState.Maximized; // Açıldığında tam ekran yap
            rapor.Show();

        }
    }

}
    

