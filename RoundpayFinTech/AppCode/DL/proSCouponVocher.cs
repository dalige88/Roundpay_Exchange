using Fintech.AppCode.DB;
using Fintech.AppCode.Interfaces;
using Fintech.AppCode.Model;
using Fintech.AppCode.StaticModel;
using RoundpayFinTech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.ProcModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;


namespace RoundpayFinTech.AppCode.DL
{
    public class procCouponMasterList : IProcedure
    {
        private readonly IDAL _dal;
        public procCouponMasterList(IDAL dal) => _dal = dal;
        public string GetName() => "proc_GetCouponMasterList";
        public object Call(object obj)
        {
            var req = (CommonReq)obj;
            SqlParameter[] param = {
                new SqlParameter("@LoginID",req.LoginID),
                new SqlParameter("@LT",req.LoginTypeID),
                new SqlParameter("@OID",req.CommonInt),

            };
            var res = new List<CoupanMaster>();
            try
            {
                DataTable dt = _dal.GetByProcedure(GetName(), param);
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        var CoupanMaster = new CoupanMaster
                        {
                            ID = row["_ID"] is DBNull ? 0 : Convert.ToInt32(row["_ID"]),
                            VoucherType = row["_VoucherType"] is DBNull ? "" : row["_VoucherType"].ToString(),
                            OID = row["_OID"] is DBNull ? 0 : Convert.ToInt32(row["_OID"]),
                            OpName = row["_Name"] is DBNull ? "" : row["_Name"].ToString(),
                            Remark = row["_Remark"] is DBNull ? "" : row["_Remark"].ToString(),
                            LastModifyDate = Convert.ToDateTime(row["_ModifyDate"]).ToString("dd-MMM-yyyy hh:mm:ss tt"),
                            Max = row["_Max"] is DBNull ? 0 : Convert.ToInt32(row["_Max"]),
                            Min = row["_Min"] is DBNull ? 0 : Convert.ToInt32(row["_Min"]),
                            IsActive = row["_IsActive"] is DBNull ? true : Convert.ToBoolean(row["_IsActive"]),
                            DenominationID = row["_Amount"] is DBNull ? 0 : Convert.ToInt32(row["_Amount"])
                        };
                        res.Add(CoupanMaster);
                    }

                }
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "Call",
                    Error = ex.Message,
                    LoginTypeID = req.LoginTypeID,
                    UserId = req.LoginID
                };
                var _ = new ProcPageErrorLog(_dal).Call(errorLog);
            }
            if (req.CommonInt == -1)
                return res;
            else
                return res;
        }

        public object Call() => throw new NotImplementedException();

    }


    public class proc_VoucherCouponService : IProcedure
    {
        private readonly IDAL _dal;
        public proc_VoucherCouponService(IDAL dal) => _dal = dal;
        public string GetName() => "proc_VoucherCouponService";
        public object Call(object obj)
        {
            var req = (CouponData)obj;
            var res = new ResponseStatus
            {
                Statuscode = ErrorCodes.Minus1,
                Msg = ErrorCodes.TempError
            };
            SqlParameter[] param = {

new SqlParameter("@UserID",req.LoginID),
new SqlParameter("@Qty",req.Quantity),
new SqlParameter("@Amount",req.Amount),
new SqlParameter("@OID",req.OID),
new SqlParameter("@ToEmail",req.To),
new SqlParameter("@CustomerName",req.UserID),
new SqlParameter("@Message",req.Message),
new SqlParameter("@IP",req.RequestIP),
new SqlParameter("@Browser",req.Browser),
new SqlParameter("@RequestMode",req.RequestMode)
            };
            try
            {
                var dt = _dal.GetByProcedure(GetName(), param);
                if (dt.Rows.Count > 0)
                {
                    res.Statuscode = Convert.ToInt32(dt.Rows[0][0]);
                    res.Msg = dt.Rows[0]["Msg"] is DBNull ? "" : dt.Rows[0]["Msg"].ToString();
                    if (res.Statuscode == ErrorCodes.One)
                    {
                        res.CommonStr = dt.Rows[0]["CouponCode"] is DBNull ? "" : dt.Rows[0]["CouponCode"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "Call",
                    Error = ex.Message,
                    LoginTypeID = req.LoginTypeID,
                    UserId = req.LoginID
                };
                var _ = new ProcPageErrorLog(_dal).Call(errorLog);
            }
            return res;
        }

        public object Call() => throw new NotImplementedException();



    }

}
