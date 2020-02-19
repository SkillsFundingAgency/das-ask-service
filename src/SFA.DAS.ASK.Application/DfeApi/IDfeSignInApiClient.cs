using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFA.DAS.ASK.Application.DfeApi
{
    public interface IDfeSignInApiClient
    {
        Task<List<DfeOrganisation>> GetOrganisations(Guid dfeSignInId);
    }
}