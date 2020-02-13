using System;
using MediatR;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInUserInformation
{
    public class AddDfeSignInUserInformationCommand : IRequest
    {
        public string Email { get; }
        public string Name { get; }
        public Guid RequestId { get; }

        public AddDfeSignInUserInformationCommand(string email, string name, Guid requestId)
        {
            Email = email;
            Name = name;
            RequestId = requestId;
        }
    }
}