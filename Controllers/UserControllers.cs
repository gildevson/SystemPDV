using SistemaCaixa.Data;
using SistemaCaixa.Models;
using Microsoft.AspNetCore.Mvc;
using SistemaCaixa.Data;
using SistemaCaixa.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DbContextDapper _dapper;

    public UserController(DbContextDapper dapper)
    {
        _dapper = dapper;
    }

    [HttpPost("login")]
    public IActionResult Login(Usuario user)
    {
        return Ok(new { mensagem = "Login OK!" });
    }
}