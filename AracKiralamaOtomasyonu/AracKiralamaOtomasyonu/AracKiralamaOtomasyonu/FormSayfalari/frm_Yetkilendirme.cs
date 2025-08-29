using AracKiralamaOtomasyonu.Models;
using AracKiralamaOtomasyonu.Repository;
using DevExpress.XtraCharts.Design;
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
    public partial class frm_Yetkilendirme : Form
    {
        KullaniciRepository kullaniciRepository = new KullaniciRepository();
        List<Kullanici> kullanici = new List<Kullanici>();
        public frm_Yetkilendirme()
        {
            InitializeComponent();
        }
        void listele()
        {
            kullanici = kullaniciRepository.GetAll(); // Dapper ile veritabanından tüm müşterileri alıyoruz
            grd_yetkilendirme.DataSource = null;  // Dapper verisini GridControl'e bağladık
            grd_yetkilendirme.DataSource = kullanici;  // Dapper verisini GridControl'e bağladık
        }
        private void frm_Yetkilendirme_Load(object sender, EventArgs e)
        {
           listele(); // Form yüklendiğinde listele metodunu çağırıyoruz
        }

        void temizle()
        {
            txt_kullaniciID.Text = "";
            txt_kullaniciAdi.Text = "";
            txt_sifre.Text = "";
            txt_yetki.Text = "";
            txt_emailAlani.Text = "";
            comboBoxEdit1.Text = "";
            txt_guvenlikCevabi.Text = "";
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                temizle();
                return;
            }

            Kullanici seciliSatir = gridView1.GetFocusedRow() as Kullanici;
            if (seciliSatir != null)
            {
                txt_kullaniciID.Text = seciliSatir.KullaniciID.ToString();
                txt_kullaniciAdi.Text = seciliSatir.KullaniciAdi;
                txt_sifre.Text = seciliSatir.Sifre;
                txt_yetki.Text = seciliSatir.Yetki;
                txt_emailAlani.Text = seciliSatir.EmailAlani;
                comboBoxEdit1.Text = seciliSatir.GuvenlikSorusu;
                txt_guvenlikCevabi.Text = seciliSatir.GuvenlikCevabi;
                
            }
        }
        void ekle()
        {
            Kullanici kullanici = new Kullanici();
            kullanici.KullaniciAdi = txt_kullaniciAdi.Text;
            kullanici.Sifre = txt_sifre.Text;
            kullanici.Yetki = txt_yetki.Text;
            kullanici.EmailAlani = txt_emailAlani.Text;
            kullanici.GuvenlikSorusu = comboBoxEdit1.Text;
            kullanici.GuvenlikCevabi = txt_guvenlikCevabi.Text;
          

            KullaniciRepository repo = new KullaniciRepository();
            repo.Add(kullanici); // Veritabanına ekleniyor

            XtraMessageBox.Show("Müşteri başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

        }
        private void btn_ekle_Click(object sender, EventArgs e)
        {
            ekle();
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        void sil()
        {
            if (gridView1.GetFocusedRowCellValue("KullaniciID") == null)
            {
                XtraMessageBox.Show("Silmek için bir kayıt seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("KullaniciID"));

            // Emin misin sorusu eklemek istersen:
            DialogResult result = XtraMessageBox.Show("Bu müşteriyi silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                KullaniciRepository kullaniciRepository = new KullaniciRepository();
                kullaniciRepository.Delete(id);


                XtraMessageBox.Show("Müşteri başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            listele();
            temizle();
        }
        private void btn_sil_Click(object sender, EventArgs e)
        {
            sil();
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_kullaniciID.Text))
            {
                XtraMessageBox.Show("Lütfen güncellenecek bir kayıt seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Kullanici kullanici = new Kullanici();
            kullanici.KullaniciID = Convert.ToInt32(txt_kullaniciID.Text);
            kullanici.KullaniciAdi = txt_kullaniciAdi.Text;
            kullanici.Sifre = txt_sifre.Text;
            kullanici.Yetki = txt_yetki.Text;
            kullanici.EmailAlani = txt_emailAlani.Text;
            kullanici.GuvenlikSorusu = comboBoxEdit1.Text;
            kullanici.GuvenlikCevabi = txt_guvenlikCevabi.Text;
           

            KullaniciRepository repo = new KullaniciRepository();
            repo.Update(kullanici);

            XtraMessageBox.Show("Müşteri bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            listele();
            temizle();
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            listele();
        }
    }
}
