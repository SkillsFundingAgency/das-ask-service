using System;
using MediatR;
using SFA.DAS.ASK.Application.DfeApi;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.AddNonDfeSignInInformation
{
    public class AddNonDfESignInInformationCommand : IRequest<TempSupportRequest>
    {
        public Guid RequestId { get; set; }
        public ReferenceDataSearchResult Organisation { get; set; }

        public AddNonDfESignInInformationCommand(ReferenceDataSearchResult organsistion, Guid requestId)
        {
            Organisation = organsistion;
            RequestId = requestId;
        }
    }
}