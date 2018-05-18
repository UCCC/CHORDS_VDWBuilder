using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;
using CHORDS_VDWBuilder.Models;
using CHORDS_VDWBuilder.CHORDS.FHIRToVDW;
using log4net;

namespace CHORDS_VDWBuilder
{
    public partial class CHORD_VDWBuilder : Form
    {

        public CHORD_VDWBuilder()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Import_FHIR_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // Connect to FHIR Server
            var client = new FhirClient(FHIR_URL.Text);
            client.PreferredFormat = ResourceFormat.Json;
            client.Timeout = 120000;

            var importer = new FHIRToVDW();

            int count = importer.LoadVDW(client);

            patientCountTB.Text = count.ToString();

            Cursor.Current = Cursors.Default;
        }

        private void CHORD_VDWBuilder_Load(object sender, EventArgs e)
        {
            FHIR_URL.Text = "https://sb-fhir-stu3.smarthealthit.org/smartstu3/open";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // Connect to FHIR Server
            var client = new FhirClient(FHIR_URL.Text);
            client.PreferredFormat = ResourceFormat.Json;
            client.Timeout = 120000;

            var importer = new FHIRToVDW();

            int patient_Count = importer.PatientCount(client);

            patientCountTB.Text = patient_Count.ToString();

            int diagnoses_Count = importer.DiagnosesCount(client);

            diagnosesCountTB.Text = diagnoses_Count.ToString();

            int encounter_Count = importer.EncounterCount(client);

            encounterCountTB.Text = encounter_Count.ToString();

            int vitalSign_Count = importer.VitalSignCount(client);

            vitalSignCountTB.Text = vitalSign_Count.ToString();

            Cursor.Current = Cursors.Default;
        }
    }
}
