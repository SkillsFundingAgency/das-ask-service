using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.GetDeliveryPartnerContact
{
    public class GetDeliveryPartnerContactHandler : IRequestHandler<GetDeliveryPartnerContactRequest, DeliveryPartnerContact>
    {
        private readonly AskContext _askContext;

        public GetDeliveryPartnerContactHandler(AskContext askContext)
        {
            _askContext = askContext;
        }

        public Task<DeliveryPartnerContact> Handle(GetDeliveryPartnerContactRequest request, CancellationToken cancellationToken)
        {
            return _askContext.DeliveryPartnerContacts.FirstOrDefaultAsync(contact => contact.Id == request.DeliveryPartnerContactId, cancellationToken: cancellationToken);
        }
    }
}
