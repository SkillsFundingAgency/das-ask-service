using System;
using System.Collections.Generic;
using MediatR;
using SFA.DAS.ASK.Application.Services.DfeApi;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetDfeOrganisations
{
    public class GetDfeOrganisationsRequest : IRequest<List<DfeOrganisation>>
    {
        public Guid DfeSignInId { get; }

        public GetDfeOrganisationsRequest(Guid dfeSignInId)
        {
            DfeSignInId = dfeSignInId;
        }
    }
}