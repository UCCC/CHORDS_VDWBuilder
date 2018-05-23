using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHORDS_VDWBuilder.Models
{
    public class FHIRPatientSummary
    {
        public string PERSON_ID { get; set; }
        public int? LocationCount { get; set; }
        public int? EncounterCount { get; set; }
        public int? DiagnosesCount { get; set; }
        public int? VitalSignCount { get; set; }
        public int LocationTotalCount { get; set; }
        public int EncounterTotalCount { get; set; }
        public int DiagnosesTotalCount { get; set; }
        public int VitalSignTotalCount { get; set; }
    }
}
