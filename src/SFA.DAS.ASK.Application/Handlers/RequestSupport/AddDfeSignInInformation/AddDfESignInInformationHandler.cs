using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Application.DfeApi;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInInformation
{
    public class AddDfESignInInformationHandler : IRequestHandler<AddDfESignInInformationCommand, TempSupportRequest>
    {
        private readonly AskContext _context;
        private readonly IDfeSignInApiClient _dfeClient;
        private readonly IMediator _mediator;

        public AddDfESignInInformationHandler(AskContext context, IDfeSignInApiClient dfeClient, IMediator mediator)
        {
            _context = context;
            _dfeClient = dfeClient;
            _mediator = mediator;
        }

        public async Task<TempSupportRequest> Handle(AddDfESignInInformationCommand command, CancellationToken cancellationToken)
        {
            var dfeOrganisations = await _dfeClient.GetOrganisations(command.DfeSignInId);

            var dfeOrganisation = dfeOrganisations.Single(o => o.Id == command.Id);

            var tempSupportRequest = _context.TempSupportRequests.Single(tsr => tsr.Id == command.RequestId);

            tempSupportRequest.Agree = true;
            tempSupportRequest.Email = command.Email;
            if (dfeOrganisation.Address != null)
            {
                var dfeOrganisationAddress = dfeOrganisation.Address.Split(new[] {','}, StringSplitOptions.None);
                tempSupportRequest.BuildingAndStreet1 = dfeOrganisationAddress[0].Trim();
                tempSupportRequest.BuildingAndStreet2 = dfeOrganisationAddress[1].Trim();
                tempSupportRequest.TownOrCity = dfeOrganisationAddress[2].Trim();
                tempSupportRequest.County = dfeOrganisationAddress[3].Trim();
                tempSupportRequest.Postcode = dfeOrganisationAddress[4].Trim();
            }
            
            tempSupportRequest.FirstName = command.FirstName;
            tempSupportRequest.LastName = command.LastName;
            tempSupportRequest.PhoneNumber = dfeOrganisation.Telephone;
            tempSupportRequest.OrganisationName = dfeOrganisation.Name;
            tempSupportRequest.ReferenceId = dfeOrganisation.Urn;
            
            await _context.SaveChangesAsync(cancellationToken);

            return tempSupportRequest;
        }
    }
}