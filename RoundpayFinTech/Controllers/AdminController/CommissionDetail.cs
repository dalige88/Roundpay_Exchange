using Fintech.AppCode.Configuration;
using Fintech.AppCode.StaticModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoundpayFinTech.AppCode.Interfaces;
using RoundpayFinTech.AppCode.Interfaces.Exchange;
using RoundpayFinTech.AppCode.MiddleLayer;
using RoundpayFinTech.AppCode.MiddleLayer.Exchange;
using RoundpayFinTech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.Exchange;
using RoundpayFinTech.AppCode.Model.Recharge;
using RoundpayFinTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoundpayFinTech.Controllers
{ 
    public partial class ReportController
    {
        [HttpGet]
        [Route("/SellerCommissionDetail")]
        public IActionResult SellerCommissionDetail()
        {
            
            return View();
        }

        [HttpGet]
        [Route("/BuyerCommissionDetail")]
        public IActionResult BuyerCommissionDetail(int Id)
        {
            return View();
        }
        [HttpPost]
        [Route("/BuyerCommissionDetailList")]
        public IActionResult _BuyerCommissionDetailList()
        {
            
            IEnumerable<BuyerCommission> res = new List<BuyerCommission>();
            
            if (_lr.LoginTypeID == LoginType.ApplicationUser)
            {
                if (_lr.RoleID.In(Role.Admin))
                {
                    IExchangeML ml = new ExchangeML(_accessor, _env);
                    res = ml.BuyerCommissionList(new Fintech.AppCode.Model.CommonReq { UserID = _lr.LoginTypeID, CommonInt = -1 });
                }
            }

            return PartialView("PartialView/_BuyerCommissionList",res);
        }
        [HttpPost]
        [Route("/SellerCommissionDetailList/")]
        public IActionResult _SellerCommissionDetailList(int Id)
        {
            
            IEnumerable<SellerCommission> res = new List<SellerCommission>();
            if (_lr.LoginTypeID == LoginType.ApplicationUser)
            {
                if (_lr.RoleID.In(Role.Admin))
                {
                    IExchangeML ml = new ExchangeML(_accessor, _env);
                    res = ml.SellerCommissionList(new Fintech.AppCode.Model.CommonReq { UserID = _lr.LoginTypeID, CommonInt = -1});
                }
            }

            return PartialView("PartialView/_SellerCommissionList", res);
        }


        [HttpGet]
        [Route("/_SellerCommissionInsert/{Id}")]
        public IActionResult SellerCommissionInser(int Id = 0)
        {
            var OpML = new OperatorML(_accessor, _env);
            ICoupanVoucherML coupanVoucherML = new CoupanVoucherML(_accessor, _env);
            SellerCommission res = new SellerCommission();
            if (Id == 0)
            {
                res.Id = 0;
                res.SellerId = _lr.UserID;
            }
            else
            {
                if (_lr.LoginTypeID == LoginType.ApplicationUser)
                {
                    if (_lr.RoleID.In(Role.Admin))
                    {
                        IExchangeML ml = new ExchangeML(_accessor, _env);
                        res = ml.SellerCommissionList(new Fintech.AppCode.Model.CommonReq { UserID = _lr.UserID, CommonInt = Id }).FirstOrDefault();

                    }
                }

            }
            res.OpDetail = OpML.GetOperators(0);
            res.denominationsoucher = coupanVoucherML.GetDenominationVoucher();
            res.CircleList = OpML.CircleList().Result;
            return PartialView("PartialView/_SellerCommissionInsert");
            
        }

        [HttpGet]
        [Route("/_BuyerCommissionInsert/{Id}")]
        public IActionResult _BuyerCommissionInsert(int Id=0)
        {
            var OpML = new OperatorML(_accessor, _env);
            ICoupanVoucherML coupanVoucherML = new CoupanVoucherML(_accessor, _env);
            BuyerCommission res= new BuyerCommission();
            if (Id==0)
            {
                res.Id = 0;
                res.BuyerId = _lr.UserID;
            }
            else
            {
                if (_lr.LoginTypeID == LoginType.ApplicationUser)
                {
                    if (_lr.RoleID.In(Role.Admin))
                    {
                        IExchangeML ml = new ExchangeML(_accessor, _env);
                        res = ml.BuyerCommissionList(new Fintech.AppCode.Model.CommonReq { UserID = _lr.UserID, CommonInt =Id }).FirstOrDefault();
                        
                    }
                }

            }
            res.OpDetail = OpML.GetOperators(0);
            res.denominationsoucher = coupanVoucherML.GetDenominationVoucher();
            res.CircleList = OpML.CircleList().Result;
            return PartialView("PartialView/_BuyerCommissionInsert",res);
        }

        [HttpPost]
        [Route("/_BuyerCommissionInsertUpdate")]
        public IActionResult BuyerCommissionInsertUpdate(BuyerCommission data)
        {
            ResponseStatus res = new ResponseStatus();
            if (_lr.LoginTypeID == LoginType.ApplicationUser)
            {
                if (_lr.RoleID.In(Role.Admin))
                {
                    IExchangeML ml = new ExchangeML(_accessor, _env);
                    res = ml.BuyerCommissionInsertupdate(data);
                }
            }

            return Json(res);


            

        }
        [HttpPost]
        [Route("/_SellerCommissionInsertUpdate")]
        public IActionResult SellerCommissionInsertUpdate(SellerCommission date)
        {
            ResponseStatus res = new ResponseStatus();
            if (_lr.LoginTypeID == LoginType.ApplicationUser)
            {
                if (_lr.RoleID.In(Role.Admin))
                {
                    IExchangeML ml = new ExchangeML(_accessor, _env);
                    res = ml.SellerCommissionInsertupdate(date);
                }
            }

            return Json(res);

        }

    }
}
