using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace APIPortal.FilterAttributes
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class RequestRateLimitAttribute : ActionFilterAttribute
  {
    public string Name { get; set; }
    public int MaximumRequests { get; set; }
    public int Duration { get; set; }
    private static MemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());

    public override void OnActionExecuting(ActionExecutingContext context)
    {
      var ipAddress = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;

      var memoryCacheKey = string.Format("{0}-{1}", Name, ipAddress);


      if (Cache.TryGetValue(memoryCacheKey, out int numberOfRequests) && numberOfRequests > MaximumRequests)
      {
        numberOfRequests += 1;

        var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(Duration));

        Cache.Set(memoryCacheKey, numberOfRequests, cacheEntryOptions);

        context.Result = new ContentResult
        {
          Content = "Requests are limited."
        };

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
      }
      else
      {
        numberOfRequests += 1;

        var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(5));

        Cache.Set(memoryCacheKey, numberOfRequests, cacheEntryOptions);
      }
    }
  }
}
