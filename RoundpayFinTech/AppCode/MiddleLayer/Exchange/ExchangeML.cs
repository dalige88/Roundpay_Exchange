using Fintech.AppCode.Configuration;
using Fintech.AppCode.DB;
using Fintech.AppCode.Interfaces;
using Fintech.AppCode.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RoundpayFinTech.AppCode.DL;
using RoundpayFinTech.AppCode.Interfaces.Exchange;
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
    }
}
