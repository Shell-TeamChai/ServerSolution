using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DOOBY.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Type { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    [JsonIgnore]
    public virtual Admin? Admin { get; set; }

    [JsonIgnore]
    public virtual Customer? Customer { get; set; }
}
