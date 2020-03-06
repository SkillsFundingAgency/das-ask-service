using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SFA.DAS.ASK.Web.Infrastructure.TagHelpers
{
    [HtmlTargetElement(Attributes = "sfa-gds-show-conditional-if-error-for")]
    public class ShowConditionalIfErrorForTagHelper : TagHelper
    {
        public const string ValidationForAttributeName = "sfa-gds-show-conditional-if-error-for";

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
            
            output.RemoveClass("govuk-radios__conditional--hidden", HtmlEncoder.Default);
            output.RemoveClass("govuk-checkboxes__conditional--hidden", HtmlEncoder.Default);
        }
    }
}