using FinanblueBackend.Data;
using FinanblueBackend.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
    private readonly DbContextDapper _dapper;

    public UserController(DbContextDapper dapper) {
        _dapper = dapper;
    }

    [HttpPost("login")]
    public IActionResult Login(Usuario user) {
        return Ok(new { mensagem = "Login OK!" });
    }
}
