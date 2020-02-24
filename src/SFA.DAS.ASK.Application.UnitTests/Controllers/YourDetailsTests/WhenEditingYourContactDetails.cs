using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SFA.DAS.ASK.Web.Controllers.RequestSupport;
using SFA.DAS.ASK.Web.ViewModels.RequestSupport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.UnitTests.Controllers.YourDetailsTests
{
    [TestFixture]
    public class WhenEditingYourContactDetails
    {
        private YourDetailsController _sut;
        private Guid requestId = Guid.Parse("63be476e-0593-40c5-9b8d-8f0358a4d195");

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task Then()
        {
            var edit = await _sut.Index(requestId, true);

            var editPage = edit as ViewResult;
            var editAction = edit as ActionResult;
            
            var model = editPage.Model as YourDetailsViewModel;
            //Assert.is
            //Assert.AreEqual("index", editAction.)
            //Assert.IsTrue(model.Edit);
        }
    }
}
