using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.Services.DfeApi
{
    public interface IDfeSignInApiClient
    {
        Task<List<DfeOrganisation>> GetOrganisations(Guid dfeSignInId);
    }
}