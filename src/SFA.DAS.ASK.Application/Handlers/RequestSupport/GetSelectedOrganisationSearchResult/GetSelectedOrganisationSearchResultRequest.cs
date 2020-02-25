
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SFA.DAS.ASK.Application.Services.DfeApi;
using SFA.DAS.ASK.Application.Services.ReferenceData;
using SFA.DAS.ASK.Application.Services.Session;

namespace SFA.DAS.ASK.Application.Handlers.RequestSupport.GetSelectedOrganisationSearchResult
{
    public class GetSelectedOrganisationSearchResultRequest : IRequest<ReferenceDataSearchResult>
    {
       
        public Guid RequestId { get; set; }
        public Guid SelectedResult { get; set; }

        public GetSelectedOrganisationSearchResultRequest(Guid selectedResult, Guid requestId)
        {
            SelectedResult = selectedResult;
            RequestId = requestId;
        }
    }
}
