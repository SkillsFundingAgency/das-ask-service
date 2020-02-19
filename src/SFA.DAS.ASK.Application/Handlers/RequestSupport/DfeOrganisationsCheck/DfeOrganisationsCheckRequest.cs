using System;
using MediatR;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.DfeOrganisationsCheck
{
    public class DfeOrganisationsCheckRequest : IRequest<DfeOrganisationsCheckResponse>
    {
        public Guid DfeSignInId { get; }

        public DfeOrganisationsCheckRequest(Guid dfeSignInId)
        {
            DfeSignInId = dfeSignInId;
        }
    }
}