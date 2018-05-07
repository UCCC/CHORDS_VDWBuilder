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
            // Connect to FHIR Server
            var client = new FhirClient(FHIR_URL.Text);
            client.PreferredFormat = ResourceFormat.Json;
            client.Timeout = 120000;

            Log_LB.Items.Add("Connected to: " + FHIR_URL.Text);

            // Build list of Patients and Fill in Demographic Table
            List<Patient> patients = loadPatients(client);

            // Build Death Table
            ;

        }

        // Load Valid Patients
        private List<Patient> loadPatients(FhirClient iClient)
        {
            List<Patient> result = new List<Patient>();

            if(iClient != null)
            {
                Bundle patients = iClient.Search<Patient>();

                Log_LB.Items.Add("Number of Patients: " + patients.Entry.Count.ToString());

                int successful = 0;

                using (var context = new VDW_3_1_Entities())
                {
                    // Add Patients to VDW
                    foreach (Bundle.EntryComponent item in patients.Entry)
                    {
                        Patient p = (Patient)item.Resource;

                        if (p.Identifier.Count > 0)
                        {
                            // build demographics record
                            DEMOGRAPHICS demo = new DEMOGRAPHICS();

                            // PERSON_ID
                            demo.PERSON_ID = p.Identifier.FirstOrDefault().Value;

                            // MRN
                            demo.MRN = p.Identifier.FirstOrDefault().Value;

                            // BIRTH_DATE
                            if (p.BirthDate != null)
                            {
                                demo.BIRTH_DATE = DateTime.ParseExact(p.BirthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                            }

                            // GENDER
                            if (p.Gender == AdministrativeGender.Female)
                            {
                                demo.GENDER = "F";
                            }
                            if (p.Gender == AdministrativeGender.Male)
                            {
                                demo.GENDER = "M";
                            }
                            if (p.Gender == AdministrativeGender.Other)
                            {
                                demo.GENDER = "O";
                            }
                            if (p.Gender == AdministrativeGender.Unknown)
                            {
                                demo.GENDER = "U";
                            }

                            // PRIMARY_LANGUAGE and NEEDS_INTERPRETER
                            if (p.Communication.Count > 0)
                            {
                                demo.PRIMARY_LANGUAGE = p.Communication.FirstOrDefault().Language.Text;
                                if (p.Communication.FirstOrDefault().Preferred != null)
                                {
                                    demo.NEEDS_INTERPRETER = "S";
                                }
                            }
                            else
                            {
                                demo.PRIMARY_LANGUAGE = "";
                                demo.NEEDS_INTERPRETER = "";
                            }

                            if (p.Extension.Count() > 0)
                            {
                                // RACE1
                                ;

                                // RACE2
                                ;

                                // RACE3
                                ;

                                // RACE4
                                ;

                                // RACE5
                                ;

                                // HISPANIC
                                ;

                                // SEXUAL_ORIENTATION
                                ;

                                // GENDER_IDENTITY
                                ;
                            }
                            else
                            {
                                // RACE1
                                demo.RACE1 = "UN";

                                // RACE2
                                demo.RACE2 = "UN";

                                // RACE3
                                demo.RACE3 = "UN";

                                // RACE4
                                demo.RACE4 = "UN";

                                // RACE5
                                demo.RACE5 = "UN";

                                // HISPANIC
                                demo.HISPANIC = "U";

                                // SEXUAL_ORIENTATION
                                demo.SEXUAL_ORIENTATION = null;

                                // GENDER_IDENTITY
                                demo.GENDER_IDENTITY = null;
                            }

                            try
                            {
                                context.DEMOGRAPHICS.Add(demo);
                                context.SaveChanges();

                                result.Add(p);
                                successful++;
                            }
                            catch (Exception ex)
                            {
                                Log_LB.Items.Add("Error: " + ex.Message);
                            }
                        }
                        else
                        {
                            Log_LB.Items.Add("Patient missing identifier: " + p.Name.FirstOrDefault().Family);
                        }
                    }
                    Log_LB.Items.Add("Number of Patients added: " + successful.ToString());
                }

            }

            return result;
        }

        private int loadDeathTable(FhirClient iClient, List<Patient> iPatients)
        {
            int result = 0;

            foreach(Patient p in iPatients)
            {
                // check to see if patient is deceased
                if((FhirBoolean) p.Deceased == new FhirBoolean())
                {
                    ;
                }
            }

            return result;
        }

        private void CHORD_VDWBuilder_Load(object sender, EventArgs e)
        {
            FHIR_URL.Text = "http://vonk.fire.ly";
        }
    }
}
