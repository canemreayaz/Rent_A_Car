using AracKiralamaOtomasyonu.Models;
using AracKiralamaOtomasyonu.Repository;
using DevExpress.XtraGrid.Views.Grid;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;




namespace AracKiralamaOtomasyonu.FormSayfalari
{
    public partial class frm_MusteriFaturaRapor : Form
    {
        public frm_MusteriFaturaRapor()
        {
            InitializeComponent();
        }

        private void MusteriListesiYukle()
        {
            var repo = new MusteriRepository();
            var liste = repo.GetLookUpMusteriler();

            lookUp_MusteriSecim.Properties.DataSource = liste;
            lookUp_MusteriSecim.Properties.DisplayMember = "AdSoyad";
            lookUp_MusteriSecim.Properties.ValueMember = "MusteriID";
            lookUp_MusteriSecim.Properties.NullText = "Müşteri Seçiniz...";
        }

        private void frm_MusteriFaturaRapor_Load(object sender, EventArgs e)
        {
            MusteriListesiYukle();
        }

        private void lookUp_MusteriSecim_EditValueChanged_1(object sender, EventArgs e)
        {
            if (lookUp_MusteriSecim.EditValue == null) return;

            int musteriID = Convert.ToInt32(lookUp_MusteriSecim.EditValue);
            var repo = new MusteriRepository();
            var gecmis = repo.GetMusteriAracKiralamaListesiByID(musteriID);

            grid_KiralamaGecmisi.DataSource = gecmis;

            var view = grid_KiralamaGecmisi.MainView as GridView;
            if (view != null)
            {
                view.Columns["Ad"].Caption = "Ad";
                view.Columns["Soyad"].Caption = "Soyad";
                view.Columns["Telefon"].Caption = "Telefon";
                view.Columns["EMail"].Caption = "E-Mail";
                view.Columns["Marka"].Caption = "Marka";
                view.Columns["Model"].Caption = "Model";
                view.Columns["AlisTarihi"].Caption = "Alış Tarihi";
                view.Columns["TeslimTarihi"].Caption = "Teslim Tarihi";
                view.Columns["ToplamUcret"].Caption = "Toplam Ücret";

                view.Columns["AlisTarihi"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["AlisTarihi"].DisplayFormat.FormatString = "dd/MM/yyyy";
                view.Columns["TeslimTarihi"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["TeslimTarihi"].DisplayFormat.FormatString = "dd/MM/yyyy";

                view.Columns["ToplamUcret"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                view.Columns["ToplamUcret"].DisplayFormat.FormatString = "c";
            }

            decimal toplam = gecmis.Sum(x => x.ToplamUcret);
            lbl_FaturaBilgisi.Text = $"Toplam Tutar: {toplam:C}";
        }

        private void btn_FaturaOlustur_Click(object sender, EventArgs e)
        {
            if (lookUp_MusteriSecim.EditValue == null)
            {
                MessageBox.Show("Lütfen bir müşteri seçin.");
                return;
            }

            int musteriID = Convert.ToInt32(lookUp_MusteriSecim.EditValue);
            var repo = new MusteriRepository();
            var musteri = repo.GetMusteriByID(musteriID);
            var kiralamaListesi = repo.GetMusteriAracKiralamaListesiByID(musteriID);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Dosyası|*.pdf",
                Title = "Fatura PDF Kaydet",
                FileName = $"Fatura_{musteri.Ad}_{musteri.Soyad}.pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string regularFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                string boldFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arialbd.ttf");

                var regularFont = PdfFontFactory.CreateFont(regularFontPath, PdfEncodings.WINANSI);
                var boldFont = PdfFontFactory.CreateFont(boldFontPath, PdfEncodings.WINANSI);

                using (var writer = new PdfWriter(saveFileDialog.FileName))
                using (var pdf = new PdfDocument(writer))
                using (var doc = new Document(pdf))
                {
                    doc.Add(new Paragraph("Rent A Car  - Müsteri Fatura Raporu")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(20)
                        .SetFont(boldFont)
                        .SetMarginBottom(10));

                    doc.Add(new Paragraph($"Tarih: {DateTime.Now:dd.MM.yyyy}")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(regularFont)
                        .SetFontSize(10));

                    doc.Add(new Paragraph("Müsteri Bilgileri")
                        .SetFont(boldFont)
                        .SetFontSize(12)
                        .SetMarginTop(15)
                        .SetUnderline());

                    doc.Add(new Paragraph(
                        $"Ad: {musteri.Ad}\n" +
                        $"Soyad: {musteri.Soyad}\n" +
                        $"Telefon: {musteri.Telefon}\n" +
                        $"Email: {musteri.Email}")
                        .SetFont(regularFont)
                        .SetFontSize(11)
                        .SetMarginBottom(20));

                    var table = new Table(UnitValue.CreatePercentArray(new float[] { 2, 2, 2, 2, 2 })).UseAllAvailableWidth();

                    // Başlık hücreleri
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Marka").SetFont(boldFont)).SetTextAlignment(TextAlignment.CENTER));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Model").SetFont(boldFont)).SetTextAlignment(TextAlignment.CENTER));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Alis Tarihi").SetFont(boldFont)).SetTextAlignment(TextAlignment.CENTER));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Teslim Tarihi").SetFont(boldFont)).SetTextAlignment(TextAlignment.CENTER));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Tutar").SetFont(boldFont)).SetTextAlignment(TextAlignment.CENTER));

                    decimal toplamTutar = 0;
                    foreach (var item in kiralamaListesi)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(item.Marka).SetFont(regularFont)).SetTextAlignment(TextAlignment.CENTER));
                        table.AddCell(new Cell().Add(new Paragraph(item.Model).SetFont(regularFont)).SetTextAlignment(TextAlignment.CENTER));
                        table.AddCell(new Cell().Add(new Paragraph(item.AlisTarihi.ToShortDateString()).SetFont(regularFont))).SetTextAlignment(TextAlignment.CENTER);
                        table.AddCell(new Cell().Add(new Paragraph(item.TeslimTarihi.ToShortDateString()).SetFont(regularFont))).SetTextAlignment(TextAlignment.CENTER);
                        table.AddCell(new Cell().Add(new Paragraph($"{item.ToplamUcret.ToString("n2")} TL").SetFont(regularFont))).SetTextAlignment(TextAlignment.RIGHT);

                        toplamTutar += item.ToplamUcret;
                    }

                    doc.Add(table);
                    doc.Add(new Paragraph($"\nToplam Tutar: {toplamTutar:C} TL")
                        .SetFont(boldFont)
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetUnderline());
                }

                // --- BURASI EKLENDİ: Fatura tabloya kaydediliyor ---
                var faturaRepo = new FaturaRepository("Server=.;Database=AracKiralamaOtomasyonu;Trusted_Connection=True;");
                faturaRepo.FaturaEkle(
                    musteriID,
                    DateTime.Now,
                    DateTime.Now.Year,
                    saveFileDialog.FileName
                );

                MessageBox.Show("Fatura kaydedildi ve PDF başarıyla oluşturuldu.", "Fatura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mail gönder
                FaturayiMailleGonder(saveFileDialog.FileName, musteri);
            }
        }

        private void FaturayiMailleGonder(string pdfPath, Musteri musteri)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("emreayaz.410@gmail.com");
                mail.To.Add(musteri.Email);
                mail.Subject = "Araç Kiralama Faturanız";
                mail.Body = $"Sayın {musteri.Ad} {musteri.Soyad},\n\nAraç kiralama işleminize ait fatura ektedir.\n\nİyi günler dileriz.";

                // PDF ekle
                Attachment attachment = new Attachment(pdfPath);
                mail.Attachments.Add(attachment);

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("emreayaz.410@gmail.com", "xjhs elra bdjm aotv");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                MessageBox.Show("Fatura PDF oluşturuldu ve e-posta ile gönderildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mail gönderilirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        

    }
}
