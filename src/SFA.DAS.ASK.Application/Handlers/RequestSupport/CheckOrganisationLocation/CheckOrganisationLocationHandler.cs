using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ASK.Data;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.CheckOrganisationLocation
{
    public class CheckOrganisationLocationHandler : IRequestHandler<CheckOrganisationLocationRequest, bool>
    {
        private readonly AskContext _dbContext;

        public CheckOrganisationLocationHandler(AskContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(CheckOrganisationLocationRequest request, CancellationToken cancellationToken)
        {
            
        }
    }

    public class CheckOrganisationLocationRequest : IRequest<bool>
    {
        public CheckOrganisationLocationRequest(string postcode)
        {
            
        }
    }
}