using Fintech.AppCode.Model;
using RoundpayFinTech.AppCode.Model;
using RoundpayFinTech.AppCode.Model.Exchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundpayFinTech.AppCode.Interfaces.Exchange
{
    public interface IExchangeML
    {
        ResponseStatus BuyerCommissionInsertupdate(BuyerCommission data);
        ResponseStatus SellerCommissionInsertupdate(SellerCommission data);
        IEnumerable<BuyerCommission> BuyerCommissionList(CommonReq req);
        IEnumerable<SellerCommission> SellerCommissionList(CommonReq req);
        IEnumerable<Settlement> SettlementReport(CommonReq req);
    }
}
