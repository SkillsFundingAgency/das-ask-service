using System;
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
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.DeliveryPartner.SignInDeliveryPartnerContactTests
{
    [TestFixture]
    public class WhenExistingContactSignsIn
    {
        private Guid _deliveryPartnerId;
        private SignInDeliveryPartnerContactHandler _handler;
        private Guid _existingContactId;
        private Guid _signInId;
        private AskContext _dbContext;
        private ISessionService _sessionService;

        [SetUp]
        public async Task SetUp()
        {
            _deliveryPartnerId = Guid.NewGuid();
            _existingContactId = Guid.NewGuid();
            _signInId = Guid.NewGuid();
            
            _dbContext = ContextHelper.GetInMemoryContext();
            await _dbContext.DeliveryPartnerContacts.AddAsync(
                new DeliveryPartnerContact()
                {
                    Id = _existingContactId,
                    SignInId = _signInId,
                    FullName = "DP Contact", 
                    DeliveryPartner = new Data.Entities.DeliveryPartner() {Name = "DP", Id = _deliveryPartnerId}
                });
            await _dbContext.SaveChangesAsync();

            _sessionService = Substitute.For<ISessionService>();
            _handler = new SignInDeliveryPartnerContactHandler(
                _dbContext, 
                _sessionService, 
                Substitute.For<IDfeSignInApiClient>());
        }

        [Test]
        public async Task ThenNoNewContactIsCreated()
        {
            await _handler.Handle(new SignInDeliveryPartnerContactRequest(_signInId, "DP Contact", "email@address.com"), CancellationToken.None);

            (await _dbContext.DeliveryPartnerContacts.CountAsync()).Should().Be(1);
            (await _dbContext.DeliveryPartnerContacts.SingleAsync()).Id.Should().Be(_existingContactId);
        }

        [Test]
        public async Task ThenTheSignedInContactIsStoredInSession()
        {
            await _handler.Handle(new SignInDeliveryPartnerContactRequest(_signInId, "DP Contact", "email@address.com"), CancellationToken.None);

            _sessionService.Received(1).Set("SignedInContact", Arg.Is<SignedInContact>(sic =>
                sic.DisplayName == "DP Contact" 
                && sic.DeliveryPartnerName == "DP" 
                && sic.DeliveryPartnerId == _deliveryPartnerId));
        }
        
        [Test]
        public async Task ThenSuccessIsReturnedAsTrue()
        {
            var result = await _handler.Handle(new SignInDeliveryPartnerContactRequest(_signInId, "DP Contact", "email@address.com"), CancellationToken.None);

            result.Success.Should().BeTrue();
        }
    }
}