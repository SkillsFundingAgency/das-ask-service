using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings
{
    public class DeliveryPartnerContactViewModel
    {
        public Guid MyId { get; set; }

        public List<DeliveryPartnerContact> DeliveryPartnerContacts { get; set; }

        public Guid SelectedDeliveryPartnerContactId { get; set; }

        public Guid SupportId { get; set; }
        public bool Edit { get; set; }

        public DeliveryPartnerContactViewModel()
        {

        }

        public DeliveryPartnerContactViewModel(Guid myId, List<DeliveryPartnerContact> contacts, PlanningMeeting meeting, bool edit)
        {
            MyId = myId;
            DeliveryPartnerContacts = contacts;
            SelectedDeliveryPartnerContactId = meeting.DeliveryPartnerContactId.GetValueOrDefault();

            SupportId = meeting.SupportRequestId;
            Edit = edit;
        }

        public PlanningMeeting UpdatePlanningMeeting(PlanningMeeting planningMeeting)
        {
            planningMeeting.DeliveryPartnerContactId = SelectedDeliveryPartnerContactId;

            return planningMeeting;
        }
    }
}
