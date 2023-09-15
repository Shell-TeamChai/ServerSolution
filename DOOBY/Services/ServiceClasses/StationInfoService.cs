using DOOBY.DTOs;
using DOOBY.GloablExceptions;
using DOOBY.Models;
using DOOBY.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOOBY.Services.ServiceClasses
{
    public class StationInfoService : IStationInfo
    {
        private CaseStudyContext _context;

        public StationInfoService(CaseStudyContext context)
        {
            _context = context;
        }

        public Task<List<StationInfo>> GetAllStations()
        {
            var result = _context.StationInfos.ToListAsync();

            if (result == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[13]);
            }
            return result;
        }

        public async Task<StationInfo> GetStationInfoById(int stationId)
        {
            var _station = await _context.StationInfos.FindAsync(stationId);

            if (_station == null)
            {
                throw new NullReferenceException(ExceptionDetails.exceptionMessages[1]);
            }

            return _station;
        }
        public async Task<StationInfo> AddNewStation(StationInfo stationInfo)
        {
            await _context.StationInfos.AddAsync(stationInfo);
            await _context.SaveChangesAsync();
            var result = await _context.StationInfos.FindAsync(stationInfo.StationId);

            if (result == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[14]);
            }
            return result;
        }

        public async Task<StationInfo> UpdateStationInfo(StationInfo stationInfo)
        {
            var _station = await _context.StationInfos.FindAsync(stationInfo.StationId);

            if (_station == null)
            {
                throw new ArgumentException(ExceptionDetails.exceptionMessages[13]);
            }
            else
            {
                _station.Latitude = stationInfo.Latitude;
                _station.Longitude = stationInfo.Longitude;
                _station.TotalNodes = stationInfo.TotalNodes;
                _station.AvailableNodes = stationInfo.AvailableNodes;
                
                await _context.SaveChangesAsync();

                _station = await _context.StationInfos.FindAsync(stationInfo.StationId);
                return _station;
            }

        }

        public async void RemoveStation(int stationId)
        {
            var _station = await _context.StationInfos.FindAsync(stationId);
            if (_station == null)
            {
                throw new ArgumentException(ExceptionDetails.exceptionMessages[13]);
            }
            else
            {
                _context.StationInfos.Remove(_station);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<GeolocationInfoDTO>> GetAllGeoLocations()
        {
            List<GeolocationInfoDTO> geolocationInfos = new List<GeolocationInfoDTO>();

            var result = await _context.StationInfos.ToListAsync();

            if (result == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[15]);
            }

            foreach ( var info in result)
            {
                geolocationInfos.Add(new GeolocationInfoDTO(info));
            }
            return geolocationInfos;
        }
    }
}
