using Fintech.AppCode.DB;
using Fintech.AppCode.StaticModel;
using Fintech.AppCode.WebRequest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RoundpayFinTech.AppCode.DL;
using RoundpayFinTech.AppCode.Interfaces;
using RoundpayFinTech.AppCode.Model.MoneyTransfer;
using RoundpayFinTech.AppCode.Model.ProcModel;
using RoundpayFinTech.AppCode.ThirdParty.Hypto;
using RoundpayFinTech.AppCode.ThirdParty.OpenBank;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoundpayFinTech.AppCode.MiddleLayer.Dmt_Api
{
    public class HyptoML : IMoneyTransferAPIML
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration Configuration;
        private readonly int _APIID;
        private readonly IDAL _dal;
        private readonly HyptoAppSetting apiSetting;
        public HyptoML(IHttpContextAccessor accessor, IHostingEnvironment env, int APIID, IDAL dal)
        {
            _APIID = APIID;
            _accessor = accessor;
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile((_env.IsProduction() ? "appsettings.json" : "appsettings.Development.json"));
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            _dal = dal;
            apiSetting = AppSetting();
        }
        public HyptoAppSetting AppSetting()
        {
            var setting = new HyptoAppSetting();
            try
            {
                setting = new HyptoAppSetting
                {
                    BaseURL = Configuration["DMR:HYPTO:BaseURL"],
                    Auth = Configuration["DMR:HYPTO:Auth"]
                };
            }
            catch (Exception ex)
            {
                var _ = new ProcPageErrorLog(_dal).Call(new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "HyptoAppSetting",
                    Error = ex.Message,
                    LoginTypeID = LoginType.ApplicationUser
                });
            }
            return setting;
        }

        public MSenderCreateResp CreateSender(MTAPIRequest request)
        {
            throw new NotImplementedException();
        }
        public MSenderLoginResponse GetSender(MTAPIRequest request)
        {
            throw new NotImplementedException();
        }
        public MSenderCreateResp SenderEKYC(MTAPIRequest request)
        {
            throw new NotImplementedException();
        }
        public MSenderCreateResp SenderKYC(MTAPIRequest request)
        {
            throw new NotImplementedException();
        }
        public MSenderCreateResp SenderResendOTP(MTAPIRequest request)
        {

            throw new NotImplementedException();
        }
        public MSenderLoginResponse VerifySender(MTAPIRequest request)
        {
            throw new NotImplementedException();
        }
        public MSenderLoginResponse CreateBeneficiary(MTAPIRequest request)
        {

            throw new NotImplementedException();
        }
        public MBeneficiaryResp GetBeneficiary(MTAPIRequest request)
        {
            throw new NotImplementedException();
        }
        public MSenderCreateResp GenerateOTP(MTAPIRequest request)
        {
            throw new NotImplementedException();
        }
        public MSenderLoginResponse ValidateBeneficiary(MTAPIRequest request)
        {
            throw new NotImplementedException();
        }
        public MSenderLoginResponse RemoveBeneficiary(MTAPIRequest request)
        {
            throw new NotImplementedException();
        }
        public MSenderLoginResponse RemoveBeneficiaryValidate(MTAPIRequest request)
        {
            throw new NotImplementedException();
        }
        public DMRTransactionResponse VerifyAccount(MTAPIRequest request)
        {
            var res = new DMRTransactionResponse
            {
                Statuscode = RechargeRespType.PENDING,
                Msg = RechargeRespType._PENDING,
                VendorID = "",
                LiveID = ""
            };

            string response = string.Empty;
            var param = new
            {
                number = request.mBeneDetail.AccountNo,
                ifsc = request.mBeneDetail.IFSC,
                reference_number = request.TransactionID
            };
            var _URL = apiSetting.BaseURL;
            try
            {
                response = AppWebRequest.O.PostJsonDataUsingHWRTLS(_URL, param, new Dictionary<string, string>
                {
                    { "Authorization",apiSetting.Auth},
                    { ContentType.Self,ContentType.application_json}
                }).Result;
                if (!string.IsNullOrEmpty(response))
                {
                    var _apiRes = JsonConvert.DeserializeObject<OBVerifiyResponse>(response);
                    if (_apiRes != null)
                    {
                        if (_apiRes.success)
                        {
                            if (_apiRes.data != null)
                            {
                                res.VendorID = _apiRes.data.reference_number;
                                if (_apiRes.data.status.Equals("COMPLETED"))
                                {
                                    res.Statuscode = RechargeRespType.SUCCESS;
                                    res.Msg = _apiRes.message;
                                    res.BeneName = _apiRes.data.verify_account_holder ?? string.Empty;
                                    res.LiveID = _apiRes.data.bank_ref_num.ToString();
                                    res.ErrorCode = ErrorCodes.Transaction_Successful;
                                }
                                else if (_apiRes.data.status.Equals("FAILED"))
                                {
                                    res.Statuscode = RechargeRespType.FAILED;
                                    res.Msg = _apiRes.data.verify_reason ?? string.Empty;
                                    if (res.Msg.Contains("suff"))
                                    {
                                        res.ErrorCode = DMTErrorCodes.Declined_by_ServiceProvider;
                                        res.Msg = nameof(DMTErrorCodes.Declined_by_ServiceProvider).Replace("_", " ");
                                    }
                                    else
                                    {
                                        res.Msg = string.Format("{0}", res.Msg);
                                        res.ErrorCode = 158;
                                    }
                                    res.LiveID = res.Msg;
                                }
                                else
                                {
                                    res.Statuscode = RechargeRespType.PENDING;
                                    res.Msg = nameof(ErrorCodes.Request_Accpeted).Replace("_", " ");
                                    res.ErrorCode = ErrorCodes.Request_Accpeted;
                                    res.LiveID = res.Msg;
                                }
                            }
                            else
                            {
                                res.Statuscode = RechargeRespType.PENDING;
                                res.Msg = _apiRes.message;
                            }
                        }
                        else
                        {
                            IErrorCodeML errorCodeML = new ErrorCodeML(_accessor, _env, false);
                            var eFromDB = errorCodeML.GetAPIErrorCodeDescription(request.APIGroupCode, _apiRes.message);
                            if (!string.IsNullOrEmpty(eFromDB.Code))
                            {
                                res.Statuscode = eFromDB.Status;
                                res.Msg = eFromDB.Error.Replace("{REPLACE}", _apiRes.message);
                                res.ErrorCode = Convert.ToInt32(eFromDB.Code.Trim());
                                res.LiveID = res.Msg;
                            }
                            res.Statuscode = res.Statuscode == 0 ? RechargeRespType.PENDING : res.Statuscode;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = ex.Message + "|" + response;
                var _ = new ProcPageErrorLog(_dal).Call(new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "VerifyAccount",
                    Error = ex.Message,
                    LoginTypeID = LoginType.ApplicationUser,
                    UserId = request.UserID
                });
            }
            res.Request = _URL+ "&RequestJson=" + JsonConvert.SerializeObject(param);
            res.Response = response;
            new ProcUpdateLogDMRReqResp(_dal).Call(new DMTReqRes
            {
                APIID = request.APIID,
                Method = "VerifyAccount",
                RequestModeID = request.RequestMode,
                Request = res.Request,
                Response = res.Response,
                SenderNo = request.SenderMobile,
                UserID = request.UserID,
                TID = request.TID.ToString()
            });
            return res;
        }
        public DMRTransactionResponse AccountTransfer(MTAPIRequest request)
        {
            throw new NotImplementedException();
        }
        public DMRTransactionResponse GetTransactionStatus(int TID, string TransactionID, int RequestMode, int UserID, int APIID, string VendorID)
        {
            throw new NotImplementedException();
        }
    }
}
