﻿using MediatR;
using SFA.DAS.ASK.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.PlanningMeetings.UpdatePlanningMeeeting
{
    public class UpdatePlanningMeetingHandler : IRequestHandler<UpdatePlanningMeetingCommand>
    {
        private readonly AskContext _askContext;

        public UpdatePlanningMeetingHandler(AskContext askContext)
        {
            _askContext = askContext;
        }

        public async Task<Unit> Handle(UpdatePlanningMeetingCommand request, CancellationToken cancellationToken)
        {
            await _askContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
