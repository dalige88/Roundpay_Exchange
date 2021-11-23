using Fintech.AppCode.DB;
using Fintech.AppCode.Interfaces;
using RoundpayFinTech.AppCode.Model.ProcModel;
using RoundpayFinTech.AppCode.Model.Shopping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RoundpayFinTech.AppCode.DL.Shopping
{
    public class ProcOrderReport : IProcedure
    {
        private readonly IDAL _dal;

        public ProcOrderReport(IDAL dal) => _dal = dal;

        public object Call(object obj)
        {
            var res = new List<OrderReport>();
            var req = (OrderModel)obj;
            SqlParameter[] param = {
                new SqlParameter("@LoginId", req.LoginID),
                new SqlParameter("@Mobile", req.MobileNo??""),
                new SqlParameter("@Top", req.TopRows),
                new SqlParameter("@CategoryId", req.CategoryId),
                new SqlParameter("@VendorId", req.VendorId),
                new SqlParameter("@RequestMode", req.RequestedMode),
                new SqlParameter("@StatusId", req.StatusID),
                new SqlParameter("@UserId", req.UserID),
                new SqlParameter("@CustomerMobile", req.CCMobile??""),
                new SqlParameter("@CustomerName", req.CCName??""),
                new SqlParameter("@FromDate", req.FromDate??""),
                new SqlParameter("@ToDate", req.ToDate??""),
            };
            try
            {
                DataTable dt = _dal.GetByProcedure(GetName(), param);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        res.Add(new OrderReport
                        {
                            OrderDate = Convert.ToString(dr["OrderDate"]),
                            OrderDetailID = Convert.ToInt32(dr["_OrderDetailID"]),
                            OrderID = Convert.ToInt32(dr["_OrderID"]),
                            UserId = Convert.ToInt32(dr["_UserId"]),
                            UserName = Convert.ToString(dr["_Name"]),
                            Address = Convert.ToString(dr["_Address"]),
                            ProductCode = Convert.ToString(dr["_ProductCode"]),
                            ProductName = Convert.ToString(dr["_ProductName"]),
                            Quantity = Convert.ToInt32(dr["_Quantity"]),
                            MRP = Convert.ToDecimal(dr["_MRP"]),
                            Discount = dr["_Discount"] is DBNull ? 0 : Convert.ToDecimal(dr["_Discount"]),
                            SellingPrice = dr["_SellingPrice"] is DBNull ? 0 : Convert.ToDecimal(dr["_SellingPrice"]),
                            RequestAmount = dr["_RequestAmount"] is DBNull ? 0 : Convert.ToDecimal(dr["_RequestAmount"]),
                            DebitAmount = dr["_DebitedAmount"] is DBNull ? 0 : Convert.ToDecimal(dr["_DebitedAmount"]),
                            AdminCommission = dr["_AdminCommTotal"] is DBNull ? 0 : Convert.ToDecimal(dr["_AdminCommTotal"]),
                            RetailCommission = dr["_RetailCommission"] is DBNull ? 0 : Convert.ToDecimal(dr["_RetailCommission"]),
                            TeamCommission = dr["_TeamCommission"] is DBNull ? 0 : Convert.ToDecimal(dr["_TeamCommission"]),
                            VendorName = Convert.ToString(dr["_VendorName"]),
                            VendorPayble = dr["_VendorPayable"] is DBNull ? 0 : Convert.ToDecimal(dr["_VendorPayable"]),
                            IsVendorPaid = dr["_IsVendorPaid"] is DBNull ? false : Convert.ToBoolean(dr["_IsVendorPaid"]),
                            Status = Convert.ToString(dr["_Status"]),
                            OrderStatusID = Convert.ToInt32(dr["_StatusID"]),
                            PDeduction = dr["_Prepaid"] is DBNull ? 0 : Convert.ToDecimal(dr["_Prepaid"]),
                            SDeduction = dr["_Utility"] is DBNull ? 0 : Convert.ToDecimal(dr["_Utility"]),
                            UserCommAmount = dr["_UserCommAmount"] is DBNull ? 0 : Convert.ToDecimal(dr["_UserCommAmount"]),
                            GSTRate = dr["_GSTRate"] is DBNull ? 0 : Convert.ToDecimal(dr["_GSTRate"]),
                            GSTAmt = dr["_GSTAmt"] is DBNull ? 0 : Convert.ToDecimal(dr["_GSTAmt"]),
                            TDSRate = dr["_TDSRate"] is DBNull ? 0 : Convert.ToDecimal(dr["_TDSRate"]),
                            TDSAmt = dr["_TDSAmt"] is DBNull ? 0 : Convert.ToDecimal(dr["_TDSAmt"]),
                            ShippingCharge = dr["_ShippingCharge"] is DBNull ? 0 : Convert.ToDecimal(dr["_ShippingCharge"])
                        });
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
                    LoginTypeID = req.LT,
                    UserId = req.LoginID
                };
                var _ = new ProcPageErrorLog(_dal).Call(errorLog);
            }
            return res;
        }
        public object Call() => throw new NotImplementedException();
        public string GetName() => "Proc_OrderReport";
    }
}