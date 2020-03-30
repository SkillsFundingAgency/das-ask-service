using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.GetSupportRequestTests
{
    [TestFixture]
    public class WhenGetSupportRequestHandled
    {
        private Guid _organisationId;

        [Test]
        public async Task ThenTheCorrectSupportRequestIsReturned()
        {
            var dbContext = ContextHelper.GetInMemoryContext();

            _organisationId = Guid.NewGuid();
            var supportRequestId = Guid.NewGuid();
            await dbContext.SupportRequests.AddRangeAsync(new List<SupportRequest>
            {
                new SupportRequest{Id = supportRequestId, OrganisationId = _organisationId},
                new SupportRequest{Id = Guid.NewGuid()}
            });

            await dbContext.Organisations.AddAsync(new Organisation{Id = _organisationId, OrganisationName = "Org 1"});

            await dbContext.SaveChangesAsync();
            
            var handler = new GetSupportRequestHandler(dbContext);

            var result = await handler.Handle(new GetSupportRequest(supportRequestId), CancellationToken.None);

            result.Id.Should().Be(supportRequestId);
            result.Organisation.OrganisationName.Should().Be("Org 1");
        }
    }
}