using System;
using MediatR;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInOrganisation
{
    public class AddDfESignInOrganisationCommand : IRequest
    {
        public Guid RequestId { get; }
        public Guid SelectedOrganisationId { get; }

        public AddDfESignInOrganisationCommand(Guid requestId, Guid selectedOrganisationId)
        {
            RequestId = requestId;
            SelectedOrganisationId = selectedOrganisationId;
        }
    }
}