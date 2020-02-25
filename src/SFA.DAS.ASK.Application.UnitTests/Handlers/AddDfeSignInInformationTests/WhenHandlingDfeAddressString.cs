using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInInformation;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.UnitTests.Handlers.AddDfeSignInInformationTests
{
    [TestFixture]
    public class WhenHandlingDfeAddressString
    {
        private IDfeSignInApiClient _dfeSignInApiClient;
        private AskContext _dbContext;
        private Guid _dfeOrganisationId;
        private AddDfESignInInformationHandler _handler;
        private Guid _requestId;

        [SetUp]
        public async Task SetUp()
        {
            _dbContext = ContextHelper.GetInMemoryContext();
            _requestId = Guid.NewGuid();
            await _dbContext.TempSupportRequests.AddAsync(new TempSupportRequest(){Id = _requestId});
            await _dbContext.SaveChangesAsync();
            
            var mediator = Substitute.For<IMediator>();
            
            _dfeSignInApiClient = Substitute.For<IDfeSignInApiClient>();
            _dfeOrganisationId = Guid.NewGuid();
            
            _handler = new AddDfESignInInformationHandler(_dbContext, _dfeSignInApiClient, mediator);
        }
        
        [Test]
        public async Task ThenAShortAddressIsPopulatedCorrectly()
        {
            _dfeSignInApiClient.GetOrganisations(Arg.Any<Guid>()).Returns(new List<DfeOrganisation>(){new DfeOrganisation(){Address = "34 Meadow Way, WS12 4RT", Id = _dfeOrganisationId}});
            
            await _handler.Handle(new AddDfESignInInformationCommand(Guid.NewGuid(), _dfeOrganisationId, "email", "firstname", "lastname", _requestId, Guid.NewGuid()), CancellationToken.None);

            var savedTempSupportRequest = await _dbContext.TempSupportRequests.SingleAsync();
            savedTempSupportRequest.BuildingAndStreet1.Should().Be("34 Meadow Way");
            savedTempSupportRequest.BuildingAndStreet2.Should().BeEmpty();
            savedTempSupportRequest.TownOrCity.Should().BeEmpty();
            savedTempSupportRequest.County.Should().BeEmpty();
            savedTempSupportRequest.Postcode.Should().Be("WS12 4RT");
        }

        [Test] public async Task ThenANormalAddressIsPopulatedCorrectly()
        {
            _dfeSignInApiClient.GetOrganisations(Arg.Any<Guid>()).Returns(new List<DfeOrganisation>(){new DfeOrganisation(){Address = "34 Meadow Way, Heath Hayes, Cannock, Staffs, WS12 4RT", Id = _dfeOrganisationId}});
            
            await _handler.Handle(new AddDfESignInInformationCommand(Guid.NewGuid(), _dfeOrganisationId, "email", "firstname", "lastname", _requestId, Guid.NewGuid()), CancellationToken.None);

            var savedTempSupportRequest = await _dbContext.TempSupportRequests.SingleAsync();
            savedTempSupportRequest.BuildingAndStreet1.Should().Be("34 Meadow Way");
            savedTempSupportRequest.BuildingAndStreet2.Should().Be("Heath Hayes");
            savedTempSupportRequest.TownOrCity.Should().Be("Cannock");
            savedTempSupportRequest.County.Should().Be("Staffs");
            savedTempSupportRequest.Postcode.Should().Be("WS12 4RT");
        }
        
        [Test] public async Task ThenALongAddressIsPopulatedCorrectly()
        {
            _dfeSignInApiClient.GetOrganisations(Arg.Any<Guid>()).Returns(new List<DfeOrganisation>(){new DfeOrganisation(){Address = "34 Meadow Way, Heath Hayes, Another field, Cannock, Staffs, WS12 4RT", Id = _dfeOrganisationId}});
            
            await _handler.Handle(new AddDfESignInInformationCommand(Guid.NewGuid(), _dfeOrganisationId, "email", "firstname", "lastname", _requestId, Guid.NewGuid()), CancellationToken.None);

            var savedTempSupportRequest = await _dbContext.TempSupportRequests.SingleAsync();
            savedTempSupportRequest.BuildingAndStreet1.Should().Be("34 Meadow Way");
            savedTempSupportRequest.BuildingAndStreet2.Should().Be("Heath Hayes");
            savedTempSupportRequest.TownOrCity.Should().Be("Another field");
            savedTempSupportRequest.County.Should().Be("Cannock");
            savedTempSupportRequest.Postcode.Should().Be("WS12 4RT");
        }
    }
}