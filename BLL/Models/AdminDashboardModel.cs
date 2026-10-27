using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class AdminDashboardModel
    {
        public int TotalUsers { get; set; }
        public int ActiveTutorOffers { get; set; }
        public int ActiveStudentRequests { get; set; }
        public int PendingModerationItems { get; set; }
    }
}
