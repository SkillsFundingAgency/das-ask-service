using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ASK.Application.DfeApi
{
    public interface INonDfeSignInApiClient
    {
        List<NonDfeOrganisation> GetOrganisations();
    }
}
