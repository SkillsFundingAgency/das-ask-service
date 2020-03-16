using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings
{
    public class PlanningContactViewModel
    {
        public Guid SelectedContact { get; set; }

        public string OrganisationName { get; set; }
        public Guid OrganisationId { get; set; }
        public List<OrganisationContact> Contacts { get; set; }

        public string NewFirstName{ get; set; }
        public string NewLastName { get; set; }
        public string NewPhoneNumber { get; set; }
        public string NewEmail { get; set; }

        public PlanningContactViewModel()
        {
        }
        public PlanningContactViewModel(SupportRequest supportRequest, PlanningMeeting planningMeeting, List<OrganisationContact> contacts)
        {
            OrganisationName = supportRequest.Organisation.OrganisationName;
            OrganisationId = supportRequest.OrganisationId;

            Contacts = contacts;
            SelectedContact = planningMeeting.OrganisationContactId.GetValueOrDefault();
        }

        public PlanningMeeting UpdatePlanningMeeting(PlanningMeeting planningMeeting)
        {
            planningMeeting.OrganisationContactId = SelectedContact;

            return planningMeeting;
        }
    }

}
