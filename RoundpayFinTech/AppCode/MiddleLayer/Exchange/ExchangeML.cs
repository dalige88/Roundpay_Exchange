using Fintech.AppCode.Configuration;
using Fintech.AppCode.DB;
using Fintech.AppCode.Interfaces;
using Fintech.AppCode.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RoundpayFinTech.AppCode.DL;
using RoundpayFinTech.AppCode.Interfaces.Exchange;
using RoundpayFinTech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundpayFinTech.AppCode.MiddleLayer.Exchange
{
    public class ExchangeML : IExchangeML
    {
        //private readonly IHttpContextAccessor _accessor;
        //private readonly IHostingEnvironment _env;
        private readonly IDAL _dal;
        public ExchangeML(IHttpContextAccessor accessor, IHostingEnvironment env)
        {
            var _c = new ConnectionConfiguration(accessor, env);
            _dal = new DAL(_c.GetConnectionString());
        }
        public IEnumerable<Settlement> SettlementReport(CommonReq req)
        {
            IProcedure proc = new ProcSettlementReport(_dal);
            return (List<Settlement>)proc.Call(req);
        }


        public IEnumerable<BuyerCommission> BuyerCommissionList(CommonReq req)
        {

          
            IProcedure proc = new ProcBuyerCommission(_dal);
            IProcedure proc1 = new ProcGetDenom(_dal);
            var ret = (List<BuyerCommission>)proc.Call(req);
           foreach(var itm in ret)
            {
                itm.Denomination = (string)proc1.Call(new CommonReq { UserID=itm.Id,CommonStr="B"});

            }


            return ret;
        }
        public IEnumerable<SellerCommission> SellerCommissionList(CommonReq req)
        {


            IProcedure proc = new ProcSellerCommission(_dal);

            return (List<SellerCommission>)proc.Call(req);
        }
        public ResponseStatus SellerCommissionInsertupdate(SellerCommission  data)
        {
            IProcedure proc = new ProcSellerCommissionUpdate(_dal);
            return (ResponseStatus)proc.Call(data);
        }

        public ResponseStatus BuyerCommissionInsertupdate(BuyerCommission data)
        {
            IProcedure proc = new ProcBuyerCommissionUpdate(_dal);
            return (ResponseStatus)proc.Call(data);
        }

        public IEnumerable<Commission_Denomination> DECommission_DenominationList(CommonReq req)
        {


            IProcedure proc = new ProcDenominationCircleAndOpwise(_dal);
            var ret = (List<Commission_Denomination>)proc.Call(req);
            return ret;
        }

    }
}
