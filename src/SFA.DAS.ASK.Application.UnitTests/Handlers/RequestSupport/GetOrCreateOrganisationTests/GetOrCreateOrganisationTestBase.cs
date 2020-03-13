using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetOrCreateOrganisation;
using SFA.DAS.ASK.Data;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.GetOrCreateOrganisationTests
{
    [TestFixture]
    public class GetOrCreateOrganisationTestBase
    {
        [SetUp]
        public void Arrange()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new AskContext(dbContextOptions);
            
            Handler = new GetOrCreateOrganisationHandler(Context);
        }

        public GetOrCreateOrganisationHandler Handler { get; set; }

        protected AskContext Context { get; set; }
    }
}