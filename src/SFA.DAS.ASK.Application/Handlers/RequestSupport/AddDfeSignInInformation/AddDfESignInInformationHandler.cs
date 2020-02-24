using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInInformation
{
    public class AddDfESignInInformationHandler : IRequestHandler<AddDfESignInInformationCommand, TempSupportRequest>
    {
        private readonly AskContext _context;
        private readonly IDfeSignInApiClient _dfeClient;
        private readonly IMediator _mediator;
        private string[] _dfeOrganisationAddress;

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
                _dfeOrganisationAddress = dfeOrganisation.Address.Split(new[] {','}, StringSplitOptions.None);
                if (_dfeOrganisationAddress.Length > 0)
                {
                    tempSupportRequest.BuildingAndStreet1 = GetAddressLine(0);
                    tempSupportRequest.BuildingAndStreet2 = GetAddressLine(1);
                    tempSupportRequest.TownOrCity = GetAddressLine(2);
                    tempSupportRequest.County = GetAddressLine(3);
                    tempSupportRequest.Postcode = _dfeOrganisationAddress[_dfeOrganisationAddress.Length - 1].Trim();    
                }
            }
            
            tempSupportRequest.FirstName = command.FirstName;
            tempSupportRequest.LastName = command.LastName;
            tempSupportRequest.PhoneNumber = dfeOrganisation.Telephone;
            tempSupportRequest.OrganisationName = dfeOrganisation.Name;
            tempSupportRequest.ReferenceId = dfeOrganisation.Urn;
            tempSupportRequest.DfeSignInId = command.DfeSignInId;
            
            await _context.SaveChangesAsync(cancellationToken);

            return tempSupportRequest;
        }
        
        private string GetAddressLine(int position)
        {
            return _dfeOrganisationAddress.Length > position ? _dfeOrganisationAddress[position].Trim() : "";
        }
    }
}