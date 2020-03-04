using FluentAssertions;
using MediatR;
using NUnit.Framework;
using SFA.DAS.ASK.Web.Controllers.Feedback;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.Feedback.ConfirmDetails
{
    [TestFixture]
    public class WhenConfirmDetailsPageIsRequested
    {
        private FeedbackConfirmDetailsController _controller;
        private IMediator _mediator;
        private Guid feedbackId;

        [SetUp]
        public void Arrange()
        {
            feedbackId = Guid.Parse("BC2BFFD8-6B20-4BEC-BF33-F83C970DD73E");
            _controller = new FeedbackConfirmDetailsController(_mediator);
        }

        [Test]
        public void ThenAViewResultIsReturned()
        {
            var result = _controller.Index(feedbackId);
            result.As<ViewResult>().ViewName.Should().Be("~/Views/Feedback/ConfirmDetails.cshtml");
        }
    }
}
