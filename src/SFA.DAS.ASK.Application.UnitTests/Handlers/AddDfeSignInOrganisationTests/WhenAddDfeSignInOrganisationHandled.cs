using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInOrganisation;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.AddDfeSignInOrganisationTests
{
    [TestFixture]
    public class WhenAddDfeSignInOrganisationHandled
    {
        [Test]
        public async Task ThenTheTempSupportRequestIsUpdated()
        {
            var dbContext = ContextHelper.GetInMemoryContext();
            var tempSupportRequestId = Guid.NewGuid();
            await dbContext.TempSupportRequests.AddRangeAsync(new List<TempSupportRequest>
            {
                new TempSupportRequest{Id = Guid.NewGuid()},
                new TempSupportRequest{Id = tempSupportRequestId},
            });
            await dbContext.SaveChangesAsync();

            var dfeSignInApiClient = Substitute.For<IDfeSignInApiClient>();
            
            var handler = new AddDfeSignInOrganisationHandler(dbContext, dfeSignInApiClient);
        }
    }
}