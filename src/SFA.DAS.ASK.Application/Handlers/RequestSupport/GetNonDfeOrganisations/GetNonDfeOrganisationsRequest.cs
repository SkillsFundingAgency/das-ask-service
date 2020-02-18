using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SFA.DAS.ASK.Application.DfeApi;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations
{
    public class GetNonDfeOrganisationsRequest : IRequest<List<NonDfeOrganisation>>
    {
        public GetNonDfeOrganisationsRequest()
        {

        }

    }
}
