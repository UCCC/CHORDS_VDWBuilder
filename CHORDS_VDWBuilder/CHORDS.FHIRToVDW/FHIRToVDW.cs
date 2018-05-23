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

namespace CHORDS_VDWBuilder.CHORDS.FHIRToVDW
{
    class FHIRToVDW
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool ClearVDW()
        {
            bool result = false;

            using (var context = new VDW_3_1_Entities())
            {
                context.CENSUS_LOCATION.RemoveRange(context.CENSUS_LOCATION);
                context.DIAGNOSES.RemoveRange(context.DIAGNOSES);
                context.ENCOUNTERS.RemoveRange(context.ENCOUNTERS);
                context.VITAL_SIGNS.RemoveRange(context.VITAL_SIGNS);
                context.DEMOGRAPHICS.RemoveRange(context.DEMOGRAPHICS);
            }

            return result;
        }

        public List<FHIRPatientSummary> ScanFHIRDB(FhirClient iFHIRClient)
        {
            List<FHIRPatientSummary> results = new List<FHIRPatientSummary>();

            log.Info("Start Scanning Patients");

            Bundle bundles = iFHIRClient.Search<Patient>();
            List<Patient> patients = new List<Patient>();
            foreach (Bundle.EntryComponent item in bundles.Entry)
            {
                Patient p = (Patient)item.Resource;
                patients.Add(p);
            }

            foreach (var p in patients)
            {
                FHIRPatientSummary sum = new FHIRPatientSummary();
                sum.PERSON_ID = p.Id;

                // Location Count
                sum.LocationTotalCount = p.Address.Count;

                // Encounter Count
                Bundle en = iFHIRClient.Search<Encounter>(new string[] { "patient=" + p.Id });
                sum.EncounterTotalCount = en.Entry.Count;

                // Diagnoses Count
                Bundle d = iFHIRClient.Search<DiagnosticReport>(new string[] { "patient=" + p.Id });
                sum.DiagnosesTotalCount = d.Entry.Count;

                // Vital Signs
                ;

                results.Add(sum);
            }

            log.Info("End Scanning Patients");

            return results;
        }

        public int LoadVDW(FhirClient iFHIRClient)
        {
            int result = 0;

            log.Info("Loading Patients");

            List<Patient> patients = loadPatients(iFHIRClient);

            result = patients.Count;

            return result;
        }

        public int PatientCount(FhirClient iFHIRClient)
        {
            int result = 0;

            Bundle response = iFHIRClient.Search<Patient>();

            result = response.Entry.Count();

            return result;
        }

        public int DiagnosesCount(FhirClient iFHIRClient)
        {
            int result = 0;

            Bundle response = iFHIRClient.Search<Patient>();

            foreach (Bundle.EntryComponent item in response.Entry)
            {
                Patient p = (Patient)item.Resource;

                Bundle d = iFHIRClient.Search<DiagnosticReport>(new string[] { "patient=" + p.Id });

                result += d.Entry.Count();
            }

            return result;
        }

        public int EncounterCount(FhirClient iFHIRClient)
        {
            int result = 0;

            Bundle response = iFHIRClient.Search<Patient>();

            foreach (Bundle.EntryComponent item in response.Entry)
            {
                Patient p = (Patient)item.Resource;

                Bundle d = iFHIRClient.Search<Encounter>(new string[] { "patient=" + p.Id });

                result += d.Entry.Count();
            }

            return result;
        }

        public int VitalSignCount(FhirClient iFHIRClient)
        {
            int result = 0;

            return result;
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
                            ENCOUNTERS tENCOUNTERS = buildEncounter(iClient, (Encounter) e_item.Resource, p);

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

                        // build and save VITAL_SIGN records for patient
                        ;

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

        private ENCOUNTERS buildEncounter(FhirClient iClient, Encounter iEncounter, Patient iPatient)
        {
            ENCOUNTERS result = new ENCOUNTERS();

            // PERSON_ID
            result.PERSON_ID = iPatient.Id;

            // ADATE
            result.ADATE = iEncounter.Period.StartElement.ToDateTime().Value;

            // ENC_ID
            result.ENC_ID = iEncounter.Id;

            // DDATE
            result.ADATE = iEncounter.Period.EndElement.ToDateTime().Value;

            // PROVIDER
            string id = "";
            Bundle provider = iClient.Search<Organization>(new string[] { "Identifier=" + id });
            //result.PROVIDER = provider.;

            // ENC_COUNT
            if(iEncounter.PartOf == null)
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
            foreach(var l in iEncounter.Location)
            {
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

                // valid test data - remove
                address = "382 Kingbird Cir, Highlands Ranch, CO 80129";
                street = "382 Kingbird Cir";
                city = "Highlands Ranch";
                state = "CO";
                postal_code = "80129";

                // use US Gov Census REST API to get location
                Uri geocoder = new Uri("https://geocoding.geo.census.gov");
                var client = new RestClient();
                client.BaseUrl = geocoder;

                var request = new RestRequest();
                request.Resource = "/geocoder/locations/onelineaddress?address=" + address + "&benchmark=9&format=json";

                IRestResponse response = client.Execute(request);
                var r_json = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content);

                // use US Gov Census API to get Census Tract
                var geographies_request = new RestRequest();
                geographies_request.Resource = "/geocoder/geographies/address?street=" + street + "&city=" + city + "&state=" + state + "&benchmark=Public_AR_Census2010&vintage=Census2010_Census2010&layer=14" + "&format=json";

                IRestResponse geographies_response = client.Execute(geographies_request);
                var g_json = Newtonsoft.Json.JsonConvert.DeserializeObject(geographies_response.Content);

                // fill in CENSUS_LOCATION
                new_location.PERSON_ID = iPatient.Id;
                if (add.Period.StartElement.ToDateTime().HasValue)
                {
                    new_location.LOC_START = add.Period.StartElement.ToDateTime().Value;
                }
                if (add.Period.EndElement.ToDateTime().HasValue)
                {
                    new_location.LOC_END = add.Period.EndElement.ToDateTime().Value;
                }
                else
                {
                    new_location.LOC_END = null;
                }


                // GEOCODE
                ;

                // CITY_GEOCODE - using lat/log determine the city
                ;

                // GEOCODE_BOUNDARY_YEAR
                new_location.GEOCODE_BOUNDARY_YEAR = 2010;

                // GEOLEVEL - should be based on the size of the GEOCODE
                new_location.GEOLEVEL = "T";

                // MATCH_STRENGTH
                new_location.MATCH_STRENGTH = null;

                // LATITUDE
                ;

                // LONGITUDE
                ;

                // GEOCODE_APP
                new_location.GEOCODE_APP = "US Census API";

                try
                {
                    result.Add(new_location);
                }
                catch(Exception ex)
                {
                    log.Info(ex.Message);
                }
            }

            return result;
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
