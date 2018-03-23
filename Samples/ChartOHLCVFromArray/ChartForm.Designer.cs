namespace ChartOHLCVFromArray
{
    partial class ChartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartForm));
            this.Chart = new AxChartSkil27.AxChart();
            this.LoadDataButton = new System.Windows.Forms.Button();
            this.ChartToolbar = new AxChartSkil27.AxChartToolbar();
            this.DataLoadProgressBar = new System.Windows.Forms.ProgressBar();
            this.DataLoadProgressLabel = new System.Windows.Forms.Label();
            this.ChartStylesCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ClearChartButton = new System.Windows.Forms.Button();
            this.UpdateBarsCheck = new System.Windows.Forms.CheckBox();
            this.ManageChartStylesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartToolbar)).BeginInit();
            this.SuspendLayout();
            // 
            // Chart
            // 
            this.Chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Chart.Enabled = true;
            this.Chart.Location = new System.Drawing.Point(0, 22);
            this.Chart.Name = "Chart";
            this.Chart.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Chart.OcxState")));
            this.Chart.Size = new System.Drawing.Size(743, 459);
            this.Chart.TabIndex = 0;
            // 
            // LoadDataButton
            // 
            this.LoadDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadDataButton.Location = new System.Drawing.Point(614, 525);
            this.LoadDataButton.Name = "LoadDataButton";
            this.LoadDataButton.Size = new System.Drawing.Size(106, 51);
            this.LoadDataButton.TabIndex = 1;
            this.LoadDataButton.Text = "Load data";
            this.LoadDataButton.UseVisualStyleBackColor = true;
            this.LoadDataButton.Click += new System.EventHandler(this.LoadDataButton_Click);
            // 
            // ChartToolbar
            // 
            this.ChartToolbar.Location = new System.Drawing.Point(0, 0);
            this.ChartToolbar.Name = "ChartToolbar";
            this.ChartToolbar.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ChartToolbar.OcxState")));
            this.ChartToolbar.Size = new System.Drawing.Size(368, 22);
            this.ChartToolbar.TabIndex = 2;
            // 
            // DataLoadProgressBar
            // 
            this.DataLoadProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataLoadProgressBar.Location = new System.Drawing.Point(183, 487);
            this.DataLoadProgressBar.Name = "DataLoadProgressBar";
            this.DataLoadProgressBar.Size = new System.Drawing.Size(537, 14);
            this.DataLoadProgressBar.TabIndex = 3;
            this.DataLoadProgressBar.Visible = false;
            // 
            // DataLoadProgressLabel
            // 
            this.DataLoadProgressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DataLoadProgressLabel.Location = new System.Drawing.Point(12, 487);
            this.DataLoadProgressLabel.Name = "DataLoadProgressLabel";
            this.DataLoadProgressLabel.Size = new System.Drawing.Size(165, 14);
            this.DataLoadProgressLabel.TabIndex = 4;
            this.DataLoadProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ChartStylesCombo
            // 
            this.ChartStylesCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChartStylesCombo.FormattingEnabled = true;
            this.ChartStylesCombo.Location = new System.Drawing.Point(361, 525);
            this.ChartStylesCombo.Name = "ChartStylesCombo";
            this.ChartStylesCombo.Size = new System.Drawing.Size(247, 21);
            this.ChartStylesCombo.TabIndex = 6;
            this.ChartStylesCombo.SelectedIndexChanged += new System.EventHandler(this.ChartStylesCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(288, 527);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Style";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ClearChartButton
            // 
            this.ClearChartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearChartButton.Enabled = false;
            this.ClearChartButton.Location = new System.Drawing.Point(614, 582);
            this.ClearChartButton.Name = "ClearChartButton";
            this.ClearChartButton.Size = new System.Drawing.Size(106, 51);
            this.ClearChartButton.TabIndex = 8;
            this.ClearChartButton.Text = "Clear chart";
            this.ClearChartButton.UseVisualStyleBackColor = true;
            this.ClearChartButton.Click += new System.EventHandler(this.ClearChartButton_Click);
            // 
            // UpdateBarsCheck
            // 
            this.UpdateBarsCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateBarsCheck.AutoSize = true;
            this.UpdateBarsCheck.Location = new System.Drawing.Point(362, 560);
            this.UpdateBarsCheck.Name = "UpdateBarsCheck";
            this.UpdateBarsCheck.Size = new System.Drawing.Size(175, 17);
            this.UpdateBarsCheck.TabIndex = 9;
            this.UpdateBarsCheck.Text = "Update chart while loading bars";
            this.UpdateBarsCheck.UseVisualStyleBackColor = true;
            // 
            // ManageChartStylesButton
            // 
            this.ManageChartStylesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ManageChartStylesButton.Location = new System.Drawing.Point(12, 527);
            this.ManageChartStylesButton.Name = "ManageChartStylesButton";
            this.ManageChartStylesButton.Size = new System.Drawing.Size(106, 51);
            this.ManageChartStylesButton.TabIndex = 10;
            this.ManageChartStylesButton.Text = "Manage chart styles...";
            this.ManageChartStylesButton.UseVisualStyleBackColor = true;
            this.ManageChartStylesButton.Click += new System.EventHandler(this.ManageChartStylesButton_Click);
            // 
            // ChartForm
            // 
            this.AcceptButton = this.LoadDataButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 640);
            this.Controls.Add(this.ManageChartStylesButton);
            this.Controls.Add(this.UpdateBarsCheck);
            this.Controls.Add(this.ClearChartButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChartStylesCombo);
            this.Controls.Add(this.DataLoadProgressLabel);
            this.Controls.Add(this.DataLoadProgressBar);
            this.Controls.Add(this.ChartToolbar);
            this.Controls.Add(this.LoadDataButton);
            this.Controls.Add(this.Chart);
            this.Name = "ChartForm";
            this.Text = "Chart OHLCV data from array";
            ((System.ComponentModel.ISupportInitialize)(this.Chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartToolbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxChartSkil27.AxChart Chart;
        private System.Windows.Forms.Button LoadDataButton;
        private AxChartSkil27.AxChartToolbar ChartToolbar;
        private System.Windows.Forms.ProgressBar DataLoadProgressBar;
        private System.Windows.Forms.Label DataLoadProgressLabel;
        private System.Windows.Forms.ComboBox ChartStylesCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ClearChartButton;
        private System.Windows.Forms.CheckBox UpdateBarsCheck;
        private System.Windows.Forms.Button ManageChartStylesButton;
    }
}

