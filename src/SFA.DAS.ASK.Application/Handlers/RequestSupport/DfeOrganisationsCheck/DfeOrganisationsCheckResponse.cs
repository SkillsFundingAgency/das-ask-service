using System;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.DfeOrganisationsCheck
{
    public class DfeOrganisationsCheckResponse
    {
        public Guid Id { get; set; }
        public DfeOrganisationsStatus DfeOrganisationsStatus { get; set; }
    }
}