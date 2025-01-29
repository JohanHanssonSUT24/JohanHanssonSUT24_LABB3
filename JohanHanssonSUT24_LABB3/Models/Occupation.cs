using System;
using System.Collections.Generic;

namespace JohanHanssonSUT24_LABB3.Models;

public partial class Occupation
{
    public int OccupationId { get; set; }

    public string? OccupationName { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
