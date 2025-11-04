using AccesoDatos.Models;
using AccesoDatos.Operations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CalificacionController : ControllerBase
    {
        private CalificacionDAO calificacionDAO = new CalificacionDAO();

        [HttpGet("calificaciones")]
        public List<Calificacion> GetCalificaciones(int id) => calificacionDAO.SelectCalificacions(id)!;

        [HttpPost("calificaciones")]
        public bool AddCalificacion([FromBody] Calificacion calificacion) => calificacionDAO.CreateCalif(calificacion);
        [HttpDelete("calificaciones")]
        public bool DeleteCalif (int id) => calificacionDAO.DeleteCalif(id);

    }
}
