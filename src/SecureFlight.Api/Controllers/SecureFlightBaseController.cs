using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;
using SecureFlight.Api.Utils;
using SecureFlight.Core;

namespace SecureFlight.Api.Controllers;

public class SecureFlightBaseController(IMapper mapper) : ControllerBase
{
    protected IActionResult MapResultToDataTransferObject<TResult, TDataTransferObject>(OperationResult<TResult> result)
    {
        if (!result.Succeeded)
        {
            return new ErrorResponseActionResult
            {
                Result = new ErrorResponse
                {
                    Error = new Error
                    {
                        Code = result.ErrorResult.Code,
                        Message = result.ErrorResult.Message
                    }
                }
            };
        }

        return Ok(mapper.Map<TResult, TDataTransferObject>(result));
    }
}