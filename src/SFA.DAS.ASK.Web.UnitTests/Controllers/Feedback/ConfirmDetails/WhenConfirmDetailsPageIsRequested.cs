using FluentAssertions;
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

        [SetUp]
        public void Arrange()
        {
            //_controller = new FeedbackConfirmDetailsController();
        }

        [Test]
        public void ThenAViewResultSReturned()
        {
           // var result = _controller.Index();
          //  result.As<ViewResult>().ViewName.Should().Be("~/Views/Feedback/ConfirmDetails.cshtml");
        }
    }
}
