using SFA.DAS.ASK.Data.Entities;

namespace SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels
{
    public class FeedbackRatingRadioViewModel
    {
        public FeedbackRatingRadioViewModel()
        {
            
        }
        public FeedbackRatingRadioViewModel(FeedbackRating? rating, string title, string id)
        {
            Rating = rating;
            Title = title;
            Id = id;
        }

        public FeedbackRating? Rating { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
    }
}