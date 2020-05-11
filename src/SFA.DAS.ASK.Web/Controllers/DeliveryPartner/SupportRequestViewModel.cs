using System;
using System.Linq;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner
{
    public class SupportRequestViewModel
    {
        public SupportRequestViewModel(SupportRequest sr)
        {
            OrganisationName = sr.Organisation.OrganisationName;
            OrganisationAddress = sr.Organisation.BuildingAndStreet1; // TODO: parse full address
            ContactName = sr.OrganisationContact.FirstName + " " + sr.OrganisationContact.LastName;
            ContactTelephone = sr.OrganisationContact.PhoneNumber;
            ContactEmail = sr.OrganisationContact.Email;

            if (sr.CurrentStatus == Status.NewRequest)
            {
                StatusDate = sr.EventLogs.Single(el => el.Status == Status.NewRequest).EventDate;    
            }
            else
            {
                StatusDate = sr.EventLogs.SingleOrDefault(el => el.Status == Status.Contacted)?.EventDate;    
            }
        }

        public string OrganisationName { get; set; }
        public string OrganisationAddress { get; set; }
        public string ContactName { get; set; }
        public string ContactTelephone { get; set; }
        public string ContactEmail { get; set; }
        public DateTime? StatusDate { get; set; }
    }
}