using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.SubmitSupportRequest
{
    public class SubmitSupportRequest : IRequest
    {
        public SupportRequest SupportRequest { get; }
        public string Email { get; set; }

        public SubmitSupportRequest(SupportRequest supportRequest, string email)
        {
            SupportRequest = supportRequest;
            Email = email;
        }
    }
}