using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Data
{
    public class AskContext : DbContext
    {
        public AskContext() { }
        public AskContext(DbContextOptions<AskContext> dbContextOptions) : base(dbContextOptions) {}
        
        public DbSet<SupportRequest> SupportRequests { get; set; }
        public DbSet<SupportRequestEventLog> SupportRequestEventLogs { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<OrganisationContact> OrganisationContacts { get; set; }
        public DbSet<TempSupportRequest> TempSupportRequests { get; set; }
        public DbSet<PostcodeRegion> PostcodeRegions { get; set; }
        public DbSet<DeliveryPartner> DeliveryPartners { get; set; }
        public DbSet<DeliveryArea> DeliveryAreas { get; set; }
        public DbSet<PlanningMeeting> PlanningMeetings { get; set; }
        public DbSet<DeliveryPartnerContact> DeliveryPartnerContacts { get; set; }
    }
}