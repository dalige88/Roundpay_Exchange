using RoundpayFinTech.AppCode.ThirdParty.HotelAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundpayFinTech.AppCode.Interfaces
{
    public interface ITrkTravalHotelMLAPI
    {
        Task<TekTvlErrorModel> SyncHotelDestinationApiDAta( string ip);
        Task<TekTvlSearchingResponse> Hotelsearch(TekTvlHotelSearchRequest req);
        Task<TekTvlErrorModel> SyncHotelMasterApiDAta(string ip, string cityId);
    }
}
