namespace AracKiralamaOtomasyonu.FormSayfalari
{
    partial class frm_MusteriIstatistik
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
            this.glue_MusteriSecim = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btn_IstatistikGetir = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl_Kiralama = new DevExpress.XtraGrid.GridControl();
            this.gridView_Kiralama = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chartControl_MarkaAnaliz = new DevExpress.XtraCharts.ChartControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_Durum = new DevExpress.XtraEditors.LabelControl();
            this.btn_TumunuGoster = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.glue_MusteriSecim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_Kiralama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Kiralama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_MarkaAnaliz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // glue_MusteriSecim
            // 
            this.glue_MusteriSecim.Location = new System.Drawing.Point(12, 12);
            this.glue_MusteriSecim.Name = "glue_MusteriSecim";
            this.glue_MusteriSecim.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glue_MusteriSecim.Properties.NullText = "";
            this.glue_MusteriSecim.Properties.PopupFormSize = new System.Drawing.Size(300, 400);
            this.glue_MusteriSecim.Properties.PopupView = this.gridLookUpEdit1View;
            this.glue_MusteriSecim.Size = new System.Drawing.Size(367, 22);
            this.glue_MusteriSecim.TabIndex = 0;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // btn_IstatistikGetir
            // 
            this.btn_IstatistikGetir.Location = new System.Drawing.Point(407, 12);
            this.btn_IstatistikGetir.Name = "btn_IstatistikGetir";
            this.btn_IstatistikGetir.Size = new System.Drawing.Size(176, 38);
            this.btn_IstatistikGetir.TabIndex = 1;
            this.btn_IstatistikGetir.Text = "İstatistikleri Göster";
            this.btn_IstatistikGetir.Click += new System.EventHandler(this.btn_IstatistikGetir_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 2);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControl_Kiralama);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.chartControl_MarkaAnaliz);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1290, 661);
            this.splitContainerControl1.SplitterPosition = 681;
            this.splitContainerControl1.TabIndex = 2;
            // 
            // gridControl_Kiralama
            // 
            this.gridControl_Kiralama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_Kiralama.Location = new System.Drawing.Point(0, 0);
            this.gridControl_Kiralama.MainView = this.gridView_Kiralama;
            this.gridControl_Kiralama.Name = "gridControl_Kiralama";
            this.gridControl_Kiralama.Size = new System.Drawing.Size(681, 661);
            this.gridControl_Kiralama.TabIndex = 0;
            this.gridControl_Kiralama.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_Kiralama});
            // 
            // gridView_Kiralama
            // 
            this.gridView_Kiralama.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.gridView_Kiralama.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView_Kiralama.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView_Kiralama.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView_Kiralama.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gridView_Kiralama.GridControl = this.gridControl_Kiralama;
            this.gridView_Kiralama.Name = "gridView_Kiralama";
            this.gridView_Kiralama.OptionsBehavior.Editable = false;
            this.gridView_Kiralama.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Müşteri ID";
            this.gridColumn1.FieldName = "MusteriID";
            this.gridColumn1.MinWidth = 25;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 100;
            // 
            // chartControl_MarkaAnaliz
            // 
            this.chartControl_MarkaAnaliz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl_MarkaAnaliz.Location = new System.Drawing.Point(0, 0);
            this.chartControl_MarkaAnaliz.Name = "chartControl_MarkaAnaliz";
            this.chartControl_MarkaAnaliz.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl_MarkaAnaliz.Size = new System.Drawing.Size(597, 661);
            this.chartControl_MarkaAnaliz.TabIndex = 2;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lbl_Durum);
            this.panelControl1.Controls.Add(this.btn_TumunuGoster);
            this.panelControl1.Controls.Add(this.btn_IstatistikGetir);
            this.panelControl1.Controls.Add(this.glue_MusteriSecim);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1294, 100);
            this.panelControl1.TabIndex = 3;
            // 
            // lbl_Durum
            // 
            this.lbl_Durum.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lbl_Durum.Appearance.Options.UseFont = true;
            this.lbl_Durum.Location = new System.Drawing.Point(928, 61);
            this.lbl_Durum.Name = "lbl_Durum";
            this.lbl_Durum.Size = new System.Drawing.Size(248, 24);
            this.lbl_Durum.TabIndex = 3;
            this.lbl_Durum.Text = "Tüm Müşteriler Listeleniyor.";
            // 
            // btn_TumunuGoster
            // 
            this.btn_TumunuGoster.Location = new System.Drawing.Point(600, 12);
            this.btn_TumunuGoster.Name = "btn_TumunuGoster";
            this.btn_TumunuGoster.Size = new System.Drawing.Size(176, 38);
            this.btn_TumunuGoster.TabIndex = 2;
            this.btn_TumunuGoster.Text = "Tümünü Göster";
            this.btn_TumunuGoster.Click += new System.EventHandler(this.btn_TumunuGoster_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.splitContainerControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 100);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1294, 665);
            this.panelControl2.TabIndex = 4;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "İsim";
            this.gridColumn2.FieldName = "Ad";
            this.gridColumn2.MinWidth = 25;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 84;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Soyisim";
            this.gridColumn3.FieldName = "Soyad";
            this.gridColumn3.MinWidth = 25;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 84;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Araç Marka";
            this.gridColumn4.FieldName = "Marka";
            this.gridColumn4.MinWidth = 25;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 91;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Araç Modeli";
            this.gridColumn5.FieldName = "Model";
            this.gridColumn5.MinWidth = 25;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 81;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Alış Tarihi";
            this.gridColumn6.FieldName = "AlisTarihi";
            this.gridColumn6.MinWidth = 25;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 85;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Teslim Tarihi";
            this.gridColumn7.FieldName = "TeslimTarihi";
            this.gridColumn7.MinWidth = 25;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 100;
            // 
            // frm_MusteriIstatistik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1294, 765);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frm_MusteriIstatistik";
            this.Text = "Müşteri İstatistik";
            this.Load += new System.EventHandler(this.frm_MusteriIstatistik_Load);
            ((System.ComponentModel.ISupportInitialize)(this.glue_MusteriSecim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_Kiralama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_Kiralama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_MarkaAnaliz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GridLookUpEdit glue_MusteriSecim;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.SimpleButton btn_IstatistikGetir;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl_Kiralama;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_Kiralama;
        private DevExpress.XtraCharts.ChartControl chartControl_MarkaAnaliz;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_TumunuGoster;
        private DevExpress.XtraEditors.LabelControl lbl_Durum;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
    }
}