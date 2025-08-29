using AracKiralamaOtomasyonu.Repository;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaOtomasyonu.FormSayfalari
{
    public partial class frm_SifreDegistirme : Form
    {
        public frm_SifreDegistirme()
        {
            InitializeComponent();
        }

        public string girisYapanKullaniciAdi; // Giriş ekranından aktarılmalı

        private void frm_SifreDegistirme_Load(object sender, EventArgs e)
        {
            groupControl2_telefonAlani.Enabled = false;
            groupControl3_sifreDegistirme.Enabled = false;

            var repo = new KullaniciRepository();
            var liste = repo.getGirisTablosu();

            comboBoxEdit_guvenlikSorusu.Properties.Items.Clear();

            if (liste != null && liste.Count > 0)
            {
                // Tekrar eden soruları önlemek için Distinct kullanıyoruz
                var sorular = liste.Select(k => k.GuvenlikSorusu).Distinct();

                foreach (var soru in sorular)
                {
                    comboBoxEdit_guvenlikSorusu.Properties.Items.Add(soru);
                }

                if (comboBoxEdit_guvenlikSorusu.Properties.Items.Count > 0)
                    comboBoxEdit_guvenlikSorusu.SelectedIndex = 0;
            }
        }

        private void btn_sorgula_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txt_kullaniciAdi.Text.Trim();
            string guvenlikSorusu = comboBoxEdit_guvenlikSorusu.Text.Trim();
            string guvenlikCevabi = txt_guvenlikCevabi.Text.Trim();

            // Boş alan kontrolü
            if (string.IsNullOrEmpty(kullaniciAdi) ||
                string.IsNullOrEmpty(guvenlikSorusu) ||
                string.IsNullOrEmpty(guvenlikCevabi))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Repository çağrısı
            var repo = new KullaniciRepository();
            var sonuc = repo.KullaniciDogrula(kullaniciAdi, guvenlikSorusu, guvenlikCevabi);

            // Sonuca göre işlem
            if (sonuc == GirisDurumlari.basarili)
            {
                MessageBox.Show("Bilgiler doğrulandı. Telefon Doğrulama alanı açılıyor.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                groupControl2_telefonAlani.Enabled = true;
            }
            else
            {
                MessageBox.Show("Kullanıcı bilgileri hatalı. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private int dogrulamaKodu;
        private void btn_dogrulamaKodu_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txt_kullaniciAdi.Text.Trim();
            string girilenEmail = txt_emailAlani.Text.Trim();

            if (string.IsNullOrWhiteSpace(kullaniciAdi) || string.IsNullOrWhiteSpace(girilenEmail))
            {
                MessageBox.Show("Kullanıcı adı ve mail alanı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Repositori üzerinden e-posta kontrolü
            KullaniciRepository repo = new KullaniciRepository();
            string emailFromDb = repo.EmailAdresKontrolEt(kullaniciAdi);

            if (emailFromDb == null)
            {
                MessageBox.Show("Kullanıcı bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (emailFromDb != girilenEmail)
            {
                MessageBox.Show("Girilen e-posta adresi, sistemdeki ile eşleşmiyor!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Doğrulama kodu oluştur
            Random random = new Random();
            dogrulamaKodu = random.Next(111111, 999999);

            // Mail gönder
            try
            {
                MailAddress mailAlici = new MailAddress(girilenEmail, kullaniciAdi);
                MailAddress mailGonderen = new MailAddress("admin mail", "admin ad soyad");

                MailMessage mesaj = new MailMessage();
                mesaj.To.Add(mailAlici);
                mesaj.From = mailGonderen;
                mesaj.Subject = "Şifre Değiştirme Talebi";
                mesaj.Body = $"Merhaba {kullaniciAdi},\n\nŞifre değiştirme işleminiz için doğrulama kodunuz:\n\n➡️ {dogrulamaKodu}\n\nBu kodu kimseyle paylaşmayın.\n\nİyi günler dileriz.\n\n📨 Destek";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("admin mail", "admin mail uygulama şifresi");
                smtp.EnableSsl = true;
                smtp.Send(mesaj);

                MessageBox.Show("Doğrulama kodu e-posta adresinize gönderildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("E-posta gönderilirken hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_onayla_Click(object sender, EventArgs e)
        {
            if (txt_dogrulamaKodu.Text == dogrulamaKodu.ToString())
            {
                XtraMessageBox.Show("Doğrulama kodu doğru. Şifre değiştirme alanı açılıyor.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                groupControl3_sifreDegistirme.Enabled = true;
            }
            else
            {
               XtraMessageBox.Show("Doğrulama kodu hatalı. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                groupControl3_sifreDegistirme.Enabled = false; // Hatalıysa şifre değiştirme alanını kapat
            }
        }

        private void btn_sifreDegistir_Click(object sender, EventArgs e)
        {
            if(txt_yeniSifre.Text == txt_yeniSifreTekrar.Text)
            {
                string kullaniciAdi = txt_kullaniciAdi.Text.Trim();
                string yeniSifre = txt_yeniSifre.Text.Trim();
                // Repository üzerinden şifre değiştirme işlemi
                KullaniciRepository repo = new KullaniciRepository();
                var sonuc = repo.SifreDegistir(kullaniciAdi, yeniSifre);
                if (sonuc == GirisDurumlari.basarili)
                {
                    MessageBox.Show("Şifreniz başarıyla değiştirildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // this.Close(); // Formu kapat
                    this.Close(); // Şifre değiştirme formunu kapat
                    frm_GirisSayfasi girisSayfasi = new frm_GirisSayfasi();
                    girisSayfasi.Show();
                }
                else
                {
                    MessageBox.Show("Şifre değiştirme işlemi başarısız. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Yeni şifreler eşleşmiyor. Lütfen kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

           


        }

       
    }
}
