using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.SaveSupportRequest
{
    public class SaveSupportRequest : IRequest
    {
        public SupportRequest SupportRequest { get; }

        public SaveSupportRequest(SupportRequest supportRequest)
        {
            SupportRequest = supportRequest;
        }
    }
}