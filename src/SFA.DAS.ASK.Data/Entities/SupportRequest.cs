using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.ASK.Data.Entities
{
    public class SupportRequest
    {
        public Guid Id { get; set; }
       
        public string AdditionalComments { get; set; }

        public RequestStatus CurrentStatus { get; set; }

        public List<SupportRequestEventLog> EventLogs { get; set; }

        public Organisation Organisation { get; set; }
        public Guid OrganisationId { get; set; }

        public OrganisationContact OrganisationContact { get; set; }
        public Guid OrganisationContactId { get; set; }
    }

    public enum RequestStatus
    {
        Draft,
        Submitted,
        ContactConfirmed
    }
}