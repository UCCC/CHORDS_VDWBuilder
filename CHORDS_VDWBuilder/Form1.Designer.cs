namespace CHORDS_VDWBuilder
{
    partial class CHORD_VDWBuilder
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
            this.FHIR_URL_LBL = new System.Windows.Forms.Label();
            this.FHIR_URL = new System.Windows.Forms.TextBox();
            this.Log_LB = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Import_FHIR = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FHIR_URL_LBL
            // 
            this.FHIR_URL_LBL.AutoSize = true;
            this.FHIR_URL_LBL.Location = new System.Drawing.Point(12, 20);
            this.FHIR_URL_LBL.Name = "FHIR_URL_LBL";
            this.FHIR_URL_LBL.Size = new System.Drawing.Size(60, 13);
            this.FHIR_URL_LBL.TabIndex = 0;
            this.FHIR_URL_LBL.Text = "FHIR URL:";
            this.FHIR_URL_LBL.Click += new System.EventHandler(this.label1_Click);
            // 
            // FHIR_URL
            // 
            this.FHIR_URL.Location = new System.Drawing.Point(78, 17);
            this.FHIR_URL.Name = "FHIR_URL";
            this.FHIR_URL.Size = new System.Drawing.Size(1122, 20);
            this.FHIR_URL.TabIndex = 1;
            // 
            // Log_LB
            // 
            this.Log_LB.FormattingEnabled = true;
            this.Log_LB.Location = new System.Drawing.Point(12, 56);
            this.Log_LB.Name = "Log_LB";
            this.Log_LB.Size = new System.Drawing.Size(1188, 485);
            this.Log_LB.TabIndex = 2;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 559);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1188, 23);
            this.progressBar.TabIndex = 3;
            // 
            // Import_FHIR
            // 
            this.Import_FHIR.Location = new System.Drawing.Point(15, 600);
            this.Import_FHIR.Name = "Import_FHIR";
            this.Import_FHIR.Size = new System.Drawing.Size(75, 23);
            this.Import_FHIR.TabIndex = 4;
            this.Import_FHIR.Text = "Import";
            this.Import_FHIR.UseVisualStyleBackColor = true;
            this.Import_FHIR.Click += new System.EventHandler(this.Import_FHIR_Click);
            // 
            // CHORD_VDWBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 646);
            this.Controls.Add(this.Import_FHIR);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.Log_LB);
            this.Controls.Add(this.FHIR_URL);
            this.Controls.Add(this.FHIR_URL_LBL);
            this.Name = "CHORD_VDWBuilder";
            this.Text = "CHORDS VDW Builder";
            this.Load += new System.EventHandler(this.CHORD_VDWBuilder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FHIR_URL_LBL;
        private System.Windows.Forms.TextBox FHIR_URL;
        private System.Windows.Forms.ListBox Log_LB;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button Import_FHIR;
    }
}

