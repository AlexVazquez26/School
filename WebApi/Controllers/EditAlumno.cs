using AccesoDatos.Models;
using AccesoDatos.Operations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class EditAlumno : ControllerBase
    {
        [HttpGet("EditAlumnoPage")]
        public Alumno GetAlumno(int id) => new AlumnoDAO().GetAlumnoById(id);

    }
}
