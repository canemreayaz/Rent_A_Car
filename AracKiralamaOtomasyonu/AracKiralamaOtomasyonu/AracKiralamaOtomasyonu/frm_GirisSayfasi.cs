using AracKiralamaOtomasyonu.FormSayfalari;
using AracKiralamaOtomasyonu.Models;
using AracKiralamaOtomasyonu.Repository;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaOtomasyonu
{
    public partial class frm_GirisSayfasi : Form
    {
        public frm_GirisSayfasi()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form yüklenirken hata: " + ex.Message);
                Application.Exit(); // Hata durumunda uygulamayı kapat
            }
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit1_Click(object sender, EventArgs e)
        {
            panel2.BackColor = Color.White;
            panel3.BackColor = SystemColors.Control;
            
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {
            panel2.BackColor = SystemColors.Control;
            panel3.BackColor = Color.White;
        }

        private void hyperlinkLabelControl3_Click(object sender, EventArgs e)
        {
           frm_GirisSayfasi frm_GirisSayfasi = new frm_GirisSayfasi();
            this.Close();
        }

        private void link_sifremiUnuttum_Click(object sender, EventArgs e)
        {
            frm_SifreDegistirme frm_SifreSifirlama = new frm_SifreDegistirme();
            frm_SifreSifirlama.Show();
            this.Hide();
        }

        private void btn_GirisYap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_kullaniciAdi.Text) || string.IsNullOrWhiteSpace(txt_sifre.Text))
            {
                XtraMessageBox.Show("Kullanıcı Adı veya Şifre Boş Bırakılamaz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                KullaniciRepository kullaniciKontrol = new KullaniciRepository();
                Kullanici kullanici = kullaniciKontrol.GirisYapanKullanici(txt_kullaniciAdi.Text, txt_sifre.Text);

                if (kullanici != null)
                {
                    //XtraMessageBox.Show("Giriş Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmAnaEkran frm_AnaSayfa = new frmAnaEkran(kullanici); // kullanıcıyı gönder
                    frm_AnaSayfa.Show();
                    this.Hide();
                }
                else
                {
                    XtraMessageBox.Show("Kullanıcı Adı veya Şifre Hatalı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_GirisYap_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txt_kullaniciAdi_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void frm_GirisSayfasi_Shown(object sender, EventArgs e)
        {
            txt_kullaniciAdi.Focus();
            txt_kullaniciAdi.SelectAll();
        }

        private void txt_sifre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_GirisYap.PerformClick();
            }
        }
    }
}

