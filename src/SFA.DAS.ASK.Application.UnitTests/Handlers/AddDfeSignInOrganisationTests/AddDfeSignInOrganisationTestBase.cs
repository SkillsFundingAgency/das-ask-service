using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInOrganisation;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.AddDfeSignInOrganisationTests
{
    [TestFixture]
    public class AddDfeSignInOrganisationTestBase
    {
        protected IDfeSignInApiClient DfeSignInApiClient;
        protected AddDfeSignInOrganisationHandler Handler;
        protected Guid SelectedOrganisationId;
        protected AskContext DbContext;
        protected Guid TempSupportRequestId;
        protected Guid DfeSignInId;

        [SetUp]
        public async Task SetUp()
        {
            DbContext = ContextHelper.GetInMemoryContext();
            TempSupportRequestId = Guid.NewGuid();
            DfeSignInId = Guid.NewGuid();
            await DbContext.TempSupportRequests.AddRangeAsync(new List<TempSupportRequest>
            {
                new TempSupportRequest{Id = Guid.NewGuid()},
                new TempSupportRequest{Id = TempSupportRequestId, DfeSignInId = DfeSignInId},
            });
            await DbContext.SaveChangesAsync();

            DfeSignInApiClient = Substitute.For<IDfeSignInApiClient>();

            SelectedOrganisationId = Guid.NewGuid(); 
            
            Handler = new AddDfeSignInOrganisationHandler(DbContext, DfeSignInApiClient);
        }
    }
}