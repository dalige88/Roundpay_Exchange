using Fintech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundpayFinTech.AppCode.Interfaces.Exchange
{
    public interface IExchangeML
    {
        IEnumerable<Settlement> SettlementReport(CommonReq req);
    }
}
