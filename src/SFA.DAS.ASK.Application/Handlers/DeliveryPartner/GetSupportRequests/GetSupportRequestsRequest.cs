using System.Collections.Generic;
using MediatR;
using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Application.Handlers.DeliveryPartner.GetSupportRequests
{
    public class GetSupportRequestsRequest : IRequest<GetSupportRequestsResponse>
    {
        public SortBy? SortBy { get; }

        public GetSupportRequestsRequest(SortBy? sortBy = DeliveryPartner.GetSupportRequests.SortBy.RecentlyPublished)
        {
            SortBy = sortBy;
        }
    }

    public class GetSupportRequestsResponse
    {
        public List<SupportRequest> NewSupportRequests { get; set; }
        public List<SupportRequest> ContactedSupportRequests { get; set; }
        public List<SupportRequest> RejectedSupportRequests { get; set; }
    }
}