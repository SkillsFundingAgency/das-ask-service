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
        public async Task AndNoAmendmentCommentsAreMade_ThenAddAmendmentHandlerIsNotCalled()
        {
            await controller.StartFeedback(FeedbackId, new ConfirmDetailsViewModel());

            await Mediator.DidNotReceive().Send(Arg.Any<AddAmendmentCommentCommand>());
        }

        [Test]
        public async Task AndAnAmendmentCommentsIsMade_ThenAddAmendmentHandlerIsCalledOnce()
        {
            await controller.StartFeedback(FeedbackId, new ConfirmDetailsViewModel() { IncorrectDetailsComments = "Additional Comment"});

            await Mediator.Received().Send(Arg.Any<AddAmendmentCommentCommand>());
        }

        [Test]
        public async Task ThenTheUserIsRedirectedToTheFeedbackSection1()
        {
            var result = await controller.StartFeedback(FeedbackId, new ConfirmDetailsViewModel());

            result.As<RedirectToActionResult>().ControllerName.Should().Be("FeedbackSection1");
        }
    }
}
