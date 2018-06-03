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

        private FHIRToVDW mFHIRToVDW = null;
        private FhirClient mFHIRClient = null;

        public CHORD_VDWBuilder()
        {
            patient_summary = new List<FHIRPatientSummary>();
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Import FHIR to a CHORDS VDW
        private void Import_FHIR_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            clear();

            // Connect to FHIR Server
            var client = new FhirClient(FHIR_URL.Text);
            client.PreferredFormat = ResourceFormat.Json;
            client.Timeout = 120000;

            //var importer = new FHIRToVDW();

            List<FHIRPatientSummary> plist = mFHIRToVDW.LoadVDW();

            int loc_count = 0;
            int encounter_count = 0;
            int diagnoses_count = 0;
            int vital_count = 0;

            foreach (FHIRPatientSummary s in plist)
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

        private void CHORD_VDWBuilder_Load(object sender, EventArgs e)
        {
            //FHIR_URL.Text = "http://hapi.fhir.org/baseDstu3";
            FHIR_URL.Text = "https://sb-fhir-stu3.smarthealthit.org/smartstu3/open";
            using (var context = new VDW_3_1_Entities())
            {
                try
                {
                    vdwServerTB.Text = context.Database.Connection.ConnectionString;
                }
                catch (Exception ex)
                {
                    vdwServerTB.Text = ex.Message;
                }

            }
            mFHIRClient = new FhirClient(FHIR_URL.Text);
            mFHIRToVDW = new FHIRToVDW(mFHIRClient, statusLB, patientsProgressBar);
        }

        // Scan FHIR Source
        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            clear();

            // Connect to FHIR Server
            var client = new FhirClient(FHIR_URL.Text);
            client.PreferredFormat = ResourceFormat.Json;
            client.Timeout = 120000;

            List<FHIRPatientSummary> plist = mFHIRToVDW.ScanFHIRDB();

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

        // 
        private void button2_Click(object sender, EventArgs e)
        {

        }

        // Clear the VDW
        private void ClearVDW_BTN_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            mFHIRToVDW.ClearVDW();

            Cursor.Current = Cursors.Default;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void clear()
        {
            patientCountTB.Text = "";
            diagnosesCountTB.Text = "";
            encounterCountTB.Text = "";
            vitalSignCountTB.Text = "";

            patientGV.DataSource = null;
            patientGV.Rows.Clear();

            return;
        }
    }
}
