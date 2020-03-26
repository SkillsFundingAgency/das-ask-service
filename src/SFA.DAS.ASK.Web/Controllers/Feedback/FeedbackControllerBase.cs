using System;
using System.Security;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ASK.Application.Handlers.Feedback.GetVisitFeedback;
using SFA.DAS.ASK.Application.Handlers.Feedback.SaveVisitFeedback;
using SFA.DAS.ASK.Data.Entities;
using SFA.DAS.ASK.Web.Controllers.Feedback.ViewModels;
using SFA.DAS.ASK.Web.Infrastructure.ModelStateTransfer;

namespace SFA.DAS.ASK.Web.Controllers.Feedback
{
    public abstract class FeedbackControllerBase<TViewModel> : Controller where TViewModel : IFeedbackViewModel, new()
    {
        protected string ViewName { get; set; }
        protected string NextPageController { get; set; }
        private readonly IMediator _mediator;

        protected virtual async Task PostSubmitAction(Guid feedbackId)    
        {
        }
        
        protected FeedbackControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{feedbackId}")]
        [ImportModelState]
        public async Task<IActionResult> Index(Guid feedbackId)
        {
            var feedback = await _mediator.Send(new GetVisitFeedbackRequest(feedbackId, false));

            if (feedback is null)
            {
                throw new SecurityException($"Feedback ID {feedbackId} is not valid.");
            }
            
            if (feedback.Status == FeedbackStatus.Complete)
            {
                return RedirectToAction("Index", "FeedbackComplete", new { feedbackId });
            }

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

            await PostSubmitAction(feedbackId);
            
            return RedirectToAction("Index", NextPageController, new { feedbackId });
        }
    }
}