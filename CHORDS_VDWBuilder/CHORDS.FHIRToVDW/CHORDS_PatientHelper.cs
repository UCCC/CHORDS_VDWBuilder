using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;

namespace CHORDS_VDWBuilder.CHORDS.FHIRToVDW
{
    class CHORDS_PatientHelper
    {
        bool Validate(Patient iPatient)
        {
            bool result = true;

            // check ID
            if (iPatient.Id.Length == 0) result = false;

            // check addresses

            // check gender

            // check race

            // 

            return result;
        }
    }
}
