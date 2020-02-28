using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.StartTempSupportRequest
{
    public class StartTempSupportRequestCommand : IRequest<StartTempSupportRequestResponse>
    {

        public StartTempSupportRequestCommand()
        {
           
        }
    }
}