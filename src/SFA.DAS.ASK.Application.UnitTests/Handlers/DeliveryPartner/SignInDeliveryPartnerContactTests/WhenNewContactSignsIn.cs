using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.SignInDeliveryPartnerContact;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.DeliveryPartner.SignInDeliveryPartnerContactTests
{
     [TestFixture]
    public class WhenNewContactSignsInWithUnknownDeliveryPartner
    {
        private Guid _deliveryPartnerId;
        private SignInDeliveryPartnerContactHandler _handler;
        private AskContext _dbContext;
        private ISessionService _sessionService;

        [SetUp]
        public async Task SetUp()
        {
            _deliveryPartnerId = Guid.NewGuid();
            
            _dbContext = ContextHelper.GetInMemoryContext();

            await _dbContext.DeliveryPartners.AddRangeAsync(new List<Data.Entities.DeliveryPartner>()
            {
                new Data.Entities.DeliveryPartner(){Id = _deliveryPartnerId, Name = "DP 1", UkPrn = 6543},
                new Data.Entities.DeliveryPartner(){Id = Guid.NewGuid(), Name = "DP 2", UkPrn = 1234}
            });
            
            await _dbContext.SaveChangesAsync();

            _sessionService = Substitute.For<ISessionService>();
            var dfeSignInApiClient = Substitute.For<IDfeSignInApiClient>();
            
            dfeSignInApiClient.GetOrganisations(Arg.Any<Guid>()).Returns(new List<DfeOrganisation>()
            {
                new DfeOrganisation(){Name = "Org 9", Id = Guid.NewGuid(), UkPrn = 9876}
            });

            _handler = new SignInDeliveryPartnerContactHandler(
                _dbContext, 
                _sessionService, 
                dfeSignInApiClient);
        }

        [Test]
        public async Task ThenANewDeliveryPartnerContactIsNotStored()
        {
            var newSignInId = Guid.NewGuid();
            await _handler.Handle(new SignInDeliveryPartnerContactRequest(newSignInId, "New DP Contact", "email@address.com"), CancellationToken.None);

            (await _dbContext.DeliveryPartnerContacts.CountAsync()).Should().Be(0);
        }
        
        [Test]
        public async Task ThenTheSignedInContactIsNotStoredInSession()
        {
            var newSignInId = Guid.NewGuid();
            await _handler.Handle(new SignInDeliveryPartnerContactRequest(newSignInId, "New DP Contact", "email@address.com"), CancellationToken.None);

            _sessionService.DidNotReceiveWithAnyArgs().Set("SignedInContact", Arg.Any<SignedInContact>());
        }
        
        [Test]
        public async Task ThenSuccessIsReturnedFalse()
        {
            var newSignInId = Guid.NewGuid();
            var result = await _handler.Handle(new SignInDeliveryPartnerContactRequest(newSignInId, "New DP Contact", "email@address.com"), CancellationToken.None);

            result.Success.Should().BeFalse();
        }
    }
    
    [TestFixture]
    public class WhenNewContactSignsIn
    {
        private Guid _deliveryPartnerId;
        private SignInDeliveryPartnerContactHandler _handler;
        private AskContext _dbContext;
        private ISessionService _sessionService;

        [SetUp]
        public async Task SetUp()
        {
            _deliveryPartnerId = Guid.NewGuid();
            
            _dbContext = ContextHelper.GetInMemoryContext();

            await _dbContext.DeliveryPartners.AddRangeAsync(new List<Data.Entities.DeliveryPartner>()
            {
                new Data.Entities.DeliveryPartner(){Id = _deliveryPartnerId, Name = "DP 1", UkPrn = 6543},
                new Data.Entities.DeliveryPartner(){Id = Guid.NewGuid(), Name = "DP 2", UkPrn = 1234}
            });
            
            await _dbContext.SaveChangesAsync();

            _sessionService = Substitute.For<ISessionService>();
            var dfeSignInApiClient = Substitute.For<IDfeSignInApiClient>();
            
            dfeSignInApiClient.GetOrganisations(Arg.Any<Guid>()).Returns(new List<DfeOrganisation>()
            {
                new DfeOrganisation(){Name = "DP 1", Id = Guid.NewGuid(), UkPrn = 6543}
            });

            _handler = new SignInDeliveryPartnerContactHandler(
                _dbContext, 
                _sessionService, 
                dfeSignInApiClient);
        }

        [Test]
        public async Task ThenANewDeliveryPartnerContactIsStored()
        {
            var newSignInId = Guid.NewGuid();
            await _handler.Handle(new SignInDeliveryPartnerContactRequest(newSignInId, "New DP Contact", "email@address.com"), CancellationToken.None);

            (await _dbContext.DeliveryPartnerContacts.CountAsync()).Should().Be(1);
            var newDeliveryPartnerContact = await _dbContext.DeliveryPartnerContacts.SingleAsync();
            newDeliveryPartnerContact.Email.Should().Be("email@address.com");
            newDeliveryPartnerContact.DeliveryPartnerId.Should().Be(_deliveryPartnerId);
            newDeliveryPartnerContact.FullName.Should().Be("New DP Contact");
            newDeliveryPartnerContact.SignInId.Should().Be(newSignInId);
        }
        
        [Test]
        public async Task ThenTheSignedInContactIsStoredInSession()
        {
            var newSignInId = Guid.NewGuid();
            await _handler.Handle(new SignInDeliveryPartnerContactRequest(newSignInId, "New DP Contact", "email@address.com"), CancellationToken.None);

            _sessionService.Received(1).Set("SignedInContact", Arg.Is<SignedInContact>(sic =>
                sic.DisplayName == "New DP Contact" 
                && sic.DeliveryPartnerName == "DP 1" 
                && sic.DeliveryPartnerId == _deliveryPartnerId));
        }
        
        [Test]
        public async Task ThenSuccessIsReturnedTrue()
        {
            var newSignInId = Guid.NewGuid();
            var result = await _handler.Handle(new SignInDeliveryPartnerContactRequest(newSignInId, "New DP Contact", "email@address.com"), CancellationToken.None);

            result.Success.Should().BeTrue();
        }
    }
}