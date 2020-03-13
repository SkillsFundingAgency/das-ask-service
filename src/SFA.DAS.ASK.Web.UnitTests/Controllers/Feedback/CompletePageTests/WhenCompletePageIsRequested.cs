using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SFA.DAS.ASK.Web.Controllers.Feedback;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.Feedback.CompletePageTests
{
    [TestFixture]
    public class WhenCompletePageIsRequested
    {
        [Test]
        public void ThenCorrectViewIsReturned()
        {
            var controller = new FeedbackCompleteController();
            var result = controller.Index(Guid.NewGuid());
            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().Be("~/Views/Feedback/Complete.cshtml");
        }
    }
}