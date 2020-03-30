using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.GetSupportRequests;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.SignInDeliveryPartnerContact;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.DeliveryPartner.GetSupportRequestsTests
{
    [TestFixture]
    public class WhenGetSupportRequestsHandled
    {
        [Test]
        public async Task ThenTheCorrectSupportRequestsAreReturned()
        {
            var sessionService = Substitute.For<ISessionService>();
            var deliveryPartnerId = Guid.NewGuid();
            sessionService.Get<SignedInContact>("SignedInContact").Returns(new SignedInContact{DeliveryPartnerId = deliveryPartnerId});
            
            
            var dbContext = ContextHelper.GetInMemoryContext();

            var orgOneId = Guid.NewGuid();
            var orgTwoId = Guid.NewGuid();
            await dbContext.Organisations.AddRangeAsync(new List<Organisation>()
            {
                new Organisation(){OrganisationName = "Org 1", Id = orgOneId},
                new Organisation(){OrganisationName = "Org 2", Id = orgTwoId}
            });

            var orgContactOneId = Guid.NewGuid();
            var orgContactTwoId = Guid.NewGuid();
            await dbContext.OrganisationContacts.AddRangeAsync(new List<OrganisationContact>
            {
                new OrganisationContact{Id = orgContactOneId, FirstName = "Contact 1"},
                new OrganisationContact{Id = orgContactTwoId, FirstName = "Contact 2"}
            });

            await dbContext.SupportRequests.AddRangeAsync(new List<SupportRequest>
            {
                new SupportRequest{DeliveryPartnerId = Guid.NewGuid(), CurrentStatus = Status.Draft},
                new SupportRequest{DeliveryPartnerId = deliveryPartnerId, OrganisationId = orgOneId, OrganisationContactId = orgContactOneId, CurrentStatus = Status.Contacted},
                new SupportRequest{DeliveryPartnerId = deliveryPartnerId, OrganisationId = orgTwoId, OrganisationContactId = orgContactTwoId, CurrentStatus = Status.NewRequest}
            });
            await dbContext.SaveChangesAsync();

            var handler = new GetSupportRequestsHandler(sessionService, dbContext);
            var result = await handler.Handle(new GetSupportRequestsRequest(), CancellationToken.None);

            result.ContactedSupportRequests.Count().Should().Be(1);
            result.NewSupportRequests.Count().Should().Be(1);
        }
    }
}