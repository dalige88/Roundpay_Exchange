﻿using Fintech.AppCode.DB;
using Fintech.AppCode.Interfaces;
using RoundpayFinTech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.Reports.Filter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RoundpayFinTech.AppCode.DL
{
    public class ProcDaybookDateAPIwise : IProcedure
    {
        private readonly IDAL _dal;
        public ProcDaybookDateAPIwise(IDAL dal) => _dal = dal;
        public object Call(object obj)
        {
            var _req = (ForDateFilter)obj;
            SqlParameter[] param = {
                new SqlParameter("@FromDate", _req.FromDate ?? DateTime.Now.ToString("dd MMM yyyy")),
                new SqlParameter("@ToDate", _req.ToDate ?? DateTime.Now.ToString("dd MMM yyyy")),
                new SqlParameter("@APIID", _req.EventID),
                new SqlParameter("@LoginID", _req.LoginID),
                new SqlParameter("@LT", _req.LT)
            };
            var _res = new List<Daybook>();
            try
            {
                var dt = _dal.GetByProcedure(GetName(), param);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var daybook = new Daybook
                        {
                            OID = Convert.ToInt32(row["_OID"] is DBNull ? 0 : row["_OID"]),
                            Operator = row["_Operator"] is DBNull ? "" : row["_Operator"].ToString(),
                            API = row["_API"] is DBNull ? "" : row["_API"].ToString(),
                            TotalHits = Convert.ToInt32(row["TotalHits"] is DBNull ? 0 : row["TotalHits"]),
                            TotalAmount = Convert.ToDecimal(row["TotalAmount"] is DBNull ? 0 : row["TotalAmount"]),
                            SuccessHits = Convert.ToInt32(row["SuccessHits"] is DBNull ? 0 : row["SuccessHits"]),
                            SuccessAmount = Convert.ToDecimal(row["SuccessAmount"] is DBNull ? 0 : row["SuccessAmount"]),
                            RefundHits = Convert.ToInt32(row["RefundHits"] is DBNull ? 0 : row["RefundHits"]),
                            RefundAmount = Convert.ToDecimal(row["RefundAmount"] is DBNull ? 0 : row["RefundAmount"]),
                            FailedHits = Convert.ToInt32(row["FailedHits"] is DBNull ? 0 : row["FailedHits"]),
                            FailedAmount = Convert.ToDecimal(row["FailedAmount"] is DBNull ? 0 : row["FailedAmount"]),
                            PendingHits = Convert.ToInt32(row["PendingHits"] is DBNull ? 0 : row["PendingHits"]),
                            PendingAmount = Convert.ToDecimal(row["PendingAmount"] is DBNull ? 0 : row["PendingAmount"]),
                            APICommission = Convert.ToDecimal(row["APICommission"] is DBNull ? 0 : row["APICommission"]),
                            Commission = Convert.ToDecimal(row["Commission"] is DBNull ? 0 : row["Commission"]),
                            Incentive = Convert.ToDecimal(row["Incentive"] is DBNull ? 0 : row["Incentive"]),
                            GSTTaxAmount = Convert.ToDecimal(row["GSTTaxAmount"] is DBNull ? 0 : row["GSTTaxAmount"]),
                            TDSAmount = Convert.ToDecimal(row["TDSAmount"] is DBNull ? 0 : row["TDSAmount"]),
                            EntryDate = Convert.ToDateTime(row["_EntryDate"] is DBNull ? "" : row["_EntryDate"]),
                            TeamCommission = Convert.ToDecimal(row["TeamCommission"] is DBNull ? 0 : row["TeamCommission"])
                        };
                        daybook.Profit = daybook.APICommission - daybook.Commission - daybook.TeamCommission-daybook.Incentive;
                        _res.Add(daybook);
                    }
                }
            }
            catch (Exception)
            { }
            return _res;
        }

        public object Call() => throw new NotImplementedException();

        public string GetName() => "proc_DaybookDateAPIwise";
    }
}
