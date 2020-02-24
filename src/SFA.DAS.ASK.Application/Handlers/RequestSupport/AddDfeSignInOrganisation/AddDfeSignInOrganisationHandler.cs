using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Data;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInOrganisation
{
    public class AddDfeSignInOrganisationHandler : IRequestHandler<AddDfESignInOrganisationCommand>
    {
        private readonly AskContext _context;
        private readonly IDfeSignInApiClient _dfeSignInApiClient;
        private readonly ILogger<AddDfeSignInOrganisationHandler> _logger;

        public AddDfeSignInOrganisationHandler(AskContext context, IDfeSignInApiClient dfeSignInApiClient, ILogger<AddDfeSignInOrganisationHandler> logger)
        {
            _context = context;
            _dfeSignInApiClient = dfeSignInApiClient;
            _logger = logger;
        }
        public async Task<Unit> Handle(AddDfESignInOrganisationCommand request, CancellationToken cancellationToken)
        {
            var tempSupportRequest = _context.TempSupportRequests.Single(tsr => tsr.Id == request.RequestId);
            
            var dfeOrganisations = await _dfeSignInApiClient.GetOrganisations(tempSupportRequest.DfeSignInId.Value);

            var dfeOrganisation = dfeOrganisations.Single(o => o.Id == request.SelectedOrganisationId);           

            if (dfeOrganisation.Address != null)
            {
                _logger.LogInformation("AddDfeOrganisation: Address is " + dfeOrganisation.Address);
                var dfeOrganisationAddress = dfeOrganisation.Address.Split(new[] {','}, StringSplitOptions.None);
                tempSupportRequest.BuildingAndStreet1 = dfeOrganisationAddress[0].Trim();
                tempSupportRequest.BuildingAndStreet2 = dfeOrganisationAddress[1].Trim();
                tempSupportRequest.TownOrCity = dfeOrganisationAddress[2].Trim();
                tempSupportRequest.County = dfeOrganisationAddress[3].Trim();
                tempSupportRequest.Postcode = dfeOrganisationAddress[4].Trim();
            }
            else
            {
                tempSupportRequest.BuildingAndStreet1 = "";
                tempSupportRequest.BuildingAndStreet2 = "";
                tempSupportRequest.TownOrCity = "";
                tempSupportRequest.County = "";
                tempSupportRequest.Postcode = "";
            }
            
            tempSupportRequest.PhoneNumber = dfeOrganisation.Telephone;
            tempSupportRequest.OrganisationName = dfeOrganisation.Name;
            tempSupportRequest.ReferenceId = dfeOrganisation.Urn;
            tempSupportRequest.SelectedDfeSignInOrganisationId = request.SelectedOrganisationId;
            
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}