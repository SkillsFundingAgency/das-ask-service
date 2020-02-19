using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.AddNonDfeSignInInformation
{
    public class AddNonDfESignInInformationHandler : IRequestHandler<AddNonDfESignInInformationCommand, TempSupportRequest>
    {
        private readonly AskContext _context;
        private readonly IMediator _mediator;

        public AddNonDfESignInInformationHandler(AskContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<TempSupportRequest> Handle(AddNonDfESignInInformationCommand command, CancellationToken cancellationToken)
        {           
            var tempSupportRequest = _context.TempSupportRequests.Single(tsr => tsr.Id == command.RequestId);

            var organisation = command.Organisation;
            tempSupportRequest.BuildingAndStreet1 = organisation.Address.Line1;
            tempSupportRequest.BuildingAndStreet2 = organisation.Address.Line2;
            tempSupportRequest.TownOrCity = organisation.Address.Line3;
            tempSupportRequest.County = organisation.Address.Line4;
            tempSupportRequest.Postcode = organisation.Address.Postcode;
            tempSupportRequest.OrganisationName = organisation.Name;
            tempSupportRequest.ReferenceId = organisation.Code;
            
            await _context.SaveChangesAsync(cancellationToken);

            return tempSupportRequest;
        }
    }
}