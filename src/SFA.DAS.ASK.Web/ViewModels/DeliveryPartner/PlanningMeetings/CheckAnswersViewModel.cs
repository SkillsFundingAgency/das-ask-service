using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings
{
    public class CheckAnswersViewModel
    {
        public Guid SupportId { get; set; }
        public Organisation Organisation { get; set; }
        public DateTime MeetingDateAndTime { get; set; }
        public MeetingType MeetingType { get; set; }
        public OrganisationContact OrganisationContact { get; set; }
        public Guid MyId { get; set; }

        public DeliveryPartnerContact DeliveryPartnerContact { get; set; }

        public CheckAnswersViewModel(SupportRequest request, PlanningMeeting meeting, OrganisationContact contact, DeliveryPartnerContact deliveryPartnerContact)
        {
            Organisation = request.Organisation;

            MeetingDateAndTime = meeting.MeetingTimeAndDate.Value;
            MeetingType = meeting.MeetingType.Value;
            OrganisationContact = contact;
            SupportId = request.Id;
            DeliveryPartnerContact = deliveryPartnerContact;
        }
    }
}
