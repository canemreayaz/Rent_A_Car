using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AracKiralamaOtomasyonu.FormSayfalari
{
    public partial class frm_Gif : Form
    {
        

        public frm_Gif()
        {
            InitializeComponent();

           
        }

        private void frm_Gif_Load(object sender, EventArgs e)
        {
            


        }

        private void frm_Gif_Shown(object sender, EventArgs e)
        {

            string gifYolu = Path.Combine(Application.StartupPath, "Resources", "yazamerhaba.gif");
            if (File.Exists(gifYolu))
            {
                pictureBox1.Image = Image.FromFile(gifYolu);
            }
            else
            {
                MessageBox.Show("GIF dosyası bulunamadı!");
            }

        }
    
    
    
    }
}
