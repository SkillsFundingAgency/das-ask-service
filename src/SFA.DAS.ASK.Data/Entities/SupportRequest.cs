using System;

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
    }
}