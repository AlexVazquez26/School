using AccesoDatos.Models;
using AccesoDatos.Operations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        public ProfesorDAO profesorDAO = new ProfesorDAO();
        [HttpPost("autentication")]

        public IActionResult Login([FromBody] LoginRequest req)
        {
            var profesor = profesorDAO.login(req.user, req.pass);
            if (profesor == null)
                return Unauthorized("User invalid");
            return Ok(profesor.Usuario);

        }


    }
}
