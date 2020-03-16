using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Data.Entities
{
    public class DeliveryPartnerContact
    {
        public Guid Id { get; set; }
        public Guid DeliveryPartnerOrganisationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public DeliveryPartnerContact()
        {

        }
        
    }
}
