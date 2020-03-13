using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SFA.DAS.ASK.Web.Infrastructure.TagHelpers
{
    [HtmlTargetElement(Attributes = "show-conditional-if-model-value,show-conditional-if-model")]
    public class ShowConditionalIfModelValueTagHelper : TagHelper
    {
        public const string ValidationForAttributeName = "show-conditional-if-model";

        [HtmlAttributeName(ValidationForAttributeName)]
        public ModelExpression For { get; set; }
        
        public const string ModelValueAttributeName = "show-conditional-if-model-value";

        [HtmlAttributeName(ModelValueAttributeName)]
        public string Value { get; set; }
 
        public override void Process(TagHelperContext context, TagHelperOutput output) 
        {
            if (For.Model == null || For.Model.ToString() != Value) return;
            output.RemoveClass("govuk-radios__conditional--hidden", HtmlEncoder.Default);
            output.RemoveClass("govuk-checkboxes__conditional--hidden", HtmlEncoder.Default);
        }
    }
}