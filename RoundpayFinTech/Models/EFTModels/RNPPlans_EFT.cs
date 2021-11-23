using System;
using System.ComponentModel.DataAnnotations;

namespace RoundpayFinTech.Models.EFTModels
{
    public class MasterRechPlanType
    {
        [Key]
        public int _ID { get; set; }
        public string _RechargePlanType { get; set; }
    }

    public class RechargePlans
    {
        [Key]
        public int _ID { get; set; }
        public int _OID { get; set; }
        public int _CircleID { get; set; }
        public int _RechPlanTypeID { get; set; }
        public decimal _RAmount { get; set; }
        public string _Validity { get; set; }
        public string _Details { get; set; }
        public DateTime _EntryDate { get; set; }
        public DateTime _ModifyDate { get; set; }
        public int _APIID { get; set; }
    }
}
