using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NServiceBus;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetOrCreateOrganisation;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetOrCreateOrganisationContact;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.SubmitSupportRequest;
using SFA.DAS.ASK.Application.Services.Email;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.Notifications.Messages.Commands;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.SubmitSupportRequestTests
{
    [TestFixture]
    public class WhenSubmitSupportRequestIsHandled
    {
        private AskContext _context;
        private Guid _midlandsDpId;
        private IEmailService _emailService;

        [SetUp]
        public async Task SetUp()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AskContext(dbContextOptions);

            var tempSupportRequestId = Guid.NewGuid();
            await _context.TempSupportRequests.AddAsync(new TempSupportRequest()
            {
                Id = tempSupportRequestId,
                Postcode = "WS12 2TG"
            });

            _midlandsDpId = Guid.NewGuid();
            var deliveryPartners = new List<DeliveryPartner>
            {
                new DeliveryPartner{Id = Guid.NewGuid(), Name = "London DP"},
                new DeliveryPartner{Id = _midlandsDpId, Name = "A Midlands DP"},
                new DeliveryPartner{Id = Guid.NewGuid(), Name = "South DP"}
            };
            await _context.DeliveryPartners.AddRangeAsync(deliveryPartners);

            await _context.DeliveryAreas.AddRangeAsync(new List<DeliveryArea>
            {
                new DeliveryArea{Id = 1, Area = "East Midlands", DeliveryPartnerId = deliveryPartners[1].Id},
                new DeliveryArea{Id = 2, Area = "West Midlands", DeliveryPartnerId = deliveryPartners[1].Id},
                new DeliveryArea{Id = 3, Area = "London", DeliveryPartnerId = deliveryPartners[0].Id},
                new DeliveryArea{Id = 4, Area = "South East", DeliveryPartnerId = deliveryPartners[2].Id}
            });

            await _context.PostcodeRegions.AddRangeAsync(new List<PostcodeRegion>
            {
                new PostcodeRegion{ PostcodePrefix = "WS", DeliveryAreaId = 2},
                new PostcodeRegion{ PostcodePrefix = "L", DeliveryAreaId = 3},
                new PostcodeRegion{ PostcodePrefix = "KT", DeliveryAreaId = 4}
            });
            
            await _context.SaveChangesAsync();
            
            var mediator = Substitute.For<IMediator>();

            mediator.Send(Arg.Any<GetOrCreateOrganisationRequest>()).Returns(new Organisation(){Id = Guid.NewGuid()});
            mediator.Send(Arg.Any<GetOrCreateOrganisationContactRequest>()).Returns(new OrganisationContact{Id = Guid.NewGuid(), FirstName = "Dave", Email = "dave@email.com"});

            _emailService = Substitute.For<IEmailService>();

            var handler = new SubmitSupportRequestHandler(_context, Substitute.For<ILogger<SubmitSupportRequestHandler>>(), mediator, Substitute.For<ISessionService>(), _emailService);
            await handler.Handle(new SubmitSupportRequest(new TempSupportRequest() {Id = tempSupportRequestId}), CancellationToken.None);
        }
        
        [Test]
        public void ThenASupportRequestIsSaved()
        {
            _context.SupportRequests.Should().HaveCount(1);
        }

        [Test]
        public async Task ThenTheCorrectDeliveryPartnerIsSelected()
        {
            var supportRequest = await _context.SupportRequests.SingleAsync();
            supportRequest.DeliveryPartnerId.Should().Be(_midlandsDpId);
        }

        [Test]
        public async Task ThenAnEmailMessageIsSent()
        {
            await _emailService.Received().SendSupportRequestSubmitted("dave@email.com", "Dave");
            // await _messageSession.Received().Send(Arg.Is<SendEmailCommand>(c => 
            //     c.RecipientsAddress == "dave@gmail.com" && 
            //     c.TemplateId == "e9383b85-c378-47f5-bc8c-269d44c586e9"));
        }
    }
}