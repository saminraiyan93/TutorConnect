using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class TutorOffering
{
    public int TutorOfferingId { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public string CoverageType { get; set; } = null!;

    public string? TopicDescription { get; set; }

    public string PostTitle { get; set; } = null!;

    public string TeachingMode { get; set; } = null!;

    public string PricingType { get; set; } = null!;

    public int RateAmount { get; set; }

    public string Availability { get; set; } = null!;

    public string ContactVia { get; set; } = null!;

    public string ContactValue { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string PostStatus { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
