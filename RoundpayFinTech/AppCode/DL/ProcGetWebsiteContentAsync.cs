using Fintech.AppCode.DB;
using Fintech.AppCode.Interfaces;
using RoundpayFinTech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.ProcModel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RoundpayFinTech.AppCode.DL
{
    public class ProcGetWebsiteContentAsync : IProcedureAsync
    {
        private readonly IDAL _dal;
        public ProcGetWebsiteContentAsync(IDAL dal) => _dal = dal;
        public async Task<object> Call(object obj)
        {
            var req = (int)obj;
            SqlParameter[] param = {
                new SqlParameter("@WID", req)
            };
            SiteTemplateSection res = new SiteTemplateSection();
            try
            {
                DataTable dt = await _dal.GetAsync(GetName(), param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    res = new SiteTemplateSection
                    {
                        AboutUs = Convert.ToString(dt.Rows[0]["_AboutUs"]),
                        ContactUs = Convert.ToString(dt.Rows[0]["_ContactUs"]),
                        BankDetail = Convert.ToString(dt.Rows[0]["_BankDetail"]),
                        FacebookLink = Convert.ToString(dt.Rows[0]["_FacebookLink"]),
                        TwitterLink = Convert.ToString(dt.Rows[0]["_TwiterLink"]),
                        WhatsappLink = Convert.ToString(dt.Rows[0]["_whatsappLink"]),
                        Map = Convert.ToString(dt.Rows[0]["_Map"])
                    };
                }
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "Call",
                    Error = ex.Message
                };
                var _ = new ProcPageErrorLog(_dal).Call(errorLog);
            }
            return res ?? new SiteTemplateSection();
        }
        public Task<object> Call() => throw new NotImplementedException();
        public string GetName() => "select * from tbl_Website_Content(nolock) where _WID=@WID";
    }
}
