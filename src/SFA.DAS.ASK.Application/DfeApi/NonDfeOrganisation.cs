using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.DfeApi
{
    public class NonDfeOrganisation
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public int SubType { get; set; }
        public string Code { get; set; }
        public string RegistrationDate { get; set; }
        public NonDfeAddress Address { get; set; }
        public string Sector { get; set; }
        public Guid Guid { get; set; }

        public string GetAddressString(NonDfeAddress address)
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
