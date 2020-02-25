using System;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;

namespace SFA.DAS.ASK.Application.UnitTests
{
    public static class ContextHelper
    {
        public static AskContext GetInMemoryContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            return new AskContext(dbContextOptions);
        }
    }
}