using System;

namespace SFA.DAS.ASK.Data.Entities
{
    public class Organisation
    {
        public Guid Id { get; set; }
        public string ReferenceId { get; set; }
        public string OrganisationName { get; set; }
        public int? OrganisationType { get; set; }
        public string OtherOrganisationType { get; set; }
        public string BuildingAndStreet1 { get; set; }
        public string BuildingAndStreet2 { get; set; }
        public string TownOrCity { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
    }
}