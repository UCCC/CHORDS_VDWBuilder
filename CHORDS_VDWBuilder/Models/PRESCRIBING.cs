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
    
    public partial class PRESCRIBING
    {
        public int PRESCRIBING_ID { get; set; }
        public string PERSON_ID { get; set; }
        public string ENC_ID { get; set; }
        public string RXMD { get; set; }
        public System.DateTime RX_ORDER_DATE { get; set; }
        public Nullable<System.TimeSpan> RX_ORDER_TIME { get; set; }
        public System.DateTime RX_START_DATE { get; set; }
        public Nullable<System.DateTime> RX_END_DATE { get; set; }
        public string RX_QUANTITY { get; set; }
        public Nullable<decimal> RX_QUANTITY_NUM { get; set; }
        public string RX_QUANTITY_UNIT { get; set; }
        public Nullable<decimal> RX_REFILLS { get; set; }
        public decimal RX_DAYS_SUPPLY { get; set; }
        public string RX_FREQUENCY { get; set; }
        public string RX_INSTRUCTIONS { get; set; }
        public string RX_BASIS { get; set; }
        public string RXNORM { get; set; }
        public string GENERIC_MED_NAME { get; set; }
    }
}
