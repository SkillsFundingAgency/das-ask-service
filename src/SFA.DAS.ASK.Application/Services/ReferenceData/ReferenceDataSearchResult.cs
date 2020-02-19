using System;
using System.Collections.Generic;

namespace SFA.DAS.ASK.Application.Services.ReferenceData
{
    public class ReferenceDataSearchResult
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public OrganisationType Type { get; set; }
        public int SubType { get; set; }
        public string Code { get; set; }
        public ReferenceDataAddress Address { get; set; }
        public string Sector { get; set; }

        public string GetAddressString(ReferenceDataAddress address)
        {
            var addressParts = new List<string>(6);

            if (!string.IsNullOrWhiteSpace(address.Line1)) addressParts.Add(address.Line1);
            if (!string.IsNullOrWhiteSpace(address.Line2)) addressParts.Add(address.Line2);
            if (!string.IsNullOrWhiteSpace(address.Line3)) addressParts.Add(address.Line3);
            if (!string.IsNullOrWhiteSpace(address.Line4)) addressParts.Add(address.Line4);
            if (!string.IsNullOrWhiteSpace(address.Line5)) addressParts.Add(address.Line5);
            if (!string.IsNullOrWhiteSpace(address.Postcode)) addressParts.Add(address.Postcode);

            return string.Join(", ", addressParts);
        }
    }
}
