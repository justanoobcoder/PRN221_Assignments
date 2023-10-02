using Microsoft.AspNetCore.Mvc.Filters;
using NguyenHongHiepRazorPages.Constants;
using NguyenHongHiepRazorPages.Models;
using NguyenHongHiepRazorPages.Utils;
using System.Diagnostics;

namespace NguyenHongHiepRazorPages.Filters;

public class AuthorizationFilter : IPageFilter
{
    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {

        var url = context.HttpContext.Request.Path.ToString();
        if (url == "/" || url == "/Register")
            return;

        var currentUser = context.HttpContext.Session.GetObjectFromJson<CurrentUser>(Contants.CurrentUserKey);
        

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
