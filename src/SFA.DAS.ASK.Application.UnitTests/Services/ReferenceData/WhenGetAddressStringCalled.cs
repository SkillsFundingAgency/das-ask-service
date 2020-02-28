using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.ASK.Application.Services.ReferenceData;

namespace SFA.DAS.ASK.Application.UnitTests.Services.ReferenceData
{
    [TestFixture]
    public class WhenGetAddressStringCalled
    {
        [Test]
        public void ThenCorrectFullAddressStringIsReturned()
        {
            var searchResult = new ReferenceDataSearchResult
            {
                Address = new ReferenceDataAddress
                {
                    Line1 = "Line 1",
                    Line2 = "Line 2",
                    Line3 = "Line 3",
                    Line4 = "Line 4",
                    Line5 = "Line 5",
                    Postcode = "Postcode"
                }
            };

            searchResult.GetAddressString().Should().Be("Line 1, Line 2, Line 3, Line 4, Line 5, Postcode");
        }
        
        [Test]
        public void ThenCorrectPartialAddressStringIsReturned()
        {
            var searchResult = new ReferenceDataSearchResult
            {
                Address = new ReferenceDataAddress
                {
                    Line1 = "Line 1",
                    Line2 = "",
                    Line3 = null,
                    Line4 = "   ",
                    Line5 = "Line 5",
                    Postcode = "Postcode"
                }
            };

            searchResult.GetAddressString().Should().Be("Line 1, Line 5, Postcode");
        }
    }
}