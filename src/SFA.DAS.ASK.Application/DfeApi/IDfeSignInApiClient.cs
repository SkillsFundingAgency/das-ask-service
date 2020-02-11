using System;
using System.Collections.Generic;

namespace SFA.DAS.ASK.Application.DfeApi
{
    public interface IDfeSignInApiClient
    {
        List<DfeOrganisation> GetOrganisations(Guid dfeSignInId);
    }
}