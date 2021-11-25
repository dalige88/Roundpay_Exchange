using Fintech.AppCode.DB;
using Fintech.AppCode.Interfaces;
using Fintech.AppCode.Model;
using Fintech.AppCode.StaticModel;
using RoundpayFinTech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.Exchange;
using RoundpayFinTech.AppCode.Model.ProcModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RoundpayFinTech.AppCode.DL
{
    public class ProcBuyerCommission : IProcedure
    {
        private readonly IDAL _dal;
        public ProcBuyerCommission(IDAL dal) => _dal = dal;

        public object Call(object obj)
        {
            var req = (CommonReq)obj;
            var res = new List<BuyerCommission>();
            SqlParameter[] param = {
                new SqlParameter("@BuyerId",req.UserID),
                new SqlParameter("@Id",req.CommonInt)
            };
            var dt = _dal.Get(GetName(), param);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    res.Add(new BuyerCommission
                    {
                        Id = item["_Id"] is DBNull ? 0 : Convert.ToInt32(item["_Id"]),
                        DenominationID = item["_DenominationID"] is DBNull ? 0 : Convert.ToInt32(item["_DenominationID"]),
                        
                        IsActive = item["_IsActive"] is DBNull ? false : Convert.ToBoolean(item["_IsActive"]),
                        OID = item["_OID"] is DBNull ? 0 : Convert.ToInt32(item["_OID"]),
                        CircleID = item["_CircleID"] is DBNull ? 0 : Convert.ToInt32(item["_CircleID"]),
                        Min = item["_Min"] is DBNull ? 0 : Convert.ToInt32(item["_Min"]),
                        Max = item["_Max"] is DBNull ? 0 : Convert.ToInt32(item["_Max"]),
                        Comm = item["_Comm"] is DBNull ? 0 : Convert.ToInt32(item["_Comm"]),
                        Ind =item["_Ind"] is DBNull ? 0 : Convert.ToInt32(item["_Ind"]),
                        BuyerId= item["_BuyerId"] is DBNull ? 0 : Convert.ToInt32(item["_BuyerId"]),
                        CommType=item["_CommType"] is DBNull ? false : Convert.ToBoolean(item["_CommType"]),
                        AmtType =item["_AmtType"] is DBNull ? false : Convert.ToBoolean(item["_AmtType"])


                    });
                }
            }
            return res;
        }

        public object Call() => throw new NotImplementedException();

        public string GetName() => @"select * from BuyerCommissionDetail where (_BuyerId=@BuyerId or @BuyerId<0)and (_Id=@Id or @Id<0)";
    }


    public class ProcSellerCommission : IProcedure
    {
        private readonly IDAL _dal;
        public ProcSellerCommission(IDAL dal) => _dal = dal;

        public object Call(object obj)
        {
            var req = (CommonReq)obj;
            var res = new List<SellerCommission>();
            SqlParameter[] param = {
                new SqlParameter("@SellerId",req.UserID),
                new SqlParameter("@Id",req.CommonInt)
            };
            var dt = _dal.Get(GetName(), param);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    res.Add(new SellerCommission
                    {
                        Id = item["_Id"] is DBNull ? 0 : Convert.ToInt32(item["_Id "]),
                        DenominationID = item["_DenominationID"] is DBNull ? 0 : Convert.ToInt32(item["_DenominationID "]),
                        EntryBy = item["_EntryBy"] is DBNull ? 0 : Convert.ToInt32(item["_EntryBy "]),
                        EntryDate = item["_EntryDate"] is DBNull ? 0 : Convert.ToInt32(item["_EntryDate "]),
                        IsActive = item["_IsActive"] is DBNull ? false : Convert.ToBoolean(item["_IsActive "]),
                        OID = item["_OID"] is DBNull ? 0 : Convert.ToInt32(item["_OID "]),
                        CircleID = item["_CircleID"] is DBNull ? 0 : Convert.ToInt32(item["_CircleID "]),
                        Min = item["_Min"] is DBNull ? 0 : Convert.ToInt32(item["_Min "]),
                        Max = item["_Max"] is DBNull ? 0 : Convert.ToInt32(item["_Max "]),
                        Comm = item["_Comm"] is DBNull ? 0 : Convert.ToInt32(item["_Comm "]),
                       // Ind = item["_Id"] is DBNull ? 0 : Convert.ToInt32(item["_Id "]),
                        SellerId = item["_SellerId"] is DBNull ? 0 : Convert.ToInt32(item["_SellerId "]),
                        CommType = item["_CommType"] is DBNull ? 0 : Convert.ToInt32(item["_CommType "]),
                        AmtType = item["_AmtType"] is DBNull ? false : Convert.ToBoolean(item["_AmtType "]),
                        Limit=item["_Limit"] is DBNull ? 0 : Convert.ToInt32(item["_Limit"]),
                        LimitType = item["_LimitType"] is DBNull ? 0 : Convert.ToInt32(item["_LimitType"]),


                    });
                }
            }
            return res;
        }

        public object Call() => throw new NotImplementedException();

        public string GetName() => @"select * from SellerCommissionDetail where (_SellerId=@SellerId or @SellerId<0)and (_Id=@Id or @Id<0)";
    }



    public class ProcSellerCommissionUpdate : IProcedure
    {
        private readonly IDAL _dal;
        public ProcSellerCommissionUpdate(IDAL dal) => _dal = dal;

        public object Call(object obj)
        {
            var req = (SellerCommission)obj;
            var res = new ResponseStatus
            {
                Statuscode = ErrorCodes.Minus1,
                Msg = ErrorCodes.TempError
            };
            SqlParameter[] param = {
               
                 new SqlParameter("@Id",req.Id), 
                 new SqlParameter("@SellerId",req.SellerId),
                 new SqlParameter("@OID",req.OID), 
                 new SqlParameter("@CircleID",req.CircleID), 
                 new SqlParameter("@APIId",req.APIId), 
                 new SqlParameter("@DenominationID",req.APIId),
                 new SqlParameter("@Min",req.Min),
                 new SqlParameter("@Max",req.Max), 
                 new SqlParameter("@Comm",req.Comm),
                 new SqlParameter("@CommType",req.CommType), 
                 new SqlParameter("@AmtType",req.AmtType), 
                 new SqlParameter("@Limit",req.Limit), 
                 new SqlParameter("@LimitType",req.LimitType),
                 new SqlParameter("@WithGST",req.WithGST), 
                 
                 new SqlParameter("@IsActive",req.IsActive)

            };
           
            try
            {
                DataTable dt = _dal.GetByProcedure(GetName(), param);
                if (dt.Rows.Count > 0)
                {
                    res.Statuscode = Convert.ToInt16(dt.Rows[0][0]);
                    res.Msg = dt.Rows[0]["Msg"] is DBNull ? "" : dt.Rows[0]["Msg"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "Call",
                    Error = ex.Message,
                    //LoginTypeID = req.LoginTypeID,
                  ///  UserId = req.LoginID
                };
                var _ = new ProcPageErrorLog(_dal).Call(errorLog);
            }

            return res;
        }

        public object Call() => throw new NotImplementedException();

        public string GetName() => "proc_UpdateSellerCommission";
    }


    public class ProcBuyerCommissionUpdate : IProcedure
    {
        private readonly IDAL _dal;
        public ProcBuyerCommissionUpdate(IDAL dal) => _dal = dal;

        public object Call(object obj)
        {
            var req = (BuyerCommission)obj;
            var res = new ResponseStatus
            {
                Statuscode = ErrorCodes.Minus1,
                Msg = ErrorCodes.TempError
            };
            SqlParameter[] param = {
               

               new SqlParameter("@Id",req.Id),
               new SqlParameter("@Ind",req.Ind),
               new SqlParameter("@BuyerId",req.BuyerId),
               new SqlParameter("@OID",req.OID),
               new SqlParameter("@DenominationID",req.DenominationID),
               new SqlParameter("@Min",req.Min),
               new SqlParameter("@Max",req.Max),
               new SqlParameter("@CircleID",req.CircleID),
               new SqlParameter("@Comm",req.Comm),
               new SqlParameter("@CommType",req.CommType), 
               new SqlParameter("@AmtType",req.AmtType) ,
               new SqlParameter("@WithGST",req.WithGST), 
               new SqlParameter("@IsActive",req.BuyerId),
               new SqlParameter("@Denomination",req.Denomination)

            };

            try
            {
                DataTable dt = _dal.GetByProcedure(GetName(), param);
                if (dt.Rows.Count > 0)
                {
                    res.Statuscode = Convert.ToInt16(dt.Rows[0][0]);
                    res.Msg = dt.Rows[0]["Msg"] is DBNull ? "" : dt.Rows[0]["Msg"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "Call",
                    Error = ex.Message,
                    //LoginTypeID = req.LoginTypeID,
                    ///  UserId = req.LoginID
                };
                var _ = new ProcPageErrorLog(_dal).Call(errorLog);
            }

            return res;
        }

        public object Call() => throw new NotImplementedException();

        public string GetName() => "proc_UpdateBuyerCommission";
    }

    

}
