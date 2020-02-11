using System;
using System.Collections.Generic;

namespace SFA.DAS.ASK.Data.Entities
{
    public class TempSupportRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobRole { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string OrganisationName { get; set; }
        public int? OrganisationType { get; set; }
        public string OtherOrganisationType { get; set; }
        public string BuildingAndStreet1 { get; set; }
        public string BuildingAndStreet2 { get; set; }
        public string TownOrCity { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string AdditionalComments { get; set; }
        public bool Agree { get; set; }
        public DateTime StartDate { get; set; }

        public SupportRequestType SupportRequestType { get; set; }
        public string ReferenceId { get; set; }
    }

    public enum SupportRequestType
    {
        DfeSignIn,
        Manual
    }
}