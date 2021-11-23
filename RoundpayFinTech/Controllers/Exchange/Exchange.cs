using Fintech.AppCode.Configuration;
using Fintech.AppCode.StaticModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoundpayFinTech.AppCode.Interfaces;
using RoundpayFinTech.AppCode.Interfaces.Exchange;
using RoundpayFinTech.AppCode.MiddleLayer;
using RoundpayFinTech.AppCode.MiddleLayer.Exchange;
using RoundpayFinTech.AppCode.Model.Exchange;
using RoundpayFinTech.AppCode.Model.Recharge;
using RoundpayFinTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundpayFinTech.Controllers
{
    public partial class ReportController 
    {
        [HttpGet]
        public IActionResult FundRedeem()
        {
            SattlementAccountModels res = new SattlementAccountModels();
            res.LoginID = _lr.UserID;
            if (_lr.LoginTypeID == LoginType.ApplicationUser)
            {
                if (_lr.RoleID.In(Role.Admin, Role.Retailor_Seller, Role.APIUser))
                {
                    IBankML bankML = new BankML(_accessor, _env);
                    var banks = bankML.BankMasters();
                    if (banks.Any())
                    {
                        var banaklist = banks.Select(x => new
                        {
                            ID = x.ID + "~" + x.IFSC,
                            BankName = x.BankName
                        });
                        res.Bankselect = new SelectList(banaklist, "ID", "BankName");
                    }
                }
            }
            return View("Exchange/FundRedeem", res);
        }

        [HttpPost]
        public IActionResult SettlementaccountList(int UserID)
        {
            IEnumerable<SattlementAccountModels> res = new List<SattlementAccountModels>();
            if (_lr.LoginTypeID == LoginType.ApplicationUser)
            {
                if (_lr.RoleID.In(Role.Admin, Role.Retailor_Seller, Role.APIUser))
                {
                    if ((UserID != _lr.UserID && _lr.RoleID == Role.Admin) || (_lr.UserID == UserID && _lr.RoleID != Role.Admin))
                    {
                        ISettlementaccountML ml = new SettlementaccountML(_accessor, _env);
                        res = ml.GetSettlementaccountList(UserID);
                    }
                }
            }
            return Json(res);
        }

        [HttpPost]
        public IActionResult SettlementReport(int userId)
        {
            userId = userId == 0 ? _lr.UserID : userId;
            IEnumerable <Settlement> res = new List<Settlement>();
            if (_lr.LoginTypeID == LoginType.ApplicationUser)
            {
                if (_lr.RoleID.In(Role.Admin, Role.APIUser))
                {
                    IExchangeML ml = new ExchangeML(_accessor, _env);
                    res = ml.SettlementReport(new Fintech.AppCode.Model.CommonReq { UserID = userId });
                }
            }
            return Json(res);
        }
    }
}
