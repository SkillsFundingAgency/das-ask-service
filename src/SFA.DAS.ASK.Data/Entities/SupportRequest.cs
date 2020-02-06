using System;
using Newtonsoft.Json;

namespace SFA.DAS.ASK.Data.Entities
{
    public class SupportRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobRole { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? OrganisationType { get; set; }
        public string OtherOrganisationType { get; set; }
        public string BuildingAndStreet1 { get; set; }
        public string BuildingAndStreet2 { get; set; }
        public string TownOrCity { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string AdditionalComments { get; set; }
        public bool Agree { get; set; }
        public bool Submitted { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public string SchoolName { get; set; }
    }
}