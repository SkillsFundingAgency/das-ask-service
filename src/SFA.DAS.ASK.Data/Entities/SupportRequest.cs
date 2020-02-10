using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.ASK.Data.Entities
{
    public class SupportRequest
    {
        public Guid Id { get; set; }
       
        public string AdditionalComments { get; set; }
        public bool Agree { get; set; }

        public RequestStatus CurrentStatus { get; set; }

        public List<SupportRequestEventLog> EventLogs { get; set; }

        public Organisation Organisation { get; set; }
        public Guid OrganisationId { get; set; }

        public OrganisationContact OrganisationContact { get; set; }
        public Guid OrganisationContactId { get; set; }
    }

    public class Organisation
    {
        public Guid Id { get; set; }
        public int UkPrn { get; set; }
        public string OrganisationName { get; set; }
        public int? OrganisationType { get; set; }
        public string OtherOrganisationType { get; set; }
        public string BuildingAndStreet1 { get; set; }
        public string BuildingAndStreet2 { get; set; }
        public string TownOrCity { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
    }

    public class OrganisationContact
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobRole { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid OrganisationId { get; set; }
    }

    public class SupportRequestEventLog
    {
        public Guid Id { get; set; }
        public Guid SupportRequestId { get; set; }
        public DateTime EventDate { get; set; }
        public RequestStatus Status { get; set; }
        public string Email { get; set; }
    }

    public enum RequestStatus
    {
        Draft,
        Submitted,
        ContactConfirmed
    }
}