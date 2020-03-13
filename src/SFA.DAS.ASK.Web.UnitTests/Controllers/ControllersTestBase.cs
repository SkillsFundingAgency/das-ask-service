using MediatR;
using NSubstitute;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Services.Session;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace SFA.DAS.ASK.Web.UnitTests.Controllers
{
    
    public class ControllersTestBase
    {
        protected const string FIRST_NAME = "FirstName";
        protected Guid REQUEST_ID = Guid.NewGuid();

        protected IMediator Mediator;
        protected ISessionService SessionService;

        [SetUp]
        public void Arrange()
        {
            Mediator = Substitute.For<IMediator>();
            SessionService = Substitute.For<ISessionService>();
        }
    }
}
