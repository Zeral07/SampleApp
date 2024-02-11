using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SampleApp.Helpers;
using SampleApp.ViewModels;

namespace SampleApp.Attributes
{
    public class AuthorizeValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!ServiceCoreHelper.IsAuthorize(context.HttpContext))
            {
                context.HttpContext.Session.Clear();
                context.HttpContext.Response.Redirect("/login");
            }
        }
    }
}
