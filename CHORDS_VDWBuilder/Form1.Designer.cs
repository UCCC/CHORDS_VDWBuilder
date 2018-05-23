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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Import_FHIR = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.vitalSignCountLBL = new System.Windows.Forms.Label();
            this.encounterCountLBL = new System.Windows.Forms.Label();
            this.diagnosesCountLBL = new System.Windows.Forms.Label();
            this.vitalSignCountTB = new System.Windows.Forms.TextBox();
            this.encounterCountTB = new System.Windows.Forms.TextBox();
            this.diagnosesCountTB = new System.Windows.Forms.TextBox();
            this.patientCountLBL = new System.Windows.Forms.Label();
            this.patientCountTB = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.patientGV = new System.Windows.Forms.DataGridView();
            this.Patient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Locations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Encounters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Diagnoses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VitalSigns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clearVDW = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientGV)).BeginInit();
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
            this.FHIR_URL.Size = new System.Drawing.Size(1018, 20);
            this.FHIR_URL.TabIndex = 1;
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
            this.Import_FHIR.Location = new System.Drawing.Point(1125, 611);
            this.Import_FHIR.Name = "Import_FHIR";
            this.Import_FHIR.Size = new System.Drawing.Size(75, 23);
            this.Import_FHIR.TabIndex = 4;
            this.Import_FHIR.Text = "Import";
            this.Import_FHIR.UseVisualStyleBackColor = true;
            this.Import_FHIR.Click += new System.EventHandler(this.Import_FHIR_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 611);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Scan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.patientGV);
            this.groupBox1.Controls.Add(this.vitalSignCountLBL);
            this.groupBox1.Controls.Add(this.encounterCountLBL);
            this.groupBox1.Controls.Add(this.diagnosesCountLBL);
            this.groupBox1.Controls.Add(this.vitalSignCountTB);
            this.groupBox1.Controls.Add(this.encounterCountTB);
            this.groupBox1.Controls.Add(this.diagnosesCountTB);
            this.groupBox1.Controls.Add(this.patientCountLBL);
            this.groupBox1.Controls.Add(this.patientCountTB);
            this.groupBox1.Location = new System.Drawing.Point(15, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1185, 374);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FHIR Stats";
            // 
            // vitalSignCountLBL
            // 
            this.vitalSignCountLBL.AutoSize = true;
            this.vitalSignCountLBL.Location = new System.Drawing.Point(6, 145);
            this.vitalSignCountLBL.Name = "vitalSignCountLBL";
            this.vitalSignCountLBL.Size = new System.Drawing.Size(85, 13);
            this.vitalSignCountLBL.TabIndex = 7;
            this.vitalSignCountLBL.Text = "Vital Sign Count:";
            // 
            // encounterCountLBL
            // 
            this.encounterCountLBL.AutoSize = true;
            this.encounterCountLBL.Location = new System.Drawing.Point(6, 106);
            this.encounterCountLBL.Name = "encounterCountLBL";
            this.encounterCountLBL.Size = new System.Drawing.Size(90, 13);
            this.encounterCountLBL.TabIndex = 6;
            this.encounterCountLBL.Text = "Encounter Count:";
            // 
            // diagnosesCountLBL
            // 
            this.diagnosesCountLBL.AutoSize = true;
            this.diagnosesCountLBL.Location = new System.Drawing.Point(6, 69);
            this.diagnosesCountLBL.Name = "diagnosesCountLBL";
            this.diagnosesCountLBL.Size = new System.Drawing.Size(91, 13);
            this.diagnosesCountLBL.TabIndex = 5;
            this.diagnosesCountLBL.Text = "Diagnoses Count:";
            // 
            // vitalSignCountTB
            // 
            this.vitalSignCountTB.Location = new System.Drawing.Point(103, 142);
            this.vitalSignCountTB.Name = "vitalSignCountTB";
            this.vitalSignCountTB.Size = new System.Drawing.Size(100, 20);
            this.vitalSignCountTB.TabIndex = 4;
            this.vitalSignCountTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // encounterCountTB
            // 
            this.encounterCountTB.Location = new System.Drawing.Point(103, 103);
            this.encounterCountTB.Name = "encounterCountTB";
            this.encounterCountTB.Size = new System.Drawing.Size(100, 20);
            this.encounterCountTB.TabIndex = 3;
            this.encounterCountTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // diagnosesCountTB
            // 
            this.diagnosesCountTB.Location = new System.Drawing.Point(103, 66);
            this.diagnosesCountTB.Name = "diagnosesCountTB";
            this.diagnosesCountTB.Size = new System.Drawing.Size(100, 20);
            this.diagnosesCountTB.TabIndex = 2;
            this.diagnosesCountTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // patientCountLBL
            // 
            this.patientCountLBL.AutoSize = true;
            this.patientCountLBL.Location = new System.Drawing.Point(6, 33);
            this.patientCountLBL.Name = "patientCountLBL";
            this.patientCountLBL.Size = new System.Drawing.Size(74, 13);
            this.patientCountLBL.TabIndex = 1;
            this.patientCountLBL.Text = "Patient Count:";
            // 
            // patientCountTB
            // 
            this.patientCountTB.Location = new System.Drawing.Point(103, 30);
            this.patientCountTB.Name = "patientCountTB";
            this.patientCountTB.Size = new System.Drawing.Size(100, 20);
            this.patientCountTB.TabIndex = 0;
            this.patientCountTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(15, 440);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1185, 97);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Import Stats";
            // 
            // patientGV
            // 
            this.patientGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Patient,
            this.Locations,
            this.Encounters,
            this.Diagnoses,
            this.VitalSigns});
            this.patientGV.Location = new System.Drawing.Point(239, 19);
            this.patientGV.Name = "patientGV";
            this.patientGV.Size = new System.Drawing.Size(925, 337);
            this.patientGV.TabIndex = 8;
            // 
            // Patient
            // 
            this.Patient.HeaderText = "Patient";
            this.Patient.Name = "Patient";
            // 
            // Locations
            // 
            this.Locations.HeaderText = "Locations";
            this.Locations.Name = "Locations";
            // 
            // Encounters
            // 
            this.Encounters.HeaderText = "Encounters";
            this.Encounters.Name = "Encounters";
            // 
            // Diagnoses
            // 
            this.Diagnoses.HeaderText = "Diagnoses";
            this.Diagnoses.Name = "Diagnoses";
            // 
            // VitalSigns
            // 
            this.VitalSigns.HeaderText = "VItal Signs";
            this.VitalSigns.Name = "VitalSigns";
            // 
            // clearVDW
            // 
            this.clearVDW.AutoSize = true;
            this.clearVDW.Location = new System.Drawing.Point(1121, 20);
            this.clearVDW.Name = "clearVDW";
            this.clearVDW.Size = new System.Drawing.Size(79, 17);
            this.clearVDW.TabIndex = 8;
            this.clearVDW.Text = "Clear VDW";
            this.clearVDW.UseVisualStyleBackColor = true;
            // 
            // CHORD_VDWBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 646);
            this.Controls.Add(this.clearVDW);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Import_FHIR);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.FHIR_URL);
            this.Controls.Add(this.FHIR_URL_LBL);
            this.Name = "CHORD_VDWBuilder";
            this.Text = "CHORDS VDW Builder";
            this.Load += new System.EventHandler(this.CHORD_VDWBuilder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FHIR_URL_LBL;
        private System.Windows.Forms.TextBox FHIR_URL;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button Import_FHIR;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label patientCountLBL;
        private System.Windows.Forms.TextBox patientCountTB;
        private System.Windows.Forms.Label diagnosesCountLBL;
        private System.Windows.Forms.TextBox vitalSignCountTB;
        private System.Windows.Forms.TextBox encounterCountTB;
        private System.Windows.Forms.TextBox diagnosesCountTB;
        private System.Windows.Forms.Label vitalSignCountLBL;
        private System.Windows.Forms.Label encounterCountLBL;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView patientGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Patient;
        private System.Windows.Forms.DataGridViewTextBoxColumn Locations;
        private System.Windows.Forms.DataGridViewTextBoxColumn Encounters;
        private System.Windows.Forms.DataGridViewTextBoxColumn Diagnoses;
        private System.Windows.Forms.DataGridViewTextBoxColumn VitalSigns;
        private System.Windows.Forms.CheckBox clearVDW;
    }
}

