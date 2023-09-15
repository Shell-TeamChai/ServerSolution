using System.ComponentModel.DataAnnotations;
using DOOBY.Models;

namespace DOOBY.DTOs
{
    public class GeolocationInfoDTO
    {
        [Required]
        public string? Latitude { get; set; }

        [Required]
        public string? Longitude { get; set; }


        public GeolocationInfoDTO(StationInfo statioInfo)
        {
            Latitude = statioInfo.Latitude;
            Longitude = statioInfo.Longitude;
        }
    }
}
