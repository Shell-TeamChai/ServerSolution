using DOOBY.DTOs;
using DOOBY.Models;
using Microsoft.AspNetCore.Mvc;

namespace DOOBY.Services.Interfaces
{
    public interface IStationInfo
    {
        public Task<List<StationInfo>> GetAllStations();

        public Task<List<GeolocationInfoDTO>> GetAllGeoLocations();

        public Task<StationInfo> GetStationInfoById(int stationId);

        public Task<StationInfo> AddNewStation(StationInfo stationInfo);

        public Task<StationInfo> UpdateStationInfo(StationInfo stationInfo);

        public void RemoveStation(int stationId);
    }
}
