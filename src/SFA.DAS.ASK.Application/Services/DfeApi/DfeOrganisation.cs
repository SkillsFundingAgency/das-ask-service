using System;

namespace SFA.DAS.ASK.Application.Services.DfeApi
{
    public class DfeOrganisation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DfeOrganisationCategory Category { get; set; }
        public string Urn { get; set; }
        public int? UkPrn { get; set; }
        public int EstablishmentNumber { get; set; }
        public DfeOrganisationStatus Status { get; set; }
        public DateTime? ClosedOn { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string statutoryLowAge { get; set; }
        public string statutoryHighAge { get; set; }
        public string LegacyId { get; set; }
        public string CompanyRegistrationNumber { get; set; }
    }
}