using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Data.Entities
{
    public class PlanningMeeting
    {
        public Guid Id { get; set; }
        public Guid? SupportRequestId { get; set; }
        public Guid? OrganisationContactId { get; set; }
        public Guid? DeliveryPartnerContactId { get; set; }
        public MeetingType? MeetingType { get; set; }
        public DateTime? MeetingTimeAndDate { get; set; }
    }

    public enum MeetingType
    {
        // Enum descriptions
        FaceToFace,
        Telephone
    }
}
