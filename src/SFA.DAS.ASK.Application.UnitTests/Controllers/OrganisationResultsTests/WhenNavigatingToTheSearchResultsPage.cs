//using System;
//using System.Collections.Generic;
//using System.Text;
//using MediatR;
//using Moq;
//using NUnit;
//using NUnit.Framework;
//using SFA.DAS.ASK.Web.Controllers.RequestSupport;
//using SFA.DAS.ASK.Application.Services.Session;

//namespace SFA.DAS.ASK.Application.UnitTests.Controllers.OrganisationResultsTests
//{
//    [TestFixture]
//    public class WhenNavigatingToTheSearchResultsPage
//    {

//        private OrganisationResultsController _sut;

//        private Mock<IMediator> _mockMediator;
//        private Mock<ISessionService> _mockSessionService;

//        private Guid requestId =// { 63be476e - 0593 - 40c5 - 9b8d - 8f0358a4d195 };

//        [SetUp]
//        public void Setup()
//        {
//            _mockMediator = new Mock<IMediator>();
//            _mockSessionService = new Mock<ISessionService>();

//            _sut = new OrganisationResultsController(_mockMediator.Object, _mockSessionService.Object);


//        }

//        [Test]
//        public void FromTheCheckYourAnswersPage_ThenSearchTermIsRetrievedFromSessionStorage()
//        {
//            var search = _sut.Index()
//        }
//    }
//}
