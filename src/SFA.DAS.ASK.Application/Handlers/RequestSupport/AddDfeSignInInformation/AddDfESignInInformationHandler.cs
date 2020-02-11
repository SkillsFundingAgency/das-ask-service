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
            var dfeOrganisations = _dfeClient.GetOrganisations(command.DfeSignInId);

            var dfeOrganisation = dfeOrganisations.Single(o => o.Urn == command.Urn);

            var dfeOrganisationAddress = dfeOrganisation.Address.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            var tempSupportRequest = _context.TempSupportRequests.Single(tsr => tsr.Id == command.RequestId);

            tempSupportRequest.Agree = true;
            tempSupportRequest.Email = command.Email;
            tempSupportRequest.BuildingAndStreet1 = dfeOrganisationAddress[0];
            tempSupportRequest.BuildingAndStreet2 = dfeOrganisationAddress[1];
            tempSupportRequest.TownOrCity = dfeOrganisationAddress[2];
            tempSupportRequest.County = dfeOrganisationAddress[3];
            tempSupportRequest.Postcode = dfeOrganisationAddress[4];
            tempSupportRequest.FirstName = command.Name.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)[0];
            tempSupportRequest.LastName = command.Name.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)[1];
            tempSupportRequest.PhoneNumber = dfeOrganisation.Telephone;
            tempSupportRequest.OrganisationName = dfeOrganisation.Name;
            tempSupportRequest.ReferenceId = dfeOrganisation.UkPrn.ToString();
            
            await _context.SaveChangesAsync(cancellationToken);

            return tempSupportRequest;
        }
    }
}