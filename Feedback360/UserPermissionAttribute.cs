using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Feedback360
{
    public class UserPermissionAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if AuthorizationFilterContext is null or not.
            if (context != null)
            {
                var descriptor = context?.ActionDescriptor as ControllerActionDescriptor;
                // Gets ControllerName.
                var ctrorlerlName = descriptor.ControllerName; // Example UserController
                // Gets ActionName.
                var actionName = descriptor.ActionName; // Example GetLogin

                // Check if not Login page then eecute the code.
                if (actionName.ToLower() != "getlogin")
                {
                    // Check if Session is null, if null redirect to Login page.
                    if (context.HttpContext.Session.GetString("Curr") == null)
                    {
                        // Sets the Login page to be redirected.
                        context.Result = new RedirectResult("/User/GetLogin");
                    }
                }
            }
        }
    }
}
