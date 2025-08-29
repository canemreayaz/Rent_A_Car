using AracKiralamaOtomasyonu.Repository;
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
    public partial class frm_FaturaSihirbazi : Form
    {
        public frm_FaturaSihirbazi()
        {
            InitializeComponent();
        }

        private void frm_FaturaSihirbazi_Load(object sender, EventArgs e)
        {
            var repo = new FaturaRepository("Server=AYAZ;Database=AracKiralamaOtomasyonu;Trusted_Connection=True;");
            gridControl1.DataSource = repo.FaturalariGetir();
        }


    }
}
