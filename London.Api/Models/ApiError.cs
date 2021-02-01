﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace London.Api.Models
{
  public class ApiError
  {
    public ApiError()
    {
    }

    public ApiError(ModelStateDictionary modelState)
    {
      Message = "Invalid parameters.";
      Detail = modelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors
        .FirstOrDefault().ErrorMessage;
    }

    public string Message { get; set; }
    public string Detail { get; set; }
  }
}
