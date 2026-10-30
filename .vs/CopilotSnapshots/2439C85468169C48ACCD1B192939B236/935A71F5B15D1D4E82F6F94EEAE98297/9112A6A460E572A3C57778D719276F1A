using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class StudentRequest
{
    public int StudentRequestId { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public string CoverageType { get; set; } = null!;

    public string? TopicDescription { get; set; }

    public string PostTitle { get; set; } = null!;

    public string TeachingMode { get; set; } = null!;

    public string BudgetType { get; set; } = null!;

    public int BudgetPerLecture { get; set; }

    public string Availability { get; set; } = null!;

    public string ConnectVia { get; set; } = null!;

    public string ConnectValue { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string PostStatus { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
