using System;
using System.Collections.Generic;

namespace SFA.DAS.ASK.Application.Services.ReferenceData
{
    public class ReferenceDataSearchResult
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ReferenceDataAddress Address { get; set; }

        public string GetAddressString()
        {
            var addressParts = new List<string>(6);

            if (!string.IsNullOrWhiteSpace(Address.Line1)) addressParts.Add(Address.Line1);
            if (!string.IsNullOrWhiteSpace(Address.Line2)) addressParts.Add(Address.Line2);
            if (!string.IsNullOrWhiteSpace(Address.Line3)) addressParts.Add(Address.Line3);
            if (!string.IsNullOrWhiteSpace(Address.Line4)) addressParts.Add(Address.Line4);
            if (!string.IsNullOrWhiteSpace(Address.Line5)) addressParts.Add(Address.Line5);
            if (!string.IsNullOrWhiteSpace(Address.Postcode)) addressParts.Add(Address.Postcode);

            return string.Join(", ", addressParts);
        }
    }
}
