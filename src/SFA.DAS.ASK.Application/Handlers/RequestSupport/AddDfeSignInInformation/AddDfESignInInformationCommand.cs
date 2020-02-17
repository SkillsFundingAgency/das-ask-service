using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInInformation
{
    public class AddDfESignInInformationCommand : IRequest<TempSupportRequest>
    {
        public Guid DfeSignInId { get; }
        public string Urn { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Guid RequestId { get; }

        public AddDfESignInInformationCommand(Guid dfeSignInId, string urn, string email, string firstName, string lastName, Guid requestId)
        {
            DfeSignInId = dfeSignInId;
            Urn = urn;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            RequestId = requestId;
        }
    }
}