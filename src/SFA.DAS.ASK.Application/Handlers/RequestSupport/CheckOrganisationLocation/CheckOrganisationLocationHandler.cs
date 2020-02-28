using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var invalidPostcodeRegions = await _dbContext.PostcodeRegions.Where(pcr => pcr.DeliveryAreaId == 0).Select(pcr=>pcr.PostcodePrefix).ToListAsync();
            return !invalidPostcodeRegions.Contains(Regex.Replace(request.Postcode.ToUpper(), @"(\p{L}+).*", "$1"));
        }
    }
}