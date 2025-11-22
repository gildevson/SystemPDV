using Microsoft.AspNetCore.Mvc;
using SistemaCaixa.Data;

namespace SistemaCaixa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var usuarios = _repo.GetAllUsers();
            return Ok(usuarios);
        }
    }
}
//comentando