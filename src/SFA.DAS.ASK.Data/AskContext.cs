using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Data
{
    public class AskContext : DbContext
    {
        public AskContext(DbContextOptions<AskContext> dbContextOptions) : base(dbContextOptions) {}
        
        public DbSet<SupportRequest> SupportRequests { get; set; }
        public DbSet<SupportRequestEventLog> SupportRequestEventLogs { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<OrganisationContact> OrganisationContacts { get; set; }
        public DbSet<TempSupportRequest> TempSupportRequests { get; set; }
    }
}