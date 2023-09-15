using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DOOBY.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string[]? Permissions { get; set; }

    public virtual User AdminNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<StationInfo> StationInfos { get; set; } = new List<StationInfo>();
}
