using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DOOBY.Models;

public partial class StationInfo
{
    public int StationId { get; set; }

    public int? StationMaster { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public int? TotalNodes { get; set; }

    public int? AvailableNodes { get; set; }

    [JsonIgnore]
    public virtual Admin? StationMasterNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<StationSelectInfo> StationSelectInfos { get; set; } = new List<StationSelectInfo>();
}
