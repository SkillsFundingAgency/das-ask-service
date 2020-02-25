using System;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInInformation
{
    public class AddDfESignInInformationCommand : IRequest<TempSupportRequest>
    {
        public Guid DfeSignInId { get; }
        public Guid DfeOrganisationId { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Guid RequestId { get; }
        public Guid SignInId { get; }

        public AddDfESignInInformationCommand(Guid dfeSignInId, Guid dfeOrganisationId, string email, string firstName, string lastName, Guid requestId, Guid signInId)
        {
            DfeSignInId = dfeSignInId;
            DfeOrganisationId = dfeOrganisationId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            RequestId = requestId;
            SignInId = signInId;
        }
    }
}