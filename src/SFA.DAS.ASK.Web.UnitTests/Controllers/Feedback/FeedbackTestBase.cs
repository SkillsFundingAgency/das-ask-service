using MediatR;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Services.Session;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers.Feedback
{
    public class FeedbackTestBase
    {
        protected Guid FEEDBACK_ID = Guid.NewGuid();
        protected const string FIRST_NAME = "FirstName";
        
        protected IMediator Mediator;
        protected ISessionService SessionService;

        [SetUp]
        public void Arrange()
        {
            Mediator = Substitute.For<IMediator>();
            SessionService = Substitute.For<ISessionService>();
        }

        public VisitFeedback GetVisitFeedback()
        {
            return new VisitFeedback()
            {
                Id = FEEDBACK_ID,
                FeedbackAnswers = new FeedbackAnswers() { },
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
