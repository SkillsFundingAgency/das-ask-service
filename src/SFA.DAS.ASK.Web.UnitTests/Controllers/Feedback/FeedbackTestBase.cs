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
        protected Guid FeedbackId = Guid.NewGuid();
        private readonly Guid _visitId = Guid.NewGuid();
        private readonly Guid _visitActivityId = Guid.NewGuid();
        private readonly DateTime _testDate = new DateTime();
        private const string FirstName = "FirstName";
        private const string LastName = "LastName";
        private const string OrganisationName = "Test Organisation";

        protected IMediator Mediator;

        [SetUp]
        public void SetUp()
        {
            Mediator = Substitute.For<IMediator>();
        }

        protected VisitFeedback GetVisitFeedback()
        {
            return new VisitFeedback()
            {
                Id = FeedbackId,
                FeedbackAnswers = new FeedbackAnswers() { },
                Status = 0,
                Visit = new Visit()
                {
                    OrganisationContact = new OrganisationContact() { FirstName = FirstName, LastName = LastName},
                    SupportRequest = new SupportRequest() { Organisation = new Organisation() { OrganisationName = OrganisationName } },
                    Activities = new List<VisitActivity>()
                                     {
                                        new VisitActivity() { ActivityType = ActivityType.AwarenessAssembly, Id = _visitActivityId, VisitId = _visitId }
                                     },
                    VisitDate = _testDate
                },
                VisitId = _visitId
            };
        }

    }
}
