using AccesoDatos.Joins;
using AccesoDatos.Models;
using AccesoDatos.Operations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {

        private AlumnoDAO alumnoDAO = new AlumnoDAO();


        [HttpGet("alumnosProfesor")]
        public List<AlumnoProfesor> AlumnosProfesor(string usuario) => alumnoDAO.JoinAlumnoProfesors(usuario);

        [HttpPut("alumno")]
        //datos viajan por el body no es viable hacerlo atraves de la URL podria ser muy largo o inseguro 
        public IActionResult UpdateAlumno([FromBody] Alumno alumno)
        {
            //permite que el resultado sea null
            bool? result = alumnoDAO.UpdateAlumno(alumno.Id, alumno.Dni, alumno.Nombre, alumno.Direccion, alumno.Edad, alumno.Email); 
            return result switch
            {
                null => NotFound("Alumno no encontrado"),
                false => NoContent(),
                true=> Ok("Register Updated")
            };
        }

        [HttpPost("alumno")]
        public bool InsertNewGest([FromBody] Alumno alumno, int id_asig) => alumnoDAO.AddAndMatriculate(alumno.Dni,
            alumno.Nombre, alumno.Direccion, alumno.Edad, alumno.Email, id_asig);

    }
}
