using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Utils;

namespace SFA.DAS.ASK.Application.UnitTests.Utils
{
    [TestFixture]
    public class EnumHelperTests
    {
        [Test]
        public void GetEnumDescriptionReturnsCorrectDescription()
        {
            EnumHelper.GetEnumDescription(TestEnum.Value2).Should().Be("This is the description for value 2");
            EnumHelper.GetEnumDescription(TestEnum.Value1).Should().Be("Value1");
        }
        
        private enum TestEnum
        {
            Value1,
            [System.ComponentModel.Description("This is the description for value 2")]
            Value2,
            [System.ComponentModel.Description("This is the description for value 3")]
            Value3
        }
    }
}