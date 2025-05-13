using Microsoft.AspNetCore.Mvc;
using TalkLens.Auth.Core.DTOs.Token;
using TalkLens.Auth.Core.Interfaces;

namespace TalkLens.Auth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly IJwtService _jwtService;

    public TokenController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("validate")]
    public ActionResult<ValidateTokenResponse> ValidateToken([FromHeader(Name = "Authorization")] string? authorization)
    {
        if (string.IsNullOrEmpty(authorization) || !authorization.StartsWith("Bearer "))
        {
            return BadRequest(new ValidateTokenResponse 
            { 
                IsValid = false, 
                Message = "Invalid token format" 
            });
        }

        var token = authorization.Substring("Bearer ".Length);
        var userId = _jwtService.ValidateToken(token);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new ValidateTokenResponse 
            { 
                IsValid = false, 
                Message = "Invalid token" 
            });
        }

        return Ok(new ValidateTokenResponse 
        { 
            IsValid = true, 
            UserId = userId,
            Message = "Token is valid"
        });
    }
} 