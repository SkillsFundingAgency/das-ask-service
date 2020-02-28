using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Application.Services.ReferenceData;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetNonDfeOrganisations
{
    public class GetOrganisationsRequest : IRequest<IEnumerable<ReferenceDataSearchResult>>
    {
        public string SearchTerm { get; set; }
        public Guid RequestId { get; set; }

        public GetOrganisationsRequest(string search, Guid requestId)
        {
            SearchTerm = search;
            RequestId = requestId;
        }

    }
}
