using RoundpayFinTech.AppCode.Model.ROffer;
using RoundpayFinTech.Models.EFTModels;
using System.Collections.Generic;

namespace RoundpayFinTech.AppCode.Interfaces
{
    public interface IEFTCommonH
    {
        List<RNPRechPlansPanel> RNPRechargePlans_EF(int oid, int circleId);
        ApplicationSetting_EFT GetApplicationSettings();
        string GetRefferalContent(int id);
    }
}
