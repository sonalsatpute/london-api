using London.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace London.Api.Filters
{
  public class RequireHttpsOrCloseAttribute : RequireHttpsAttribute
  {
    protected override void HandleNonHttpsRequest(AuthorizationFilterContext filterContext)
    {
      //var error = new ApiError()
      //{
      //  Message = "Non HTTPS requeste.",
      //  Detail = "server only support https requests."
      //};

      //filterContext.Result = new ObjectResult(error) { StatusCode = 400 };

      filterContext.Result = new StatusCodeResult(400);
    }
  }
}
