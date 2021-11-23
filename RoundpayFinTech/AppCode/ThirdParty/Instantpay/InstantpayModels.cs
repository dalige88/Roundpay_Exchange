using System.Collections.Generic;
using System.Xml.Serialization;

namespace RoundpayFinTech.AppCode.ThirdParty.Instantpay
{
    public class InstantPayAppSetting
    {
        public string BaseURL { get; set; }
        public string Token { get; set; }
    }
    public class IPayPayoutRequest
    {
        public string token { get; set; }
        public IPayPayoutRequestHelper request { get; set; }
    }
    public class IPayPayoutRequestHelper
    {
        public string sp_key { get; set; }
        public string external_ref { get; set; }
        public string credit_account { get; set; }
        public string credit_rmn { get; set; }
        public string ifs_code { get; set; }
        public string bene_name { get; set; }
        public string credit_amount { get; set; }
        public string upi_mode { get; set; }
        public string vpa { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string endpoint_ip { get; set; }
        public string alert_mobile { get; set; }
        public string alert_email { get; set; }
        public string remarks { get; set; }
        public string otp_auth { get; set; }
        public string otp { get; set; }
    }
    public class IPayCommonResponse
    {
        public string statuscode { get; set; }
        public string status { get; set; }
    }
    public class IPayPayoutResponse : IPayCommonResponse
    {
        public string timestamp { get; set; }
        public string ipay_uuid { get; set; }
        public string orderid { get; set; }
        public string environment { get; set; }
        public IPayPayoutRespData data { get; set; }
    }
    public class IPayPayoutRespData
    {
        public string ipay_id { get; set; }
        public string transfer_value { get; set; }
        public string type_pricing { get; set; }
        public string commercial_value { get; set; }
        public string value_tds { get; set; }
        public string ccf { get; set; }
        public string vendor_ccf { get; set; }
        public string charged_amt { get; set; }
        public IPayPayout payout { get; set; }
    }
    public class IPayPayout
    {
        public string credit_refid { get; set; }
        public string account { get; set; }
        public string ifsc { get; set; }
        public string name { get; set; }
    }
    public class IPayStatusCheckExternalResponse : IPayCommonResponse
    {
        public IPayStatusCheckExternalData data { get; set; }
    }
    public class IPayStatusCheckExternalData
    {
        public string transaction_dt { get; set; }
        public string external_id { get; set; }
        public string order_id { get; set; }
        public string serviceprovider_id { get; set; }
        public string product_key { get; set; }
        public string transaction_account { get; set; }
        public string transaction_amount { get; set; }
        public string transaction_status { get; set; }
        public string transaction_description { get; set; }
        public IPayAdditionalDetail additional_details { get; set; }
    }
    public class IPayAdditionalDetail
    {
        public string beneficiary_name { get; set; }
        public string account { get; set; }
        public string ifsc { get; set; }
    }

    public class IPaySetting
    {
        public string Token { get; set; }
        public string BaseURL { get; set; }
        public string OnboardURL { get; set; }
    }

    public class IPayDMRReq : IPayCommonResponse
    {
        public string token { get; set; }

        public IPayRequest request { get; set; }
    }

    public class IPayRequest
    {
        public string mobile { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string pincode { get; set; }
        public int outletid { get; set; }
        public string remitterid { get; set; }
        public string otp { get; set; }
        public string ifsc { get; set; }
        public string account { get; set; }
        public string beneficiaryid { get; set; }
        public string remittermobile { get; set; }
        public string agentid { get; set; }
        public string amount { get; set; }
        public string mode { get; set; }
    }


    public class IPayDMRResp : IPayCommonResponse
    {
        public IPayResponse data { get; set; }
    }

    public class IPayResponse
    {
        public int otp { get; set; }
        public string ipay_id { get; set; }
        public string ref_no { get; set; }
        public string opr_id { get; set; }
        public string name { get; set; }
        public string opening_bal { get; set; }
        public int amount { get; set; }
        public string charged_amt { get; set; }
        public int locked_amt { get; set; }
        public int ccf_bank { get; set; }
        public string bank_alias { get; set; }
        public Remitter remitter { get; set; }
        public List<Beneficiary> beneficiary { get; set; }
        public List<RemitterLimit> remitter_limit { get; set; }
    }

    public class Remitter
    {
        public string id { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string pincode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string kycstatus { get; set; }
        public int consumedlimit { get; set; }
        public int remaininglimit { get; set; }
        public string kycdocs { get; set; }
        public int is_verified { get; set; }
        public int perm_txn_limit { get; set; }
    }

    public class Beneficiary
    {
        public string id { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string account { get; set; }
        public string bank { get; set; }
        public string status { get; set; }
        public string last_success_date { get; set; }
        public string last_success_name { get; set; }
        public string last_success_imps { get; set; }
        public string ifsc { get; set; }
        public string imps { get; set; }
    }

    public class RemitterLimit
    {
        public string code { get; set; }
        public string status { get; set; }
        public Mode mode { get; set; }
        public Limit limit { get; set; }
    }
    public class Mode
    {
        public string imps { get; set; }
        public string neft { get; set; }
    }

    public class Limit
    {
        public string total { get; set; }
        public string consumed { get; set; }
        public string remaining { get; set; }
    }

    public class IPayCBResp : IPayCommonResponse
    {
        public CBResponse data { get; set; }
    }

    public class CBResponse
    {
        public Remitter remitter { get; set; }
        public Beneficiary beneficiary { get; set; }
    }




    public class InstantPayUO
    {
        public string mobile { get; set; }
        public string outletid { get; set; }
        public string email { get; set; }
        public string company { get; set; }
        public string name { get; set; }
        public string pan { get; set; }
        public string pincode { get; set; }
        public string address { get; set; }
        public string otp { get; set; }
        public string old_mobile { get; set; }
        public string new_mobile { get; set; }
        public string old_mobile_otp { get; set; }
        public string new_mobile_otp { get; set; }

        public string docId { get; set; }
        public string docLink { get; set; }
        public string fileName { get; set; }
    }

    public class InstantPayUOReq
    {
        public string token { get; set; }
        public InstantPayUO request { get; set; }
    }

    public class InstantPayUOResp
    {
        public string statuscode { get; set; }
        public string status { get; set; }
    }


    public class IPDataxml
    {
        public double mobile { get; set; }
        public int outlet_id { get; set; }
        public string mobile_number { get; set; }
        public string email_id { get; set; }
        public string outlet_name { get; set; }
        public string contact_person { get; set; }
        public string pan_no { get; set; }
        public int kyc_status { get; set; }
        public int outlet_status { get; set; }
    }

    public class IPUORespXml
    {
        public string statuscode { get; set; }
        public string status { get; set; }
        public IPDataxml data { get; set; }
    }
    public class IPRegistrationData
    {
        public int Statuscode { get; set; }
        public string Msg { get; set; }
        public int outlet_id { get; set; }
        public string mobile_number { get; set; }
        public string email_id { get; set; }
        public string outlet_name { get; set; }
        public string contact_person { get; set; }
        public string pan_no { get; set; }
        public int kyc_status { get; set; }
        public int outlet_status { get; set; }
        public string RequestURL { get; set; }
        public string Response { get; set; }
    }


    public class IPGetKYCDocData
    {
        public List<List<string>> APPROVED { get; set; }
        public List<object> SCREENING { get; set; }
        public List<List<string>> REQUIRED { get; set; }
    }

    public class IPGetKYCDocResp
    {
        public string statuscode { get; set; }
        public string status { get; set; }
        public IPGetKYCDocData data { get; set; }
    }


    public class IPUORespJson
    {
        public string statuscode { get; set; }
        public string status { get; set; }
        public string data { get; set; }
        public string timestamp { get; set; }
        public string ipay_uuid { get; set; }
        public string orderid { get; set; }
        public string environment { get; set; }
    }


    public class IPOUDocument
    {
        public string id { get; set; }
        public string link { get; set; }
        public string filename { get; set; }
    }

    public class IPDocRequest
    {
        public int outletid { get; set; }
        public string pan_no { get; set; }
        public IPOUDocument document { get; set; }
    }

    public class IPOUDocReq
    {
        public string token { get; set; }
        public IPDocRequest request { get; set; }
    }
}
