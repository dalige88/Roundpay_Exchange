using Fintech.AppCode.DB;
using Fintech.AppCode.Interfaces;
using Fintech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.ProcModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RoundpayFinTech.AppCode.DL
{
    public class ProcGetBank : IProcedure
    {
        private readonly IDAL _dal;
        public ProcGetBank(IDAL dal)
        {
            _dal = dal;
        }
        public object Call(object obj)
        {
            var _req = (CommonReq)obj;
            SqlParameter[] param = {
                new SqlParameter("@LoginID", _req.LoginID),
                new SqlParameter("@LT", _req.LoginTypeID),
                new SqlParameter("@ID", _req.CommonInt)
            };
            var bankMasters = new List<BankMaster>();
            try
            {
                DataTable dt = _dal.GetByProcedure(GetName(), param);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var bankM = new BankMaster
                    {
                        ID = Convert.ToInt32(dt.Rows[i]["_ID"]),
                        BankName = dt.Rows[i]["_Bank"].ToString(),
                        AccountLimit = Convert.ToInt32(dt.Rows[i]["_ACNo_Limit"] is DBNull ? 0 : dt.Rows[i]["_ACNo_Limit"]),
                        Code = dt.Rows[i]["_Code"] is DBNull ? "" : dt.Rows[i]["_Code"].ToString(),
                        IsIMPS = dt.Rows[i]["_IsIMPS"] is DBNull ? false : Convert.ToBoolean(dt.Rows[i]["_IsIMPS"]),
                        IsNEFT = dt.Rows[i]["_IsNEFT"] is DBNull ?false : Convert.ToBoolean(dt.Rows[i]["_IsNEFT"]),
                        IsACVerification = Convert.ToBoolean(dt.Rows[i]["_IsACVerification"] is DBNull ? false : dt.Rows[i]["_IsACVerification"]),
                        IFSC = dt.Rows[i]["_IFSC"] is DBNull ? "" : dt.Rows[i]["_IFSC"].ToString(),
                        Logo = dt.Rows[i]["_Logo"] is DBNull ? "" : dt.Rows[i]["_Logo"].ToString(),
                    };
                    bankMasters.Add(bankM);
                }
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "Call",
                    Error = ex.Message,
                    LoginTypeID = _req.LoginTypeID,
                    UserId = _req.LoginID
                };
                var _ = new ProcPageErrorLog(_dal).Call(errorLog);
            }
            return bankMasters;
        }

        public object Call()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return "proc_GetBank";
        }
    }
}
