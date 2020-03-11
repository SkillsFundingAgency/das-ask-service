using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetOrCreateOrganisationContact;
using SFA.DAS.ASK.Data;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.GetOrCreateOrganisationContactTests
{
    [TestFixture]
    public class GetOrCreateOrganisationContactTestBase
    {
        protected GetOrCreateOrganisationContactHandler Handler;
        protected AskContext Context;
        
        [SetUp]
        public void Arrange()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new AskContext(dbContextOptions);
            
            Handler = new GetOrCreateOrganisationContactHandler(Context);
        }
    }
}