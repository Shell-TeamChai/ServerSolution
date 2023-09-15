using DOOBY.Models;
using DOOBY.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOOBY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationInfoController : ControllerBase
    {
        IStationInfo _stationInfo;

        public StationInfoController(IStationInfo stationInfo)
        {
            _stationInfo = stationInfo;
        }

        [HttpGet("all/")]
        [ProducesResponseType(typeof(StationInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StationInfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<StationInfo>>> GetAllStations()
        {
            var result = await _stationInfo.GetAllStations();

            return result;
        }

        [HttpGet("{stationId}")]
        [ProducesResponseType(typeof(StationInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StationInfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<StationInfo> GetStationInfoById(int stationId)
        {
            var result = await _stationInfo.GetStationInfoById(stationId);

            return result;
        }

        [HttpGet("geolocations/")]
        [ProducesResponseType(typeof(StationInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StationInfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<StationInfo>>> GetAllGeoLocations()
        {
            var result = await _stationInfo.GetAllGeoLocations();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(StationInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StationInfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<StationInfo> AddNewStation(StationInfo stationInfo)
        {
            var result = await _stationInfo.AddNewStation(stationInfo);

            return result;
        }

        [HttpPut]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(StationInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StationInfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<StationInfo> UpdateStationInfo(StationInfo stationInfo)
        {
            var result = await _stationInfo.UpdateStationInfo(stationInfo);

            return result;
        }
    }
}
