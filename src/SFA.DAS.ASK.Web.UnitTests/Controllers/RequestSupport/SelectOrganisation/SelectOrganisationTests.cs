using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInOrganisation;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetDfeOrganisations;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.RequestSupport.SelectOrganisation
{
    [TestFixture]
    public class SelectOrganisationTests
    {
        private IMediator _mediator;
        private Guid _selectedDfeSignInOrganisationId;
        private Guid _dfeSignInId;
        private SelectOrganisationController _controller;
        private Guid _requestId;

        [SetUp]
        public async Task SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _selectedDfeSignInOrganisationId = Guid.NewGuid();
            _dfeSignInId = Guid.NewGuid();
            _mediator.Send(Arg.Any<GetTempSupportRequest>(), CancellationToken.None).Returns(new TempSupportRequest{DfeSignInId = _dfeSignInId, SelectedDfeSignInOrganisationId = _selectedDfeSignInOrganisationId});
            _mediator.Send(Arg.Any<GetDfeOrganisationsRequest>(), CancellationToken.None).Returns(new List<DfeOrganisation>()
            {
                new DfeOrganisation{Id = Guid.Parse("99FE258E-1FCE-4D69-83FA-27390F0BF9CA"), Name = "Org 1"},
                new DfeOrganisation{Id = Guid.Parse("7205A890-8779-4659-9F60-A1116584853B"), Name = "Org 2"}
            });
            
            _controller = new SelectOrganisationController(_mediator);

            _requestId = Guid.NewGuid();

        }
        
        [Test]
        public async Task WhenGetIndexIsCalled_ThenTheViewIsReturnedWithTheCorrectViewModel()
        {
            var result = await _controller.Index(_requestId);

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/RequestSupport/SelectOrganisation.cshtml");
            result.As<ViewResult>().Model.Should().BeOfType<SelectOrganisationViewModel>();

            var model = result.As<ViewResult>().Model.As<SelectOrganisationViewModel>();
            model.Organisations.Should().BeEquivalentTo(new Dictionary<Guid, string>
            {
                {Guid.Parse("99FE258E-1FCE-4D69-83FA-27390F0BF9CA"), "Org 1"},
                {Guid.Parse("7205A890-8779-4659-9F60-A1116584853B"), "Org 2"}
            });
            model.SelectedId.Should().Be(_selectedDfeSignInOrganisationId);
            model.RequestId.Should().Be(_requestId);
        }

        [Test]
        public async Task WhenPostIndexIsCalled_ThenTheCorrectOrganisationIdIsPassedToTheHandler()
        {
            var selectedId = Guid.NewGuid();
            var vm = new SelectOrganisationViewModel {SelectedId = selectedId};
            var result = await _controller.Index(_requestId, vm);

            await _mediator.Received().Send(Arg.Is<AddDfESignInOrganisationCommand>(request => request.RequestId == _requestId && request.SelectedOrganisationId == selectedId));

            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
            result.As<RedirectToActionResult>().ControllerName.Should().Be("CheckYourDetails");
        }

        [Test]
        public async Task WhenPostToIndexWithoutASelectedOrganisation_ThenRedirectedToPageAndHandlerNotCalled()
        {
            var vm = new SelectOrganisationViewModel {SelectedId = null};
          
            _controller.ModelState.AddModelError("SelectedId", "Required");

            var result = await _controller.Index(_requestId, vm);

            await _mediator.DidNotReceive().Send(Arg.Is<AddDfESignInOrganisationCommand>(request => request.RequestId == _requestId && request.SelectedOrganisationId == null));

            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        }
    }
}