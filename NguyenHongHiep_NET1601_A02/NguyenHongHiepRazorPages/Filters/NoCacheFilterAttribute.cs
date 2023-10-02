using Microsoft.AspNetCore.Mvc.Filters;

namespace NguyenHongHiepRazorPages.Filters;

public class NoCacheFilterAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
        context.HttpContext.Response.Headers.Add("Pragma", "no-cache");
        context.HttpContext.Response.Headers.Add("Expires", "-1000");
        base.OnResultExecuting(context);
    }
}
