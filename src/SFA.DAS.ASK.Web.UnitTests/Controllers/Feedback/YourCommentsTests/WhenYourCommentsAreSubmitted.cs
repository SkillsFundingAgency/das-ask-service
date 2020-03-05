using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.Feedback;
using SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.Feedback.YourCommentsTests
{
    [TestFixture]
    public class WhenYourCommentsAreSubmitted
    {
        private IMediator _mediator;
        private Guid feedbackId;
        private FeedbackYourCommentsController _controller;

        [SetUp]
        public void Arrange()
        {
            _mediator = Substitute.For<IMediator>();
            feedbackId = Guid.Parse("BC2BFFD8-6B20-4BEC-BF33-F83C970DD73E");
            _controller = new FeedbackYourCommentsController(_mediator);
        }

        [Test]
        public async Task ThenUserIsRedirectedToTheFeedbackCompletePage()
        {
            _mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(GetVisitFeedback());

            var result = await _controller.Index(feedbackId, new YourCommentsViewModel());

            result.As<ViewResult>().ViewName.Should().Be("~/Views/Feedback/Complete.cshtml");
        }

        private VisitFeedback GetVisitFeedback()
        {
            return new VisitFeedback()
            {
                Id = Guid.NewGuid(),
                FeedbackAnswers = { },
                Status = 0,
                Visit = new Visit()
                {
                    OrganisationContact = new OrganisationContact() { FirstName = "First", LastName = "Last" },
                    SupportRequest = new SupportRequest() { Organisation = new Organisation() { OrganisationName = "Test Organisation" } },
                    Activities = new List<VisitActivity>()
                                     {
                                        new VisitActivity() { ActivityType = ActivityType.AwarenessAssembly, Id = Guid.NewGuid(), VisitId = Guid.NewGuid() }
                                     },
                    VisitDate = new DateTime()
                },
                VisitId = Guid.NewGuid()
            };
        }
    }
}
