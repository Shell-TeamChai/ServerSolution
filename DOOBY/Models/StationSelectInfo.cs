using System;
using System.Collections.Generic;

namespace DOOBY.Models;

public partial class StationSelectInfo
{
    public int StationId { get; set; }

    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int Price { get; set; }

    public int? Count { get; set; }

    public virtual StationInfo Station { get; set; } = null!;
}
