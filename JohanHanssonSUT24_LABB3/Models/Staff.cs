using System;
using System.Collections.Generic;

namespace JohanHanssonSUT24_LABB3.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? StaffName { get; set; }

    public int? OccupationId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Occupation? Occupation { get; set; }
}
