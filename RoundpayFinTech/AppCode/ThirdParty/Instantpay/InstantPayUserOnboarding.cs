using Fintech.AppCode.DB;
using Fintech.AppCode.HelperClass;
using Fintech.AppCode.StaticModel;
using Fintech.AppCode.WebRequest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RoundpayFinTech.AppCode.DL;
using RoundpayFinTech.AppCode.Interfaces;
using RoundpayFinTech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.MoneyTransfer;
using RoundpayFinTech.AppCode.Model.ProcModel;
using RoundpayFinTech.AppCode.ThirdParty.Instantpay;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace RoundpayFinTech.AppCode.MiddleLayer.Dmt_Api
{
    public partial class InstantPayUserOnboarding
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration Configuration;

        private readonly string _APICode;
        private readonly int _APIID;
        private readonly IDAL _dal;
        private readonly IPaySetting apiSetting;


        public InstantPayUserOnboarding(IHttpContextAccessor accessor, IHostingEnvironment env, string APICode, int APIID, IDAL dal)
        {
            _APICode = APICode;
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
        public IPaySetting AppSetting()
        {
            var setting = new IPaySetting();
            try
            {
                setting = new IPaySetting
                {
                    Token = Configuration["DMR:" + _APICode + ":Token"],
                    BaseURL = Configuration["DMR:" + _APICode + ":BaseURL"],
                    OnboardURL = Configuration["OnboardURL:" + _APICode + ":OnboardURL"]
                };
            }
            catch (Exception ex)
            {
                var _ = new ProcPageErrorLog(_dal).Call(new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "IPaySetting:" + (_APICode ?? string.Empty),
                    Error = ex.Message,
                    LoginTypeID = LoginType.ApplicationUser
                });
            }
            return setting;
        }

        public ResponseStatus InstantPayRegisterOTP(string MobileNO)
        {
            var _resp = new ResponseStatus
            {
                Statuscode = ErrorCodes.Minus1,
                Msg = ErrorCodes.AnError
            };
            var req = new InstantPayUOReq
            {
                token = apiSetting.Token,
                request = new InstantPayUO
                {
                    mobile = MobileNO
                }
            };
            string response = string.Empty;
            var _URL = apiSetting.OnboardURL + "registrationOTP";
            try
            {
                response = AppWebRequest.O.PostJsonDataUsingHWR(_URL, req);
                var objIPOU = new IPUORespXml();
                var _apiRes = XMLHelper.O.DesrializeToObject(objIPOU, response, null);
                if (_apiRes != null)
                {
                    if (_apiRes.statuscode.Equals("TXN"))
                    {
                        _resp.Statuscode = ErrorCodes.One;
                        _resp.Msg = _apiRes.status;
                    }
                    else
                    {
                        _resp.Statuscode = ErrorCodes.Minus1;
                        _resp.Msg = _apiRes.status;
                    }
                }

            }
            catch (Exception ex)
            {
                response = ex.Message + "|" + response;
                var _ = new ProcPageErrorLog(_dal).Call(new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "InstantPayRegisterOTP",
                    Error = ex.Message,
                    LoginTypeID = LoginType.ApplicationUser,
                    UserId = 1
                });
            }
            _resp.CommonStr3 = _URL + "?" + JsonConvert.SerializeObject(req);
            _resp.CommonStr4 = response;
            return _resp;
        }

        public IPRegistrationData InstantPayRegistration(InstantPayUO instantPayUO)
        {
            var _resp = new IPRegistrationData
            {
                Statuscode = ErrorCodes.Minus1,
                Msg = ErrorCodes.AnError
            };
            var req = new InstantPayUOReq
            {
                token = apiSetting.Token,
                request = new InstantPayUO
                {
                    mobile = instantPayUO.mobile,
                    email = instantPayUO.email,
                    company = instantPayUO.company,
                    name = instantPayUO.name,
                    pan = instantPayUO.pan,
                    pincode = instantPayUO.pincode,
                    address = instantPayUO.address,
                    otp = instantPayUO.otp
                }
            };
            string response = string.Empty;
            var _URL = apiSetting.OnboardURL + "registration";
            try
            {
                response = AppWebRequest.O.PostJsonDataUsingHWR(_URL, req);
                var objIPOU = new IPUORespXml();
                var _apiRes = XMLHelper.O.DesrializeToObject(objIPOU, response, null);
                if (_apiRes != null)
                {
                    if (_apiRes.statuscode.Equals("TXN"))
                    {
                        _resp.Statuscode = ErrorCodes.One;
                        _resp.Msg = _apiRes.status;
                        _resp.outlet_id = objIPOU.data.outlet_id;
                        _resp.email_id = objIPOU.data.email_id;
                        _resp.contact_person = objIPOU.data.contact_person;
                        _resp.outlet_name = objIPOU.data.outlet_name;
                        _resp.pan_no = objIPOU.data.pan_no;
                        _resp.outlet_status = objIPOU.data.outlet_status;
                        _resp.kyc_status = objIPOU.data.kyc_status;
                    }
                    else
                    {
                        _resp.Statuscode = ErrorCodes.Minus1;
                        _resp.Msg = _apiRes.status;
                    }
                }

            }
            catch (Exception ex)
            {
                response = ex.Message + "|" + response;
                var _ = new ProcPageErrorLog(_dal).Call(new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "InstantPayRegisterOTP",
                    Error = ex.Message,
                    LoginTypeID = LoginType.ApplicationUser,
                    UserId = 1
                });
            }
            _resp.RequestURL = _URL + "?" + JsonConvert.SerializeObject(req);
            _resp.Response = response;
            return _resp;
        }

        public IPRegistrationData InstantPayGetKYCDoc(InstantPayUO instantPayUO)
        {
            var _resp = new IPRegistrationData
            {
                Statuscode = ErrorCodes.Minus1,
                Msg = ErrorCodes.AnError
            };
            var req = new InstantPayUOReq
            {
                token = apiSetting.Token,
                request = new InstantPayUO
                {
                    outletid = instantPayUO.outletid,
                    pan = instantPayUO.pan
                }
            };
            string response = string.Empty;
            var _URL = apiSetting.OnboardURL + "requiredDocs";
            try
            {
                response = AppWebRequest.O.PostJsonDataUsingHWR(_URL, req);
                var _apiRes = JsonConvert.DeserializeObject<IPGetKYCDocResp>(response);
                if (_apiRes != null)
                {
                    if (_apiRes.statuscode.Equals("TXN"))
                    {
                        _resp.Statuscode = ErrorCodes.One;
                        _resp.Msg = _apiRes.status;
                        
                    }
                    else
                    {
                        _resp.Statuscode = ErrorCodes.Minus1;
                        _resp.Msg = _apiRes.status;
                    }
                }

            }
            catch (Exception ex)
            {
                response = ex.Message + "|" + response;
                var _ = new ProcPageErrorLog(_dal).Call(new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "InstantPayRegisterOTP",
                    Error = ex.Message,
                    LoginTypeID = LoginType.ApplicationUser,
                    UserId = 1
                });
            }
            _resp.RequestURL = _URL + "?" + JsonConvert.SerializeObject(req);
            _resp.Response = response;
            return _resp;
        }

        public IPRegistrationData InstantPayUploadKYCDoc(InstantPayUO instantPayUO)
        {
            var _resp = new IPRegistrationData
            {
                Statuscode = ErrorCodes.Minus1,
                Msg = ErrorCodes.AnError
            };
            var req = new IPOUDocReq
            {
                token = apiSetting.Token,
                request = new IPDocRequest
                {
                    outletid = Convert.ToInt32(instantPayUO.outletid),
                    pan_no = instantPayUO.pan,
                    document = new IPOUDocument { 
                        id = instantPayUO.docId,
                        link = instantPayUO.docLink,
                        filename = instantPayUO.fileName,
                    }
                }
            };
            string response = string.Empty;
            var _URL = apiSetting.OnboardURL + "uploadDocs";
            try
            {
                response = AppWebRequest.O.PostJsonDataUsingHWR(_URL, req);
                var _apiRes = JsonConvert.DeserializeObject<IPUORespJson>(response);
                if (_apiRes != null)
                {
                    if (_apiRes.statuscode.Equals("TXN"))
                    {
                        _resp.Statuscode = ErrorCodes.One;
                        _resp.Msg = _apiRes.status;
                    }
                    else
                    {
                        _resp.Statuscode = ErrorCodes.Minus1;
                        _resp.Msg = _apiRes.status;
                    }
                }

            }
            catch (Exception ex)
            {
                response = ex.Message + "|" + response;
                var _ = new ProcPageErrorLog(_dal).Call(new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "InstantPayRegisterOTP",
                    Error = ex.Message,
                    LoginTypeID = LoginType.ApplicationUser,
                    UserId = 1
                });
            }
            _resp.RequestURL = _URL + "?" + JsonConvert.SerializeObject(req);
            _resp.Response = response;
            return _resp;
        }

        public IPRegistrationData InstantPayMobileChange(string oldMobileNo,string newMobileNo)
        {
            var _resp = new IPRegistrationData
            {
                Statuscode = ErrorCodes.Minus1,
                Msg = ErrorCodes.AnError
            };
            var req = new InstantPayUOReq
            {
                token = apiSetting.Token,
                request = new InstantPayUO
                {
                    old_mobile = oldMobileNo,
                    new_mobile = newMobileNo
                }
            };
            string response = string.Empty;
            var _URL = apiSetting.OnboardURL + "change_mobile";
            try
            {
                response = AppWebRequest.O.PostJsonDataUsingHWR(_URL, req);
                var _apiRes = JsonConvert.DeserializeObject<IPUORespJson>(response);
                if (_apiRes != null)
                {
                    if (_apiRes.statuscode.Equals("TXN"))
                    {
                        _resp.Statuscode = ErrorCodes.One;
                        _resp.Msg = _apiRes.status;
                    }
                    else
                    {
                        _resp.Statuscode = ErrorCodes.Minus1;
                        _resp.Msg = _apiRes.status;
                    }
                }

            }
            catch (Exception ex)
            {
                response = ex.Message + "|" + response;
                var _ = new ProcPageErrorLog(_dal).Call(new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "InstantPayRegisterOTP",
                    Error = ex.Message,
                    LoginTypeID = LoginType.ApplicationUser,
                    UserId = 1
                });
            }
            _resp.RequestURL = _URL + "?" + JsonConvert.SerializeObject(req);
            _resp.Response = response;
            return _resp;
        }

        public IPRegistrationData InstantPayMobileChangeVerify(string oldMobileNo, string newMobileNo, string oldMobileOTP, string newMobileOTP)
        {
            var _resp = new IPRegistrationData
            {
                Statuscode = ErrorCodes.Minus1,
                Msg = ErrorCodes.AnError
            };
            var req = new InstantPayUOReq
            {
                token = apiSetting.Token,
                request = new InstantPayUO
                {
                    old_mobile = oldMobileNo,
                    new_mobile = newMobileNo,
                    old_mobile_otp = oldMobileOTP,
                    new_mobile_otp = newMobileOTP,
                }
            };
            string response = string.Empty;
            var _URL = apiSetting.OnboardURL + "change_mobile_verify";
            try
            {
                response = AppWebRequest.O.PostJsonDataUsingHWR(_URL, req);
                var _apiRes = JsonConvert.DeserializeObject<IPUORespJson>(response);
                if (_apiRes != null)
                {
                    if (_apiRes.statuscode.Equals("TXN"))
                    {
                        _resp.Statuscode = ErrorCodes.One;
                        _resp.Msg = _apiRes.status;
                    }
                    else
                    {
                        _resp.Statuscode = ErrorCodes.Minus1;
                        _resp.Msg = _apiRes.status;
                    }
                }

            }
            catch (Exception ex)
            {
                response = ex.Message + "|" + response;
                var _ = new ProcPageErrorLog(_dal).Call(new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "InstantPayRegisterOTP",
                    Error = ex.Message,
                    LoginTypeID = LoginType.ApplicationUser,
                    UserId = 1
                });
            }
            _resp.RequestURL = _URL + "?" + JsonConvert.SerializeObject(req);
            _resp.Response = response;
            return _resp;
        }

    }
}