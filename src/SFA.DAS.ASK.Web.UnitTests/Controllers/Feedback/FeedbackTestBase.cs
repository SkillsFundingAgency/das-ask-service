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
        protected Guid VISIT_ID = Guid.NewGuid();
        protected Guid VISIT_ACTIVITY_ID = Guid.NewGuid();
        protected DateTime TEST_DATE = new DateTime();
        protected const string FIRST_NAME = "FirstName";
        protected const string LAST_NAME = "LastName";
        protected const string ORGANISATION_NAME = "Test Organisation";


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
                    OrganisationContact = new OrganisationContact() { FirstName = FIRST_NAME, LastName = LAST_NAME},
                    SupportRequest = new SupportRequest() { Organisation = new Organisation() { OrganisationName = ORGANISATION_NAME } },
                    Activities = new List<VisitActivity>()
                                     {
                                        new VisitActivity() { ActivityType = ActivityType.AwarenessAssembly, Id = VISIT_ACTIVITY_ID, VisitId = VISIT_ID }
                                     },
                    VisitDate = TEST_DATE
                },
                VisitId = VISIT_ID
            };
        }

    }
}
