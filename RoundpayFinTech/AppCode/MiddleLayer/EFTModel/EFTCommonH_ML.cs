using RoundpayFinTech.AppCode.Configuration;
using RoundpayFinTech.AppCode.Interfaces;
using RoundpayFinTech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.ROffer;
using RoundpayFinTech.Models.EFTModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoundpayFinTech.AppCode.MiddleLayer.EFTModel
{

    public class EFTCommonH_ML : IEFTCommonH
    {
        public readonly AppDBContext _appDBContext;
        public EFTCommonH_ML(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public string GetRefferalContent(Int32 id)
        {
            WebsiteModel model = new WebsiteModel();
            var res = _appDBContext.MASTER_WEBSITE.Where(x => x._UserId == id).FirstOrDefault();
            return model.RefferalContent = res._ReferralContent;


        }


        public List<RNPRechPlansPanel> RNPRechargePlans_EF(int oid, int circleId)
        {
            var rJData = new List<RNPRechPlansPanel>();
            try
            {
                rJData = _appDBContext.tbl_RechargePlans
                        .Join(
                            _appDBContext.MASTER_RECH_PLAN_TYPE,
                            rechPlans => rechPlans._RechPlanTypeID,
                            rechType => rechType._ID,
                            (rechPlans, rechType) => new RNPRechPlansPanel
                            {
                                OID =rechPlans._OID,
                                CircleID = rechPlans._CircleID,
                                RechargePlanType = rechType._RechargePlanType,
                                RAmount = rechPlans._RAmount.ToString(),
                                Details = rechPlans._Details,
                                Validity = rechPlans._Validity,
                                EntryDate = rechPlans._EntryDate.ToString("dd MMM yyyy")
                            }
                        ).Where(x => x.OID == oid && x.CircleID == circleId).ToList();
                
                return rJData;
            }
            catch (Exception ex)
            {
                return rJData;

            }
        }

        public ApplicationSetting_EFT GetApplicationSettings()
        {
            var resp = new ApplicationSetting_EFT();
            try
            {
                return _appDBContext.ApplicationSetting.FirstOrDefault<ApplicationSetting_EFT>();
            }
            catch (Exception ex)
            {
                return resp;
            }
        }

    }
}
