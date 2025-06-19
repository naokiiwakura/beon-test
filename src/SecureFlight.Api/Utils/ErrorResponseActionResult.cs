using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;

namespace SecureFlight.Api.Utils;

public class ErrorResponseActionResult : ActionResult
{
    public required ErrorResponse Result { get; set; }
}