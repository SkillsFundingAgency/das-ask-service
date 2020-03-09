using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.Feedback;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using ViewResult = Microsoft.AspNetCore.Mvc.ViewResult;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.Feedback.ConfirmDetails
{
    [TestFixture]
    public class WhenConfirmDetailsPageIsRequested : FeedbackTestBase
    {
        private FeedbackConfirmDetailsController _controller;
        

        [SetUp]
        public void Arrange()
        {
            _controller = new FeedbackConfirmDetailsController(Mediator);
        }

        [Test]
        public async Task ThenAViewResultIsReturned()
        {
            Mediator.Send(Arg.Any<GetVisitFeedbackRequest>()).Returns(GetVisitFeedback());

            var result = await _controller.Index(FEEDBACK_ID);

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/Feedback/ConfirmDetails.cshtml");
        }

        
    }
}
