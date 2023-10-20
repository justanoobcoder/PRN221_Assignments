using Microsoft.AspNetCore.Mvc.Filters;
using NguyenHongHiepSignalR.Constants;
using NguyenHongHiepSignalR.Models;
using NguyenHongHiepSignalR.Utils;
using System.Diagnostics;

namespace NguyenHongHiepSignalR.Filters;

public class AuthorizationFilter : IPageFilter
{
    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {

        var url = context.HttpContext.Request.Path.ToString();
        if (url == "/" || url == "/Register")
            return;

        var currentUser = context.HttpContext.Session.GetObjectFromJson<CurrentUser>(SessionKey.CurrentUserKey);
        

        if (currentUser == null)
            context.HttpContext.Response.Redirect("/");
        else if (!currentUser.IsAdmin && (url.ToLower().StartsWith("/admin") || url.ToLower().StartsWith("/admin/")))
            context.HttpContext.Response.Redirect("/ForbidenError");
        else if (currentUser.IsAdmin && (url.ToLower().StartsWith("/member") || url.ToLower().StartsWith("/member/")))
            context.HttpContext.Response.Redirect("/ForbidenError");
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        
    }
}
