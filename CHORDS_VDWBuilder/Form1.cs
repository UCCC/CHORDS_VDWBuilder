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
        public List<FHIRPatientSummary> patient_summary;

        public CHORD_VDWBuilder()
        {
            patient_summary = new List<FHIRPatientSummary>();
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

            if (clearVDW.Checked) importer.ClearVDW();

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
            // clear table
            //while (patientGV.Rows.Count > 0)
            //{
            //    patientGV.Rows.RemoveAt(0);
            //}
            var importer = new FHIRToVDW();

            List<FHIRPatientSummary> plist = importer.ScanFHIRDB(client);
            int loc_count = 0;
            int encounter_count = 0;
            int diagnoses_count = 0;
            int vital_count = 0;

            foreach(FHIRPatientSummary s in plist)
            {
                loc_count += s.LocationTotalCount;
                encounter_count += s.EncounterTotalCount;
                diagnoses_count += s.DiagnosesTotalCount;
                vital_count += s.VitalSignTotalCount;

                patientGV.Rows.Add(s.PERSON_ID, s.LocationTotalCount.ToString(), s.EncounterTotalCount.ToString(), s.DiagnosesTotalCount.ToString(), s.VitalSignTotalCount.ToString());
            }

            int patient_Count = plist.Count;
            patientCountTB.Text = patient_Count.ToString();
            diagnosesCountTB.Text = diagnoses_count.ToString();
            encounterCountTB.Text = encounter_count.ToString();
            vitalSignCountTB.Text = vital_count.ToString();

            Cursor.Current = Cursors.Default;
        }
    }
}
