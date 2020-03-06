using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Handlers.Feedback.AddAmmendmentComment;
using SFA.DAS.ASK.Web.Controllers.Feedback;
using SFA.DAS.ASK.Web.ViewModels.Feedback;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.Feedback.ConfirmDetails
{
    [TestFixture]
    public class WhenConfirmingDetailsAreCorrect : FeedbackTestBase
    {
        private FeedbackConfirmDetailsController controller;
        
        [SetUp]
        public void Arrange()
        {
            controller = new FeedbackConfirmDetailsController(Mediator);
        }

        [Test]
        public async Task AndNoAmmendmentCommentsAreMade_ThenAddAmmendmentHandlerIsNotCalled()
        {
            var result = await controller.StartFeedback(FEEDBACK_ID, new ConfirmDetailsViewModel());

            Mediator.DidNotReceive().Send(Arg.Any<AddAmmendmentCommentCommand>());
        }

        [Test]
        public async Task AndaAnAmmendmentCommentsisMade_ThenAddAmmendmentHandlerIsCalledOnce()
        {
            var result = await controller.StartFeedback(FEEDBACK_ID, new ConfirmDetailsViewModel() { IncorrectDetailsComments = "Additional Comment"});

            Mediator.Received().Send(Arg.Any<AddAmmendmentCommentCommand>());
        }

        [Test]
        public async Task ThenTheUserIsRedirectedToTheFeedbackSection1()
        {
            var result = await controller.StartFeedback(FEEDBACK_ID, new ConfirmDetailsViewModel());

            result.As<RedirectToActionResult>().ControllerName.Should().Be("FeedbackSection1");
        }
    }
}
