using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.DAS.ASK.Data.Entities
{
    public class VisitActivity
    {
        public Guid Id { get; set; }
        public Guid VisitId { get; set; }
        public ActivityType ActivityType { get; set; }
    }
}