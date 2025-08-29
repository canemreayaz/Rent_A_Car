namespace AracKiralamaOtomasyonu.FormSayfalari
{
    partial class frm_MusteriFaturaRapor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lookUp_MusteriSecim = new DevExpress.XtraEditors.LookUpEdit();
            this.grid_KiralamaGecmisi = new DevExpress.XtraGrid.GridControl();
            this.gridView_Kiralama = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lbl_FaturaBilgisi = new DevExpress.XtraEditors.LabelControl();
            this.btn_FaturaOlustur = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lookUp_MusteriSecim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_KiralamaGecmisi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Kiralama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lookUp_MusteriSecim
            // 
            this.lookUp_MusteriSecim.Location = new System.Drawing.Point(12, 12);
            this.lookUp_MusteriSecim.Name = "lookUp_MusteriSecim";
            this.lookUp_MusteriSecim.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUp_MusteriSecim.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MusteriID", "Müşteri ID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ad", "İsim"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Soyad", "Soyisim"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MusteriDurum", "Müşteri Durum")});
            this.lookUp_MusteriSecim.Properties.NullText = "";
            this.lookUp_MusteriSecim.Properties.PopupWidth = 800;
            this.lookUp_MusteriSecim.Size = new System.Drawing.Size(342, 22);
            this.lookUp_MusteriSecim.TabIndex = 0;
            this.lookUp_MusteriSecim.EditValueChanged += new System.EventHandler(this.lookUp_MusteriSecim_EditValueChanged_1);
            // 
            // grid_KiralamaGecmisi
            // 
            this.grid_KiralamaGecmisi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_KiralamaGecmisi.Location = new System.Drawing.Point(2, 2);
            this.grid_KiralamaGecmisi.MainView = this.gridView_Kiralama;
            this.grid_KiralamaGecmisi.Name = "grid_KiralamaGecmisi";
            this.grid_KiralamaGecmisi.Size = new System.Drawing.Size(796, 406);
            this.grid_KiralamaGecmisi.TabIndex = 1;
            this.grid_KiralamaGecmisi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_Kiralama});
            // 
            // gridView_Kiralama
            // 
            this.gridView_Kiralama.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.gridView_Kiralama.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_Kiralama.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_Kiralama.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_Kiralama.GridControl = this.grid_KiralamaGecmisi;
            this.gridView_Kiralama.Name = "gridView_Kiralama";
            this.gridView_Kiralama.OptionsBehavior.Editable = false;
            this.gridView_Kiralama.OptionsView.ShowAutoFilterRow = true;
            this.gridView_Kiralama.OptionsView.ShowGroupPanel = false;
            // 
            // lbl_FaturaBilgisi
            // 
            this.lbl_FaturaBilgisi.Location = new System.Drawing.Point(12, 14);
            this.lbl_FaturaBilgisi.Name = "lbl_FaturaBilgisi";
            this.lbl_FaturaBilgisi.Size = new System.Drawing.Size(7, 16);
            this.lbl_FaturaBilgisi.TabIndex = 2;
            this.lbl_FaturaBilgisi.Text = "0";
            // 
            // btn_FaturaOlustur
            // 
            this.btn_FaturaOlustur.Location = new System.Drawing.Point(387, 12);
            this.btn_FaturaOlustur.Name = "btn_FaturaOlustur";
            this.btn_FaturaOlustur.Size = new System.Drawing.Size(138, 22);
            this.btn_FaturaOlustur.TabIndex = 3;
            this.btn_FaturaOlustur.Text = "Fatura Oluştur";
            this.btn_FaturaOlustur.Click += new System.EventHandler(this.btn_FaturaOlustur_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lookUp_MusteriSecim);
            this.panelControl1.Controls.Add(this.btn_FaturaOlustur);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(800, 79);
            this.panelControl1.TabIndex = 6;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.lbl_FaturaBilgisi);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 489);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(800, 42);
            this.panelControl2.TabIndex = 7;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.grid_KiralamaGecmisi);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 79);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(800, 410);
            this.panelControl3.TabIndex = 8;
            // 
            // frm_MusteriFaturaRapor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 531);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frm_MusteriFaturaRapor";
            this.Text = "Müşteri Fatura Raporlama";
            this.Load += new System.EventHandler(this.frm_MusteriFaturaRapor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookUp_MusteriSecim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_KiralamaGecmisi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Kiralama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit lookUp_MusteriSecim;
        private DevExpress.XtraGrid.GridControl grid_KiralamaGecmisi;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_Kiralama;
        private DevExpress.XtraEditors.LabelControl lbl_FaturaBilgisi;
        private DevExpress.XtraEditors.SimpleButton btn_FaturaOlustur;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
    }
}