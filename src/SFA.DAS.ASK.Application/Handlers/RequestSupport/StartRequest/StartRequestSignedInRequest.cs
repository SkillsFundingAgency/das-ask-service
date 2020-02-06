using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest
{
    public class StartRequestSignedInRequest : IRequest<SupportRequest>
    {
        public Guid DfeSignInId { get; }

        public StartRequestSignedInRequest(Guid dfeSignInId)
        {
            DfeSignInId = dfeSignInId;
        }
    }
}