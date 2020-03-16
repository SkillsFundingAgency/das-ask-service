using MediatR;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContacts
{
    public class GetDeliveryPartnerContactsHandler : IRequestHandler<GetDeliveryPartnerContactsRequest, List<DeliveryPartnerContact>>
    {
        private readonly AskContext _askContext;

        public GetDeliveryPartnerContactsHandler(AskContext askContext)
        {
            _askContext = askContext;
        }

        public async Task<List<DeliveryPartnerContact>> Handle(GetDeliveryPartnerContactsRequest request, CancellationToken cancellationToken)
        {
            return _askContext.DeliveryPartnerContacts.Where(x => x.DeliveryPartnerOrganisationId == request.DeliveryPartnerOrganisationId).ToList();
        }
    }
}
