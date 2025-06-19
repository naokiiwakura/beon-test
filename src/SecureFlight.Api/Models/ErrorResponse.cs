using SecureFlight.Core;

namespace SecureFlight.Api.Models;

public class ErrorResponse
{
    public required Error Error { get; set; }
}