using System;
using System.Collections.Generic;

namespace JohanHanssonSUT24_LABB3.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public string? GradeChar { get; set; }

    public DateOnly? GradeDate { get; set; }

    public int? StudentId { get; set; }

    public int? TeacherId { get; set; }

    public int? SubjectId { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Subject? Subject { get; set; }

    public virtual Staff? Teacher { get; set; }
}
