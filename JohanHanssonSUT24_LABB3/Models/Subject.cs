using System;
using System.Collections.Generic;

namespace JohanHanssonSUT24_LABB3.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string? SubjectName { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
