using Fintech.AppCode.Interfaces;
using RoundpayFinTech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.ProcModel;
using RoundpayFinTech.AppCode.ThirdParty.Roundpay;
using System.Threading.Tasks;

namespace RoundpayFinTech.AppCode.Interfaces
{
    public interface IOnboardingML
    {
        IResponseStatus CallOnboarding(OutletServicePlusReqModel MDL);
        OnboardingStatusCheckModel CallOnboardingStatusCheck(OutletServicePlusReqModel MDL, bool IsAPIOutletIDExists);
        ServicePlusStatusModel CallBBPSServicePlus(OutletServicePlusReqModel MDL);
        ServicePlusStatusModel CallAEPSServicePlus(OutletServicePlusReqModel MDL);
        BCResponse CallBCService(OutletServicePlusReqModel MDL);
        ServicePlusStatusModel CallPSAServicePlus(OutletServicePlusReqModel MDL);
        ServicePlusStatusModel CallDMTServicePlus(OutletServicePlusReqModel MDL);
        ServicePlusStatusModel CallRailServicePlus(OutletServicePlusReqModel MDL);
        IResponseStatus UpdateUIDTokenAgent(OutletEKYCRequest outletEKYCRequest);
        ResponseStatus CallManualPSA(ValidateAPIOutletResp MDL);
        bool ISKYCRequired(ValidateAPIOutletResp MDL);
    }
    public interface IPSATransaction {
        Task CallPSATransaction(TransactionServiceResp TSResp, TransactionResponse resp);
    }
    public interface IAPISetting {
        FingpayAPISetting GetFingpay();
    }
}
