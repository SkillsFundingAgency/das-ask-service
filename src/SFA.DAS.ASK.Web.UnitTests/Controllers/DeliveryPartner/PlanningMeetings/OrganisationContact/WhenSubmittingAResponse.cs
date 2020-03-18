using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrCreatePlanningMeeting;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrganisationContacts;
using SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSupportRequest;
using SFA.DAS.ASK.Application.Handlers.Shared.CreateOrganisationContact;
using SFA.DAS.ASK.Web.Controllers.DeliveryPartner.PlanningMeetings;
using SFA.DAS.ASK.Web.ViewModels.DeliveryPartner.PlanningMeetings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.DeliveryPartner.PlanningMeetings.OrganisationContact
{
    [TestFixture]
    public class WhenSubmittingAResponse : PlanningMeetingControllersTestBase
    {
        private PlanningContactController sut;

        [SetUp]
        public void Arrange()
        {
            Mediator.Send(Arg.Any<GetPlanningMeetingRequest>()).Returns(GetPlanningMeeting());
            Mediator.Send(Arg.Any<GetOrganisationContactsRequest>()).Returns(GetOrganisationContacts());
            Mediator.Send(Arg.Any<GetSupportRequest>()).Returns(GetSupportRequest());

            sut = new PlanningContactController(Mediator, SessionService);
        }

        [Test]
        public async Task AndModelStateIsValid_ThenTheCorrectRedirectIsReturned()
        {
            var result = await sut.Index(SUPPORT_ID, new PlanningContactViewModel()
            {
                OrganisationId = ORGANISATION_ID,
                Contacts = new List<Data.Entities.OrganisationContact>()
                {
                    new Data.Entities.OrganisationContact()
                    { 
                        Id = ORGANISATION_CONTACT_ID_1
                    }
                },
                SelectedContact = ORGANISATION_CONTACT_ID_1
            });

            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
            result.As<RedirectToActionResult>().ControllerName.Should().Be("DeliveryPartnerContact");
        }

        [Test]
        public async Task AndModelStateIsInvalid_ThenTheCorrectViewIsReturned()
        {
            var result = await sut.Index(SUPPORT_ID, new PlanningContactViewModel()
            {
                OrganisationId = ORGANISATION_ID,
                Contacts = new List<Data.Entities.OrganisationContact>()
                {
                    new Data.Entities.OrganisationContact()
                    {
                        Id = ORGANISATION_CONTACT_ID_1
                    }
                },
                
            });

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/DeliveryPartner/PlanningMeetings/PlanningContact.cshtml");
        }

        [Test]
        public async Task WithANewContact_ThenANewContactIsCreated_AndTheCorrectRedirectReturned()
        {
            var result = await sut.Index(SUPPORT_ID, new PlanningContactViewModel()
            {
                OrganisationId = ORGANISATION_ID,
                Contacts = new List<Data.Entities.OrganisationContact>()
                {
                    new Data.Entities.OrganisationContact()
                    {
                        Id = ORGANISATION_CONTACT_ID_1
                    }
                },
                SelectedContact = Guid.Empty,
                NewFirstName = "NewFirstName",
                NewLastName = "NewLastName",
                NewEmail = "New@Email.com",
                NewPhoneNumber = "07111111111"

            });

            Mediator.Received(1).Send(Arg.Any<CreateOrganisationContactCommand>());

            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
            result.As<RedirectToActionResult>().ControllerName.Should().Be("DeliveryPartnerContact");
        }
        [Test]
        public async Task WithAnInvalidNewContact_ThenANewContactIsNotCreated_AndTheCorrectViewReturned()
        {
            var result = await sut.Index(SUPPORT_ID, new PlanningContactViewModel()
            {
                OrganisationId = ORGANISATION_ID,
                Contacts = new List<Data.Entities.OrganisationContact>()
                {
                    new Data.Entities.OrganisationContact()
                    {
                        Id = ORGANISATION_CONTACT_ID_1
                    }
                },
                SelectedContact = Guid.Empty
            });

            Mediator.Received(0).Send(Arg.Any<CreateOrganisationContactCommand>());

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/DeliveryPartner/PlanningMeetings/PlanningContact.cshtml");
        }
    }
}
