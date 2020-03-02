using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetDfeOrganisations;
using SFA.DAS.ASK.Application.Services.DfeApi;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.RequestSupport.GetDfeOrganisationsTests
{
    [TestFixture]
    public class WhenGetDfeOrganisationsIsHandled
    {
        private IDfeSignInApiClient _dfeApiClient;
        private GetDfeOrganisationsHandler _handler;
        private Guid _dfeSignInId;
        private List<DfeOrganisation> _dfeOrganisationsFromTheApi;

        [SetUp]
        public void SetUp()
        {
            _dfeSignInId = Guid.NewGuid();
            
            _dfeApiClient = Substitute.For<IDfeSignInApiClient>();
            _dfeOrganisationsFromTheApi = new List<DfeOrganisation>()
            {
                new DfeOrganisation() {Id = Guid.NewGuid(), Name = "Org 1"},
                new DfeOrganisation() {Id = Guid.NewGuid(), Name = "Org 2"},
                new DfeOrganisation() {Id = Guid.NewGuid(), Name = "Org 3"}
            };
            _dfeApiClient.GetOrganisations(Arg.Any<Guid>()).Returns(_dfeOrganisationsFromTheApi);
            
            _handler = new GetDfeOrganisationsHandler(_dfeApiClient);
        }
        
        [Test]
        public async Task ThenTheRequestIsPassedOnToTheDfeApiClient()
        {
            await _handler.Handle(new GetDfeOrganisationsRequest(_dfeSignInId), CancellationToken.None);
            await _dfeApiClient.Received(1).GetOrganisations(_dfeSignInId);
        }
        
        [Test]
        public async Task ThenAListOfDfeOrganisationsIsReturned()
        {
            var dfeOrganisationsReturnedFromTheHandler = await _handler.Handle(new GetDfeOrganisationsRequest(_dfeSignInId), CancellationToken.None);
            dfeOrganisationsReturnedFromTheHandler.Should().BeEquivalentTo(_dfeOrganisationsFromTheApi);
        }
        
        
    }
}