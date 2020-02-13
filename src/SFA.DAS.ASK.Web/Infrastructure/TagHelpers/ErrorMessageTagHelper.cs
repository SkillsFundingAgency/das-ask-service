using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SFA.DAS.ASK.Web.Infrastructure.TagHelpers
{
    [HtmlTargetElement("span", Attributes = "sfa-gds-error-message-for")]
    public class ErrorMessageTagHelper : TagHelper
    {
        public const string ValidationForAttributeName = "sfa-gds-error-message-for";

        [HtmlAttributeName(ValidationForAttributeName)]
        public ModelExpression For { get; set; }
        
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ModelStateEntry entry;
            ViewContext.ViewData.ModelState.TryGetValue(For.Name, out entry);
            if (entry == null || !entry.Errors.Any()) return;
            
            output.Attributes.RemoveAll(ValidationForAttributeName);
            output.AddClass("govuk-error-message", HtmlEncoder.Default);

            var errorMessage = entry.Errors.First().ErrorMessage;
            
            output.Content.AppendHtml($"<span class=\"govuk-visually-hidden\">Error:</span> {errorMessage}");

        }
    }
}