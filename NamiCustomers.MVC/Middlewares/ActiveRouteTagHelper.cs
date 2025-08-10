using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NamiCustomers.MVC.Middlewares
{
	[HtmlTargetElement(Attributes = "asp-controller, asp-action")]
	public class ActiveRouteTagHelper : TagHelper
	{
		[HtmlAttributeName("asp-controller")]
		public string Controller { get; set; }

		[HtmlAttributeName("asp-action")]
		public string Action { get; set; }

		[ViewContext]
		public ViewContext ViewContext { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var currentController = ViewContext.RouteData.Values["Controller"].ToString();
			var currentAction = ViewContext.RouteData.Values["Action"].ToString();

			if (Controller == currentController && Action == currentAction)
			{
				output.Attributes.SetAttribute("class", "active");
			}
		}
	}
}
