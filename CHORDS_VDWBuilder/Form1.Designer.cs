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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.FHIR_URL_LBL = new System.Windows.Forms.Label();
            this.FHIR_URL = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusLB = new System.Windows.Forms.ListBox();
            this.patientGV = new System.Windows.Forms.DataGridView();
            this.Patient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Locations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Encounters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Diagnoses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VitalSigns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vitalSignCountLBL = new System.Windows.Forms.Label();
            this.encounterCountLBL = new System.Windows.Forms.Label();
            this.diagnosesCountLBL = new System.Windows.Forms.Label();
            this.vitalSignCountTB = new System.Windows.Forms.TextBox();
            this.encounterCountTB = new System.Windows.Forms.TextBox();
            this.diagnosesCountTB = new System.Windows.Forms.TextBox();
            this.patientCountLBL = new System.Windows.Forms.Label();
            this.patientCountTB = new System.Windows.Forms.TextBox();
            this.Import_FHIR = new System.Windows.Forms.Button();
            this.ClearVDW_BTN = new System.Windows.Forms.Button();
            this.patientsProgressBar = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.vdwServerTB = new System.Windows.Forms.TextBox();
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
            this.FHIR_URL.Size = new System.Drawing.Size(483, 20);
            this.FHIR_URL.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 481);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Scan FHIR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.patientsProgressBar);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.Import_FHIR);
            this.groupBox1.Controls.Add(this.ClearVDW_BTN);
            this.groupBox1.Controls.Add(this.patientGV);
            this.groupBox1.Controls.Add(this.vitalSignCountLBL);
            this.groupBox1.Controls.Add(this.encounterCountLBL);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.diagnosesCountLBL);
            this.groupBox1.Controls.Add(this.vitalSignCountTB);
            this.groupBox1.Controls.Add(this.encounterCountTB);
            this.groupBox1.Controls.Add(this.diagnosesCountTB);
            this.groupBox1.Controls.Add(this.patientCountLBL);
            this.groupBox1.Controls.Add(this.patientCountTB);
            this.groupBox1.Location = new System.Drawing.Point(15, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1185, 521);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FHIR Stats";
            // 
            // statusLB
            // 
            this.statusLB.FormattingEnabled = true;
            this.statusLB.Location = new System.Drawing.Point(15, 591);
            this.statusLB.Name = "statusLB";
            this.statusLB.Size = new System.Drawing.Size(1185, 43);
            this.statusLB.TabIndex = 9;
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
            this.patientGV.Location = new System.Drawing.Point(200, 19);
            this.patientGV.Name = "patientGV";
            this.patientGV.Size = new System.Drawing.Size(964, 415);
            this.patientGV.TabIndex = 8;
            // 
            // Patient
            // 
            this.Patient.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Patient.HeaderText = "Patient";
            this.Patient.Name = "Patient";
            this.Patient.ReadOnly = true;
            this.Patient.Width = 65;
            // 
            // Locations
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Locations.DefaultCellStyle = dataGridViewCellStyle5;
            this.Locations.HeaderText = "Addresses";
            this.Locations.Name = "Locations";
            this.Locations.ReadOnly = true;
            // 
            // Encounters
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Encounters.DefaultCellStyle = dataGridViewCellStyle6;
            this.Encounters.HeaderText = "Encounters";
            this.Encounters.Name = "Encounters";
            this.Encounters.ReadOnly = true;
            // 
            // Diagnoses
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Diagnoses.DefaultCellStyle = dataGridViewCellStyle7;
            this.Diagnoses.HeaderText = "Diagnoses";
            this.Diagnoses.Name = "Diagnoses";
            this.Diagnoses.ReadOnly = true;
            // 
            // VitalSigns
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.VitalSigns.DefaultCellStyle = dataGridViewCellStyle8;
            this.VitalSigns.HeaderText = "VItal Signs";
            this.VitalSigns.Name = "VitalSigns";
            this.VitalSigns.ReadOnly = true;
            // 
            // vitalSignCountLBL
            // 
            this.vitalSignCountLBL.AutoSize = true;
            this.vitalSignCountLBL.Location = new System.Drawing.Point(8, 100);
            this.vitalSignCountLBL.Name = "vitalSignCountLBL";
            this.vitalSignCountLBL.Size = new System.Drawing.Size(85, 13);
            this.vitalSignCountLBL.TabIndex = 7;
            this.vitalSignCountLBL.Text = "Vital Sign Count:";
            // 
            // encounterCountLBL
            // 
            this.encounterCountLBL.AutoSize = true;
            this.encounterCountLBL.Location = new System.Drawing.Point(8, 74);
            this.encounterCountLBL.Name = "encounterCountLBL";
            this.encounterCountLBL.Size = new System.Drawing.Size(90, 13);
            this.encounterCountLBL.TabIndex = 6;
            this.encounterCountLBL.Text = "Encounter Count:";
            // 
            // diagnosesCountLBL
            // 
            this.diagnosesCountLBL.AutoSize = true;
            this.diagnosesCountLBL.Location = new System.Drawing.Point(7, 48);
            this.diagnosesCountLBL.Name = "diagnosesCountLBL";
            this.diagnosesCountLBL.Size = new System.Drawing.Size(91, 13);
            this.diagnosesCountLBL.TabIndex = 5;
            this.diagnosesCountLBL.Text = "Diagnoses Count:";
            // 
            // vitalSignCountTB
            // 
            this.vitalSignCountTB.Location = new System.Drawing.Point(103, 97);
            this.vitalSignCountTB.Name = "vitalSignCountTB";
            this.vitalSignCountTB.Size = new System.Drawing.Size(74, 20);
            this.vitalSignCountTB.TabIndex = 4;
            this.vitalSignCountTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // encounterCountTB
            // 
            this.encounterCountTB.Location = new System.Drawing.Point(103, 71);
            this.encounterCountTB.Name = "encounterCountTB";
            this.encounterCountTB.Size = new System.Drawing.Size(74, 20);
            this.encounterCountTB.TabIndex = 3;
            this.encounterCountTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // diagnosesCountTB
            // 
            this.diagnosesCountTB.Location = new System.Drawing.Point(103, 45);
            this.diagnosesCountTB.Name = "diagnosesCountTB";
            this.diagnosesCountTB.Size = new System.Drawing.Size(74, 20);
            this.diagnosesCountTB.TabIndex = 2;
            this.diagnosesCountTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // patientCountLBL
            // 
            this.patientCountLBL.AutoSize = true;
            this.patientCountLBL.Location = new System.Drawing.Point(7, 22);
            this.patientCountLBL.Name = "patientCountLBL";
            this.patientCountLBL.Size = new System.Drawing.Size(74, 13);
            this.patientCountLBL.TabIndex = 1;
            this.patientCountLBL.Text = "Patient Count:";
            // 
            // patientCountTB
            // 
            this.patientCountTB.Location = new System.Drawing.Point(103, 19);
            this.patientCountTB.Name = "patientCountTB";
            this.patientCountTB.Size = new System.Drawing.Size(74, 20);
            this.patientCountTB.TabIndex = 0;
            this.patientCountTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Import_FHIR
            // 
            this.Import_FHIR.Location = new System.Drawing.Point(173, 481);
            this.Import_FHIR.Name = "Import_FHIR";
            this.Import_FHIR.Size = new System.Drawing.Size(97, 23);
            this.Import_FHIR.TabIndex = 4;
            this.Import_FHIR.Text = "Import to VDW";
            this.Import_FHIR.UseVisualStyleBackColor = true;
            this.Import_FHIR.Click += new System.EventHandler(this.Import_FHIR_Click);
            // 
            // ClearVDW_BTN
            // 
            this.ClearVDW_BTN.Location = new System.Drawing.Point(92, 481);
            this.ClearVDW_BTN.Name = "ClearVDW_BTN";
            this.ClearVDW_BTN.Size = new System.Drawing.Size(75, 23);
            this.ClearVDW_BTN.TabIndex = 10;
            this.ClearVDW_BTN.Text = "Clear VDW";
            this.ClearVDW_BTN.UseVisualStyleBackColor = true;
            this.ClearVDW_BTN.Click += new System.EventHandler(this.ClearVDW_BTN_Click);
            // 
            // patientsProgressBar
            // 
            this.patientsProgressBar.Location = new System.Drawing.Point(105, 447);
            this.patientsProgressBar.Name = "patientsProgressBar";
            this.patientsProgressBar.Size = new System.Drawing.Size(1059, 23);
            this.patientsProgressBar.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 453);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Importing Patients";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(579, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "VDW Connection:";
            // 
            // vdwServerTB
            // 
            this.vdwServerTB.Location = new System.Drawing.Point(678, 17);
            this.vdwServerTB.Name = "vdwServerTB";
            this.vdwServerTB.Size = new System.Drawing.Size(522, 20);
            this.vdwServerTB.TabIndex = 11;
            // 
            // CHORD_VDWBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 646);
            this.Controls.Add(this.vdwServerTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusLB);
            this.Controls.Add(this.groupBox1);
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
        private System.Windows.Forms.DataGridView patientGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Patient;
        private System.Windows.Forms.DataGridViewTextBoxColumn Locations;
        private System.Windows.Forms.DataGridViewTextBoxColumn Encounters;
        private System.Windows.Forms.DataGridViewTextBoxColumn Diagnoses;
        private System.Windows.Forms.DataGridViewTextBoxColumn VitalSigns;
        private System.Windows.Forms.ListBox statusLB;
        private System.Windows.Forms.ProgressBar patientsProgressBar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Import_FHIR;
        private System.Windows.Forms.Button ClearVDW_BTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox vdwServerTB;
    }
}

