using System;
using System.Collections.Generic;

namespace SFA.DAS.ASK.Data.Entities
{
    public class Visit
    {
        public Guid Id { get; set; }
        public Guid SupportRequestId { get; set; }
        public SupportRequest SupportRequest { get; set; }
        public List<VisitActivity> Activities { get; set; }
        public DateTime VisitDate { get; set; }

        public Guid OrganisationContactId { get; set; }
        public OrganisationContact OrganisationContact { get; set; }
    }
}