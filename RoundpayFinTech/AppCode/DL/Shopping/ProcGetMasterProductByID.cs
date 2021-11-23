using Fintech.AppCode.DB;
using Fintech.AppCode.Interfaces;
using RoundpayFinTech.AppCode.Model.ProcModel;
using RoundpayFinTech.AppCode.Model.Shopping;
using System;
using System.Data;
using System.Data.SqlClient;


namespace RoundpayFinTech.AppCode.DL.Shopping
{
    public class ProcGetMasterProductByID : IProcedure
    {
        private readonly IDAL _dal;

        public ProcGetMasterProductByID(IDAL dal) => _dal = dal;

        public object Call(object obj)
        {
            var data = new MasterProduct();
            var PID = (int)obj;
            SqlParameter[] param = {
                new SqlParameter("@PID", PID)
            };
            try
            {
                DataTable dt = _dal.Get(GetName(), param);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    data.ProductID = Convert.ToInt32(dr["_ID"]);
                    data.ProductName = Convert.ToString(dr["_ProductName"]);
                    data.CategoryID = Convert.ToInt32(dr["_CategoryID"]);
                    data.SubCategoryID1 = Convert.ToInt32(dr["_SubCategoryID1"]);
                    data.SubCategoryID2 = Convert.ToInt32(dr["_SubCategoryID2"]);
                    data.Description = Convert.ToString(dr["_Description"]);
                    data.WalletDeductionPerc = Convert.ToDecimal(dr["_WalletDeductionPerc"]);
                }
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    ClassName = GetType().Name,
                    FuncName = "Call",
                    Error = ex.Message,
                    LoginTypeID = 1,
                    UserId = 1
                };
                var _ = new ProcPageErrorLog(_dal).Call(errorLog);
            }
            return data;
        }

        public object Call() => throw new NotImplementedException();
        public string GetName() => @"select * from Master_Product where _ID=@PID";
    }
}
