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
    }
}
