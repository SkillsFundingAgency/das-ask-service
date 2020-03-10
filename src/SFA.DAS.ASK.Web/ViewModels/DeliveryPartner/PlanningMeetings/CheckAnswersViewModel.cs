using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings
{
    public class CheckAnswersViewModel
    {
        public string OrganisationName { get; set; }
        // public Address OrganisationAddress {get;set;}
        public DateTime MeetingDateAndTime { get; set; }
        public MeetingType MeetingType { get; set; }

        //public Contact OrganisationContact { get; set; }
        //public Contact DeliveryPartnerContact { get; set; }
    }
}
