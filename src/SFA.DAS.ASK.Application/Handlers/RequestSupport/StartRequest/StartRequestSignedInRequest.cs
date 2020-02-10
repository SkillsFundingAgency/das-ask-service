using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartRequest
{
    public class StartRequestSignedInRequest : IRequest<SupportRequest>
    {
        public Guid DfeSignInId { get; }
        public string Email { get; }
        public string Name { get; }

        public StartRequestSignedInRequest(Guid dfeSignInId, string email, string name)
        {
            DfeSignInId = dfeSignInId;
            Email = email;
            Name = name;
        }
    }
}