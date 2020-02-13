using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Data;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.AddDfeSignInUserInformation
{
    public class AddDfeSignInUserInformationHandler : IRequestHandler<AddDfeSignInUserInformationCommand>
    {
        private readonly AskContext _context;

        public AddDfeSignInUserInformationHandler(AskContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(AddDfeSignInUserInformationCommand request, CancellationToken cancellationToken)
        {
            var tempSupportRequest = _context.TempSupportRequests.Single(tsr => tsr.Id == request.RequestId);

            tempSupportRequest.Agree = true;
            tempSupportRequest.Email = request.Email;
            tempSupportRequest.FirstName = request.Name.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)[0];
            tempSupportRequest.LastName = request.Name.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)[1];
            tempSupportRequest.SupportRequestType = SupportRequestType.Manual;
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}