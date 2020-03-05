using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Application.Handlers.Feedback.SaveVisitFeedback;
using SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    public abstract class FeedbackControllerBase<TViewModel> : Controller where TViewModel : IFeedbackViewModel, new()
    {
        protected string ViewName { get; set; }
        protected string NextPageController { get; set; }
        private readonly IMediator _mediator;

        protected FeedbackControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{feedbackId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid feedbackId)
        {
            var feedback = await _mediator.Send(new GetVisitFeedbackRequest(feedbackId, false));

            var viewModel = new TViewModel();
            viewModel.Load(feedbackId, feedback);
            
            return View(ViewName, viewModel);
        }

        [HttpPost("{feedbackId}")]
        [ExportModelState]
        public async Task<IActionResult> Index(Guid feedbackId, TViewModel viewModel)
        {
            if (!ModelState.IsValid || !viewModel.ExecuteCustomValidation(ModelState))
            {
                return RedirectToAction("Index", new {feedbackId});
            }

            var feedback = await _mediator.Send(new GetVisitFeedbackRequest(feedbackId, false));

            var feedbackAnswers = feedback.FeedbackAnswers.CloneJson();
            
            var updatedAnswers = viewModel.ToFeedbackAnswers(feedbackAnswers);
            
            await _mediator.Send(new SaveVisitFeedbackRequest(feedbackId, updatedAnswers));

            return RedirectToAction("Index", NextPageController, new { feedbackId });
        }
    }
}