using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;
using CHORDS_VDWBuilder.Models;
using RestSharp;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace CHORDS_VDWBuilder.CHORDS.FHIRToVDW
{
    class FHIRToVDW
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private FhirClient m_FhirClient = null;
        private ProgressBar m_ProgressBar = null;
        private ListBox m_Status = null;

        public FHIRToVDW(FhirClient iFhirClient, ListBox iStatus = null, ProgressBar iPatientProgressBar = null)
        {
            m_FhirClient = iFhirClient;
            m_ProgressBar = iPatientProgressBar;
            m_Status = iStatus;
        }

        public bool ClearVDW()
        {
            bool result = false;
            m_ProgressBar.Maximum = 5;
            m_ProgressBar.Step = 1;
            m_ProgressBar.Value = 0;

            using (var context = new VDW_3_1_Entities())
            {
                try
                {
                    // clear priority 1 tables
                    context.CENSUS_LOCATION.RemoveRange(context.CENSUS_LOCATION);
                    m_ProgressBar.Value = m_ProgressBar.Value + 1;

                    context.DIAGNOSES.RemoveRange(context.DIAGNOSES);
                    m_ProgressBar.Value = m_ProgressBar.Value + 1;

                    context.ENCOUNTERS.RemoveRange(context.ENCOUNTERS);
                    m_ProgressBar.Value = m_ProgressBar.Value + 1;

                    context.VITAL_SIGNS.RemoveRange(context.VITAL_SIGNS);
                    m_ProgressBar.Value = m_ProgressBar.Value + 1;

                    context.DEMOGRAPHICS.RemoveRange(context.DEMOGRAPHICS);
                    m_ProgressBar.Value = m_ProgressBar.Value + 1;

                    // clear priority 2 tables
                    ;

                    // clear priority 3 tables
                    ;

                    context.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                    log.Info("Error: " + ex.Message);
                    m_Status.Items.Add("Error: " + ex.Message);
                    m_Status.SelectedIndex = m_Status.Items.Count - 1;
                }

            }

            if(result)
            {
                m_Status.Items.Add("VDW Cleared.");
                m_Status.SelectedIndex = m_Status.Items.Count - 1;
            }
            return result;
        }

        public List<FHIRPatientSummary> ScanFHIRDB()
        {
            List<FHIRPatientSummary> results = new List<FHIRPatientSummary>();

            log.Info("Start Scanning Patients");
            m_Status.Items.Add("Start Scanning Patients");

            try
            {
                Bundle bundles = m_FhirClient.Search<Patient>(new string[] { "_count=100" });

                // get all the patients from all the pages
                List<Patient> patients = new List<Patient>();
                while( bundles != null)
                {
                    foreach (Bundle.EntryComponent item in bundles.Entry)
                    {
                        Patient p = (Patient)item.Resource;
                        patients.Add(p);
                    }
                    bundles = m_FhirClient.Continue(bundles);
                    m_Status.Items.Add("Getting Patients: " + patients.Count.ToString());
                    m_Status.SelectedIndex = m_Status.Items.Count - 1;
                }

                m_Status.Items.Add("Loading " + patients.Count.ToString() + " patients.");
                m_ProgressBar.Maximum = patients.Count;
                m_ProgressBar.Step = 1;
                m_ProgressBar.Value = 0;

                int total = patients.Count;
                int cur = 1;

                foreach (var p in patients)
                {
                    m_Status.Items.Add("  Loading Patient " + cur.ToString() + " out of " + total.ToString());
                    m_Status.SelectedIndex = m_Status.Items.Count - 1;

                    FHIRPatientSummary sum = new FHIRPatientSummary();
                    sum.PERSON_ID = p.Id;

                    // Location Count
                    sum.LocationTotalCount = p.Address.Count;

                    // Encounter Count
                    Bundle en = m_FhirClient.Search<Encounter>(new string[] { "patient=" + p.Id });
                    sum.EncounterTotalCount = en.Entry.Count;

                    // Diagnoses Count
                    Bundle d = m_FhirClient.Search<DiagnosticReport>(new string[] { "patient=" + p.Id });
                    sum.DiagnosesTotalCount = d.Entry.Count;

                    // Vital Signs Count
                    Bundle o = m_FhirClient.Search<Observation>(new string[] { "patient=" + p.Id });
                    foreach (var tempD in o.Entry)
                    {
                        Observation obs = (Observation)tempD.Resource;

                        foreach (CodeableConcept cc in obs.Category)
                        {
                            foreach (var code in cc.Coding)
                            {
                                if (code.Code == "vital-signs")
                                {
                                    sum.VitalSignTotalCount++;
                                }
                            }
                        }
                    }

                    results.Add(sum);

                    cur++;
                    m_ProgressBar.Value = m_ProgressBar.Value + 1;
                }

                log.Info("Number of Patients " + results.Count.ToString());

                log.Info("End Scanning Patients");

                m_Status.Items.Add("End Scanning Patients");
                m_Status.SelectedIndex = m_Status.Items.Count - 1;
            }
            catch (Exception ex)
            {
                log.Info("Error: " + ex.Message);

                m_Status.Items.Add("Error: " + ex.Message);
                m_Status.Items.Add("End Scanning Patients");
            }

            return results;
        }

        public List<FHIRPatientSummary> LoadVDW()
        {
            List<FHIRPatientSummary> results = new List<FHIRPatientSummary>();

            log.Info("Start Importing Patients");

            using (var context = new VDW_3_1_Entities())
            {
                try
                {
                    Bundle bundles = m_FhirClient.Search<Patient>(new string[] { "_count=100" });

                    // get all the patients from all the pages
                    List<Patient> patients = new List<Patient>();
                    while (bundles != null)
                    {
                        foreach (Bundle.EntryComponent item in bundles.Entry)
                        {
                            Patient p = (Patient)item.Resource;
                            patients.Add(p);
                        }
                        bundles = m_FhirClient.Continue(bundles);
                    }

                    m_ProgressBar.Maximum = patients.Count;
                    m_ProgressBar.Step = 1;
                    m_ProgressBar.Value = 0;

                    int total = patients.Count;
                    int cur = 1;

                    foreach (var p in patients)
                    {

                        FHIRPatientSummary sum = new FHIRPatientSummary();
                        sum.PERSON_ID = p.Id;

                        // build and save DEMOGRAPHIC record for patient
                        DEMOGRAPHICS demo = buildDemographic(p);

                        try
                        {
                            context.DEMOGRAPHICS.Add(demo);
                            context.SaveChanges();

                            // build and save CENSUS_LOCATION records
                            List<CENSUS_LOCATION> locs = buildGeocode(p);
                            foreach (var loc in locs)
                            {
                                try
                                {
                                    context.CENSUS_LOCATION.Add(loc);
                                    context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    log.Info("Error: " + ex.Message);
                                }
                            }

                            // build and save Encounter records for patient
                            Bundle encounters = m_FhirClient.Search<Encounter>(new string[] { "patient=" + p.Id });

                            foreach (var e_item in encounters.Entry)
                            {
                                ENCOUNTERS tENCOUNTERS = buildEncounter((Encounter)e_item.Resource, p);

                                try
                                {
                                    //context.ENCOUNTERS.Add(tENCOUNTERS);
                                    //context.SaveChanges();

                                    // build and save Diagnoses records associated with this encounter
                                    ;

                                }
                                catch (Exception ex)
                                {
                                    log.Info("Error: " + ex.Message);
                                }
                            }

                            // build and save VITAL_SIGN records for patient
                            Bundle o = m_FhirClient.Search<Observation>(new string[] { "patient=" + p.Id });
                            foreach (var tempD in o.Entry)
                            {
                                Observation obs = (Observation)tempD.Resource;

                                foreach (CodeableConcept cc in obs.Category)
                                {
                                    foreach (var code in cc.Coding)
                                    {
                                        if (code.Code == "vital-signs")
                                        {
                                            sum.VitalSignTotalCount++;
                                        }
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            log.Info("Error: " + ex.Message);
                        }

                        // Location Count
                        sum.LocationTotalCount = p.Address.Count;

                        // Encounter Count
                        Bundle en = m_FhirClient.Search<Encounter>(new string[] { "patient=" + p.Id });
                        sum.EncounterTotalCount = en.Entry.Count;

                        // Diagnoses Count
                        Bundle d = m_FhirClient.Search<DiagnosticReport>(new string[] { "patient=" + p.Id });
                        sum.DiagnosesTotalCount = d.Entry.Count;

                        results.Add(sum);

                        cur++;

                        m_ProgressBar.Value = m_ProgressBar.Value + 1;
                    }

                    log.Info("Number of Patients " + results.Count.ToString());

                    log.Info("End Importing Patients");
                }
                catch (Exception ex)
                {
                    log.Info("Error: " + ex.Message);
                }
            }

            return results;
        }

        // 
        private List<Patient> loadPatients(FhirClient iClient)
        {
            int diagnoses_count = 0;
            int encounter_count = 0;
            int vital_sign_count = 0;
            int census_loc_count = 0;

            List<Patient> result = new List<Patient>();

            if (iClient != null)
            {
                Bundle patients = iClient.Search<Patient>();

                log.Info("Number of Patients: " + patients.Entry.Count.ToString());

                int successful = 0;

                using (var context = new VDW_3_1_Entities())
                {
                    // Add Patients to VDW
                    foreach (Bundle.EntryComponent item in patients.Entry)
                    {
                        Patient p = (Patient)item.Resource;

                        // build and save DEMOGRAPHIC record for patient
                        DEMOGRAPHICS demo = buildDemographic(p);

                        try
                        {
                            context.DEMOGRAPHICS.Add(demo);
                            context.SaveChanges();

                            result.Add(p);
                            successful++;
                        }
                        catch (Exception ex)
                        {
                            log.Info("Error: " + ex.Message);
                        }

                        // build and save CENSUS_LOCATION records
                        List<CENSUS_LOCATION> locs = buildGeocode(p);
                        foreach(var loc in locs)
                        {
                            try
                            {
                                context.CENSUS_LOCATION.Add(loc);
                                context.SaveChanges();

                                census_loc_count++;
                            }
                            catch (Exception ex)
                            {
                                log.Info("Error: " + ex.Message);
                            }
                        }

                        // build and save Encounter records for patient
                        Bundle encounters = iClient.Search<Encounter>(new string[] { "patient=" + p.Id });

                        foreach(var e_item in  encounters.Entry)
                        {
                            ENCOUNTERS tENCOUNTERS = buildEncounter((Encounter) e_item.Resource, p);

                            if(tENCOUNTERS != null)
                            {
                                try
                                {
                                    context.ENCOUNTERS.Add(tENCOUNTERS);
                                    context.SaveChanges();

                                    encounter_count++;

                                    // build and save Diagnoses records associated with this encounter
                                    ;

                                }
                                catch (Exception ex)
                                {
                                    log.Info("Error: " + ex.Message);
                                }
                            }
                        }

                        // build and save VITAL_SIGN records for patient
                        Bundle o = iClient.Search<Observation>(new string[] { "patient=" + p.Id });
                        foreach (var tempD in o.Entry)
                        {
                            Observation obs = (Observation)tempD.Resource;

                            ;

                        }

                    }
                    log.Info("Number of Patients added: " + successful.ToString());
                    log.Info("Number of Encounters added: " + encounter_count.ToString()); ; ; ; ;
                }

            }

            return result;
        }

        private DEMOGRAPHICS buildDemographic(Patient iPatient)
        {
            DEMOGRAPHICS result = new DEMOGRAPHICS();

            if (iPatient.Identifier.Count > 0)
            {
                // PERSON_ID
                result.PERSON_ID = iPatient.Id;

                // MRN
                foreach(Identifier i in iPatient.Identifier)
                {
                    if(i.Type != null && i.Type.Coding.FirstOrDefault().Code == "MR")
                    {
                        result.MRN = iPatient.Identifier.FirstOrDefault().Value;
                    }
                }

                // BIRTH_DATE
                if (iPatient.BirthDate != null)
                {
                    result.BIRTH_DATE = DateTime.ParseExact(iPatient.BirthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }

                // GENDER
                if (iPatient.Gender == AdministrativeGender.Female)
                {
                    result.GENDER = "F";
                }
                else if (iPatient.Gender == AdministrativeGender.Male)
                {
                    result.GENDER = "M";
                }
                else if (iPatient.Gender == AdministrativeGender.Other)
                {
                    result.GENDER = "O";
                }
                else if (iPatient.Gender == AdministrativeGender.Unknown)
                {
                    result.GENDER = "U";
                }
                else
                {
                    result.GENDER = "U";
                }

                // PRIMARY_LANGUAGE and NEEDS_INTERPRETER
                if (iPatient.Communication.Count > 0)
                {
                    string lang = iPatient.Communication.FirstOrDefault().Language.Coding.FirstOrDefault().Code;
                    int end = lang.IndexOf('-');
                    if (end < 0)
                    {
                        end = lang.Length < 4 ? lang.Length : 3;
                    }
                        
                    result.PRIMARY_LANGUAGE = lang.Substring(0, end);

                    if (iPatient.Communication.FirstOrDefault().Preferred != null)
                    {
                        result.NEEDS_INTERPRETER = "S";
                    }
                    else
                    {
                        result.NEEDS_INTERPRETER = "";
                    }
                }
                else
                {
                    result.PRIMARY_LANGUAGE = "";
                    result.NEEDS_INTERPRETER = "";
                }

                // race 
                result.RACE1 = "UN";
                result.RACE2 = "UN";
                result.RACE3 = "UN";
                result.RACE4 = "UN";
                result.RACE5 = "UN";
                result.HISPANIC = "U";
                result.SEXUAL_ORIENTATION = null;
                result.GENDER_IDENTITY = null;

                if (iPatient.Extension.Count() > 0)
                {
                    int race_count = 0;
                    foreach(Extension e in iPatient.Extension)
                    {
                        if(e.TypeName == "CodeableConcept")
                        {
                            CodeableConcept c = (CodeableConcept)e.Value;

                            if (c.Text == "race")
                            {
                                if (race_count == 0)
                                {
                                    result.RACE1 = RaceAbbreviationGivenConceptCode(c.Coding.FirstOrDefault().Code);
                                    race_count++;
                                }
                                else if (race_count == 1)
                                {
                                    result.RACE2 = RaceAbbreviationGivenConceptCode(c.Coding.FirstOrDefault().Code);
                                    race_count++;
                                }
                                else if (race_count == 2)
                                {
                                    result.RACE3 = RaceAbbreviationGivenConceptCode(c.Coding.FirstOrDefault().Code);
                                    race_count++;
                                }
                                else if (race_count == 3)
                                {
                                    result.RACE4 = RaceAbbreviationGivenConceptCode(c.Coding.FirstOrDefault().Code);
                                    race_count++;
                                }
                                else if (race_count == 4)
                                {
                                    result.RACE5 = RaceAbbreviationGivenConceptCode(c.Coding.FirstOrDefault().Code);
                                    race_count++;
                                }
                            }

                            if (c.Text == "ethnicity")
                            {
                                result.HISPANIC = EthnicityAbbreviationGivenConceptCode(c.Coding.FirstOrDefault().Code);
                            }
                        }
                    }

                    // SEXUAL_ORIENTATION
                    result.SEXUAL_ORIENTATION = null;

                    // GENDER_IDENTITY
                    result.GENDER_IDENTITY = null;
                }
            }

            return result;
        }

        private ENCOUNTERS buildEncounter(Encounter iEncounter, Patient iPatient)
        {
            ENCOUNTERS result = new ENCOUNTERS();

            if(iPatient.Id.Length > 0)
            {
                // PERSON_ID
                result.PERSON_ID = iPatient.Id;

                // ADATE
                if(iEncounter.Period != null && iEncounter.Period.StartElement != null && iEncounter.Period.StartElement.ToDateTime() != null)
                {
                    result.ADATE = iEncounter.Period.StartElement.ToDateTime().Value;

                    // ENC_ID
                    result.ENC_ID = iEncounter.Id;

                    // DDATE
                    if(iEncounter.Period != null && iEncounter.Period.EndElement != null && iEncounter.Period.EndElement.ToDateTime() != null)
                    {
                        result.DDATE = iEncounter.Period.EndElement.ToDateTime().Value;
                    }
                    else
                    {
                        result.DDATE = null;
                    }

                    // PROVIDER
                    string id = "";
                    Bundle provider = m_FhirClient.Search<Organization>(new string[] { "Identifier=" + id });
                    result.PROVIDER = provider.Id;

                    // ENC_COUNT
                    if (iEncounter.PartOf == null)
                    {
                        result.ENC_COUNT = 1;
                    }
                    else
                    {
                        // get parent encounter to determine order number
                        ;
                    }

                    // DRG_VERSION
                    ;

                    // DRG_VALUE
                    ;

                    // ENCTYPE
                    //result.ENCTYPE = iEncounter.Type;

                    // ENCOUNTER_SUBTYPE
                    //result.ENCOUNTER_SUBTYPE = ;

                    // FACILITY_CODE
                    string fc = "UNK";
                    foreach (var l in iEncounter.Location)
                    {
                        //if(l.Location.Identifier.;
                        ;
                    }
                    result.FACILITY_CODE = fc;

                    // DISCHARGE_DISPOSITION
                    ;

                    // DISCHARGE_STATUS
                    ;

                    // ADMITTING_SOURCE
                    ;

                    // DEPARTMENT
                    ;
                }
                else
                {
                    // error this date is required
                    log.Info("Patient: " + iPatient.Id + " encounter date is missing.");
                    result = null;
                }

            }
            else
            {
                // Invalid Patient ID
                log.Info("Error: Missing Patient ID");
                result = null;
            }

            return result;
        }

        private List<CENSUS_LOCATION> buildGeocode(Patient iPatient)
        {
            List<CENSUS_LOCATION> result = new List<CENSUS_LOCATION>();


            foreach(var add in iPatient.Address)
            {
                CENSUS_LOCATION new_location = new CENSUS_LOCATION();

                string address = "";
                string street = "";
                string city = "";
                string state = "";
                string postal_code = "";

                int c = 0;
                foreach (var line in add.Line)
                {
                    if (c == 0) street = line;
                    address = address + line + " ";
                    c++;
                }
                address = address + ", " + add.City + ", " + add.State + " " + add.PostalCode;
                city = add.City;
                state = add.State;
                postal_code = add.PostalCode;

                // use US Gov Census REST API to get location
                Uri geocoder = new Uri("https://geocoding.geo.census.gov");
                var client = new RestClient();
                client.BaseUrl = geocoder;

                // use US Gov Census API to get Census Tract
                var geographies_request = new RestRequest();
                geographies_request.Resource = "/geocoder/geographies/address?street=" + street + "&city=" + city + "&state=" + state + "&benchmark=Public_AR_Census2010&vintage=Census2010_Census2010&layer=14" + "&format=json";
                IRestResponse geographies_response = client.Execute(geographies_request);

                if(geographies_response.IsSuccessful)
                {
                    Census json = null;
                    json = JsonConvert.DeserializeObject<Census>(geographies_response.Content);


                    // fill in CENSUS_LOCATION
                    new_location.PERSON_ID = iPatient.Id;
                    if (add.Period != null && add.Period.StartElement.ToDateTime().HasValue)
                    {
                        new_location.LOC_START = add.Period.StartElement.ToDateTime().Value;
                    }
                    else
                    {
                        new_location.LOC_START = DateTime.Now;
                    }

                    if (add.Period != null && add.Period.EndElement.ToDateTime().HasValue)
                    {
                        new_location.LOC_END = add.Period.EndElement.ToDateTime().Value;
                    }
                    else
                    {
                        new_location.LOC_END = null;
                    }


                    // GEOCODE
                    if (json != null && json.result != null && json.result.addressMatches != null && json.result.addressMatches.Count() > 0)
                    {
                        new_location.GEOCODE = json.result.addressMatches[0].geographies.censusblocks[0].GEOID;
                        // CITY_GEOCODE - using lat/log determine the city
                        ;

                        // GEOCODE_BOUNDARY_YEAR
                        new_location.GEOCODE_BOUNDARY_YEAR = 2010;

                        // GEOLEVEL - should be based on the size of the GEOCODE
                        new_location.GEOLEVEL = "T";

                        // MATCH_STRENGTH
                        new_location.MATCH_STRENGTH = null;

                        // LATITUDE
                        if (json != null)
                        {
                            new_location.LATITUDE = Convert.ToDecimal(json.result.addressMatches[0].coordinates.x);
                        }

                        // LONGITUDE
                        if (json != null)
                        {
                            new_location.LATITUDE = Convert.ToDecimal(json.result.addressMatches[0].coordinates.y);
                        }

                        // GEOCODE_APP
                        new_location.GEOCODE_APP = "US Census API";

                        try
                        {
                            result.Add(new_location);
                        }
                        catch (Exception ex)
                        {
                            log.Info(ex.Message);
                        }
                    }
                    else
                    {
                        // not able to geocode location
                        log.Info("Error: Patient (" + iPatient.Id + ") Not able to geocode '" + address + "'");
                    }

                }
                else
                {
                    // not able to geocode location
                    log.Info("Error: Not able to geocode '" + address + "'");
                }
            }

            return result;
        }

        private List<VITAL_SIGNS> buildVitalSigns(FhirClient iClient, Patient iPatient)
        {
            List<VITAL_SIGNS> results = new List<VITAL_SIGNS>();

            Bundle o = iClient.Search<Observation>(new string[] { "patient=" + iPatient.Id });
            foreach (var tempD in o.Entry)
            {
                Observation obs = (Observation)tempD.Resource;

                foreach (CodeableConcept cc in obs.Category)
                {
                    foreach (var code in cc.Coding)
                    {
                        if (code.Code == "vital-signs")
                        {
                            // build VITAL_SIGNS rec
                            ;

                            ;
                        }
                    }
                }
            }

            return results;
        }

        // Helper methods

        // from CDC : https://phinvads.cdc.gov/vads/ViewValueSet.action?id=67D34BBC-617F-DD11-B38D-00188B398520
        private string RaceAbbreviationGivenConceptCode(string iConceptCode)
        {
            string result = "MU";

            if(iConceptCode == "1002-5")
            {
                result = "IN";
            }
            else if(iConceptCode == "2028-9")
            {
                result = "AS";
            }
            else if (iConceptCode == "2054-5")
            {
                result = "BA";
            }
            else if (iConceptCode == "2076-8")
            {
                result = "HP";
            }
            else if (iConceptCode == "2131-1")
            {
                result = "MU";
            }
            else if (iConceptCode == "2106-3")
            {
                result = "WH";
            }

            return result;
        }

        // from CDC : https://phinvads.cdc.gov/vads/http:/phinvads.cdc.gov/vads/ViewCodeSystemConcept.action?oid=2.16.840.1.113883.6.238&code=2135-2
        private string EthnicityAbbreviationGivenConceptCode(string iConceptCode)
        {
            string result = "U";

            if (iConceptCode == "2135-2")
            {
                result = "Y";
            }
            else if (iConceptCode == "2186-5")
            {
                result = "N";
            }

            return result;
        }

        private int loadDeathTable(FhirClient iClient, List<Patient> iPatients)
        {
            int result = 0;

            foreach (Patient p in iPatients)
            {
                // check to see if patient is deceased
                if ((FhirBoolean)p.Deceased == new FhirBoolean())
                {
                    ;
                }
            }

            return result;
        }

    }
}
