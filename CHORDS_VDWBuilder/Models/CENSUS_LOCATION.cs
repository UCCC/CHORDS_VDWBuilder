//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CHORDS_VDWBuilder.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CENSUS_LOCATION
    {
        public string PERSON_ID { get; set; }
        public System.DateTime LOC_START { get; set; }
        public Nullable<System.DateTime> LOC_END { get; set; }
        public string GEOCODE { get; set; }
        public string CITY_GEOCODE { get; set; }
        public Nullable<decimal> GEOCODE_BOUNDARY_YEAR { get; set; }
        public string GEOLEVEL { get; set; }
        public string MATCH_STRENGTH { get; set; }
        public Nullable<decimal> LATITUDE { get; set; }
        public Nullable<decimal> LONGITUDE { get; set; }
        public string GEOCODE_APP { get; set; }
    }
}
