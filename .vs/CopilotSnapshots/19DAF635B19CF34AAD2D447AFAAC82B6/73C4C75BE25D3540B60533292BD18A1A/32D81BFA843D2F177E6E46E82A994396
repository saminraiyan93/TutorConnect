using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string AccountStatus { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<StudentRequest> StudentRequests { get; set; } = new List<StudentRequest>();

    public virtual ICollection<TutorOffering> TutorOfferings { get; set; } = new List<TutorOffering>();
}
