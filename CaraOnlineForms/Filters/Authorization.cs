using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaraOnlineForms.Filters
{
    public class CaraAuthorizeAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Session session = new Session();
            return session.User != null;
        }
    

        //Handle authorization failure. We will not simply redirect to login, as some requests may be AJAX requests, and we need to provide appropriate output from our authorization attribute.
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
       
        if (context.HttpContext.Request.IsAjaxRequest())
            {
                context.HttpContext.Response.StatusCode = 500;
                ContentResult cResult = new ContentResult();
                cResult.Content = string.Format("Please {0} again to continue",
                                                HtmlHelper.GenerateLink(context.RequestContext,
                                                                      System.Web.Routing.RouteTable.Routes,
                                                                      "login",
                                                                      "Default",
                                                                      "Logon",
                                                                      "Account",
                                                                      null,
                                                                      null));
                                 
                context.Result = cResult;

            }
            else
            {


                context.Result = new RedirectResult("/Account/Logon");
            
            }
        } //-- end  HandleUnauthorizedRequest --

    }
}