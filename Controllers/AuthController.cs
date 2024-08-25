using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("token")]
    public IActionResult GenerateToken()
    {
        // 这里应该添加用户验证逻辑
        // 假设验证成功，我们生成 token
        var token = _jwtService.GenerateToken("kent", ["admin"]);
        return Ok(new { token });
    }
}

public class TokenRequest
{
    public string UserId { get; set; }
    public string[] Roles { get; set; }
}
