using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest
{
    public class StartTempSupportRequestCommand : IRequest<StartTempSupportRequestResponse>
    {
        public SupportRequestType Type { get; }

        public StartTempSupportRequestCommand(SupportRequestType type)
        {
            Type = type;
        }
    }
}