using RoundpayFinTech.AppCode.Model.ProcModel;
using RoundpayFinTech.AppCode.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundpayFinTech.AppCode.Model.Exchange
{

    public class CommonCommission
    {
        public int DenominationID { get; set; }
        public int EntryBy { get; set; }

        public int EntryDate { get; set; }

        public int ModifyBy { get; set; }

        public int ModifyDate { get; set; }

        public Boolean IsActive { get; set; }
        public int Id { get; set; }
        public int OID { get; set; }
        public int CircleID { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Comm { get; set; }
        public Boolean WithGST { get; set; }
        public Boolean AmtType { get; set; }
        public IEnumerable<OperatorDetail> OpDetail { get; set; }
        public IEnumerable<DenominationVoucher> denominationsoucher { get; set; }
        public IEnumerable<CirlceMaster> CircleList { get; set; }
        public string Denomination { get; set; }
        public string OpretorName{ get; set; }
        public string CircleName { get; set; }

    }
    public class BuyerCommission: CommonCommission
    {
         
        public int Ind { get; set; }
        public int BuyerId { get; set; }
        public Boolean CommType { get; set; }
    }

public class SellerCommission: CommonCommission
    {
        public int SellerId { get; set; }
        public int APIId { get; set; }
        public int CommType{ get; set; }
      
        public int Limit { get; set; }
        public int LimitType{ get; set; }




}

    public class Commission_Denomination
    {
                              
        public int ID            { get; set; }
        public int CommissionID  { get; set; }
        public string Type          { get; set; }
        public int DenominationID{ get; set; }
        public string DenominationName{ get; set; }
        public int CircleID { get; set; }
        public int OID { get; set; }
        public decimal Amount { get; set; }

    }
}
