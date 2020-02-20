using System;
using MediatR;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInUserInformation
{
    public class AddDfeSignInUserInformationCommand : IRequest
    {
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Guid RequestId { get; }
        public Guid DfeSignInId { get; }

        public AddDfeSignInUserInformationCommand(string email, string firstName, string lastName, Guid requestId, Guid dfeSignInId)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            RequestId = requestId;
            DfeSignInId = dfeSignInId;
        }
    }
}