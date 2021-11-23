using Fintech.AppCode.DB;
using Fintech.AppCode.Interfaces;
using Fintech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.Exchange;
using RoundpayFinTech.AppCode.Model.ProcModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RoundpayFinTech.AppCode.DL
{
    public class ProcSettlementReport : IProcedure
    {
        private readonly IDAL _dal;
        public ProcSettlementReport(IDAL dal) => _dal = dal;

        public object Call(object obj)
        {
            var req = (CommonReq)obj;
            var res = new List<Settlement>();
            SqlParameter[] param = {
                new SqlParameter("@LoginId",req.UserID)
            };
            var dt = _dal.Get(GetName(), param);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    res.Add(new Settlement
                    {
                        TransactionDate = item["_TransactionDate"] is DBNull ? string.Empty : item["_TransactionDate"].ToString(),
                        OpeningBalance = item["_LastBalance"] is DBNull ? 0 : Convert.ToDecimal(item["_LastBalance"]),
                        Amount = item["_Amount"] is DBNull ? 0 : Convert.ToDecimal(item["_Amount"]),
                        ClosingBalance = item["_Balance"] is DBNull ? 0 : Convert.ToDecimal(item["_Balance"]),
                        BankName = item["_BankName"] is DBNull ? string.Empty : Convert.ToString(item["_BankName"]),
                        AccountNumber = item["_Account"] is DBNull ? string.Empty : Convert.ToString(item["_Account"]),
                        Type = item["_Type"] is DBNull ? string.Empty : Convert.ToString(item["_Type"]),
                        LiveId = item["_LiveId"] is DBNull ? string.Empty : Convert.ToString(item["_LiveId"]),
                    });
                }
            }
            return res;
        }

        public object Call() => throw new NotImplementedException();

        public string GetName() => @"select dbo.fn_DT_FullFormat(t._EntryDate) _TransactionDate,t._LastBalance,t._Balance ,t._Amount ,t._Account,t._Optional1 _BankName,s._Type,t._LiveId
                                     From tbl_Transaction t(nolock) Left Join MASTER_RECHARGE_RESPTYPE s(nolock) on s._ID=t._Type 
                                     where _ServiceID = 32 /* and _UserID = @LoginId */ order by t._EntryDate Desc";
    }
}
