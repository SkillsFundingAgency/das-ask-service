using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.SubmitSupportRequest
{
    public class SubmitSupportRequest : IRequest
    {
        public SupportRequest SupportRequest { get; }

        public SubmitSupportRequest(SupportRequest supportRequest)
        {
            SupportRequest = supportRequest;
        }
    }
}