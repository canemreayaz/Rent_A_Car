using AracKiralamaOtomasyonu.Models;
using AracKiralamaOtomasyonu.Repository;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frm_Musteriler : Form
    {


        List<Musteri> musteriler = new List<Musteri>();
        public frm_Musteriler()
        {
            InitializeComponent();
        }
        MusteriRepository repo = new MusteriRepository();
        void listele()
        {
            musteriler = repo.GetAll(); // Dapper ile veritabanından tüm müşterileri alıyoruz
            gridControl1.DataSource = null;  // Dapper verisini GridControl'e bağladık
            gridControl1.DataSource = musteriler;  // Dapper verisini GridControl'e bağladık
        }
      
        void temizle()
        {
            txt_musteriID.Text = "";
            txt_tc.Text = "";
            txt_isim.Text = "";
            txt_soyisim.Text = "";
            txt_telefonNumarasi.Text = "";
            txt_emailAdres.Text = "";
            txt_ehliyetNo.Text = "";
            dateEdit1.EditValue = null;  // Tarih alanını temizler
        }

        void ekle()
        {
            Musteri musteri = new Musteri();
            musteri.TC = txt_tc.Text;
            musteri.Ad = txt_isim.Text;
            musteri.Soyad = txt_soyisim.Text;
            musteri.Telefon = txt_telefonNumarasi.Text;
            musteri.Email = txt_emailAdres.Text;
            musteri.EhliyetNo = txt_ehliyetNo.Text;
            musteri.EhliyetTarihi = dateEdit1.DateTime;

            // TC kontrolü: 11 haneli ve sadece rakam olmalı
            if (musteri.TC.Length != 11 || !musteri.TC.All(char.IsDigit))
            {
                XtraMessageBox.Show("TC Kimlik Numarası 11 haneli ve sadece rakamlardan oluşmalıdır.",
                                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ehliyet No kontrolü: 6 haneli ve sadece rakam olmalı
            if (musteri.EhliyetNo.Length != 6 || !musteri.EhliyetNo.All(char.IsDigit))
            {
                XtraMessageBox.Show("Ehliyet Numarası 6 haneli ve sadece rakamlardan oluşmalıdır.",
                                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MusteriRepository repo = new MusteriRepository();
            repo.Add(musteri); // Veritabanına ekleniyor

            XtraMessageBox.Show("Müşteri başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            ekle();
        }

        void sil()
        {
            if (gridView1.GetFocusedRowCellValue("MusteriID") == null)
            {
                XtraMessageBox.Show("Silmek için bir kayıt seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("MusteriID"));

            // Emin misin sorusu eklemek istersen:
            DialogResult result = XtraMessageBox.Show("Bu müşteriyi silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MusteriRepository repo = new MusteriRepository();
                repo.Delete(id);


                XtraMessageBox.Show("Müşteri başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            listele();
            temizle();
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            sil();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                temizle();
                return;
            }

            Musteri seciliSatir = gridView1.GetFocusedRow() as Musteri;
            if (seciliSatir != null)
            {
                txt_musteriID.Text = seciliSatir.MusteriID.ToString();
                txt_tc.Text = seciliSatir.TC;
                txt_isim.Text = seciliSatir.Ad;
                txt_soyisim.Text = seciliSatir.Soyad;
                txt_telefonNumarasi.Text = seciliSatir.Telefon;
                txt_emailAdres.Text = seciliSatir.Email;
                txt_ehliyetNo.Text = seciliSatir.EhliyetNo;
                dateEdit1.DateTime = seciliSatir.EhliyetTarihi;
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_musteriID.Text))
            {
                XtraMessageBox.Show("Lütfen güncellenecek bir kayıt seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Musteri musteri = new Musteri();
            musteri.MusteriID = Convert.ToInt32(txt_musteriID.Text);
            musteri.TC = txt_tc.Text;
            musteri.Ad = txt_isim.Text;
            musteri.Soyad = txt_soyisim.Text;
            musteri.Telefon = txt_telefonNumarasi.Text;
            musteri.Email = txt_emailAdres.Text;
            musteri.EhliyetNo = txt_ehliyetNo.Text;
            musteri.EhliyetTarihi = dateEdit1.DateTime;

            MusteriRepository repo = new MusteriRepository();
            repo.Update(musteri);

            XtraMessageBox.Show("Müşteri bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            listele();
            temizle();
            
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
       
       
        private void frm_Musteriler_Load(object sender, EventArgs e)
        {
            listele(); // Form yüklendiğinde listele metodunu çağırıyoruz
            temizle(); // Form yüklendiğinde alanları temizliyoruz
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            listele(); // Listeleme butonuna tıklandığında listele metodunu çağırıyoruz
        }
    
    
    
    }
}
