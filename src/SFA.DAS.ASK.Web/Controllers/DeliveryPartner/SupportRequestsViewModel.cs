using System.Collections.Generic;
using System.Linq;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.GetSupportRequests;

namespace SFA.DAS.ASK.Web.Controllers.DeliveryPartner
{
    public class SupportRequestsViewModel
    {
        public SortBy SortBy { get; }

        public SupportRequestsViewModel(GetSupportRequestsResponse supportRequestResponse, SortBy sortBy)
        {
            SortBy = sortBy;
            NewSupportRequests = supportRequestResponse.NewSupportRequests.Select(sr => new SupportRequestViewModel(sr)).ToList();
            ContactedSupportRequests = supportRequestResponse.ContactedSupportRequests.Select(sr => new SupportRequestViewModel(sr)).ToList();
            RejectedSupportRequests = supportRequestResponse.RejectedSupportRequests.Select(sr => new SupportRequestViewModel(sr)).ToList();
            
            NewSupportRequests = Sort(NewSupportRequests);
            ContactedSupportRequests = Sort(ContactedSupportRequests);
        }

        private List<SupportRequestViewModel> Sort(List<SupportRequestViewModel> supportRequestViewModels)
        {
            var sorted = supportRequestViewModels;
            if (SortBy == SortBy.RecentlyPublished)
            {
                sorted = supportRequestViewModels.OrderBy(sr => sr.StatusDate).ToList();   
            }

            return sorted;
        }

        public List<SupportRequestViewModel> NewSupportRequests { get; set; }
        public List<SupportRequestViewModel> ContactedSupportRequests { get; set; }
        public List<SupportRequestViewModel> RejectedSupportRequests { get; set; }

        public int NewSupportRequestsCount => NewSupportRequests.Count;
        public int ContactedSupportRequestsCount => ContactedSupportRequests.Count;
        public int AllSupportRequestsCount => NewSupportRequestsCount + ContactedSupportRequestsCount;
        public int RejectedSupportRequestsCount => RejectedSupportRequests.Count;
    }
}