using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings
{
    public class LogContactViewModel
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public bool Telephone { get; set; }
        public bool Email { get; set; }
        public bool SchedulePlanningMeeting { get; set; }

    }
}
