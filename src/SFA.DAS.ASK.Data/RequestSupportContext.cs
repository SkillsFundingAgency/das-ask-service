using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Data
{
    public class RequestSupportContext : DbContext
    {
        public RequestSupportContext(DbContextOptions<RequestSupportContext> dbContextOptions) : base(dbContextOptions) {}
        
        public DbSet<SupportRequest> SupportRequests { get; set; }
        public DbSet<SupportRequestEventLog> SupportRequestEventLogs { get; set; }
    }
}