using MediatR;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetOrganisationContacts
{
    

    public class GetOrganisationContactsHandler : IRequestHandler<GetOrganisationContactsRequest, List<OrganisationContact>>
    {
        private readonly AskContext _askContext;
   
        public GetOrganisationContactsHandler(AskContext askContext)
        {
            _askContext = askContext;
        }

        public async Task<List<OrganisationContact>> Handle(GetOrganisationContactsRequest request, CancellationToken cancellationToken)
        {
            return await _askContext.OrganisationContacts.Where(x => x.OrganisationId == request.OrganisationId).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
