using AccesoDatos.Models;
using Microsoft.Identity.Client;

namespace AccesoDatos
{
    public class AlumnoDAO
    {
        private AppRegistryContext appContext = new AppRegistryContext();

        public List<Alumno> SelectAll() => appContext.Alumnos.ToList();
        public Alumno SelectId(int id) => appContext.Alumnos.Where(a => a.Id == id).FirstOrDefault()!;

        public bool InsertAlumno(string dni, string nombre, string direccion, int edad, string email)
        {
            try
            {
                var alumno = new Alumno
                {
                    Dni = dni,
                    Nombre = nombre,
                    Direccion = direccion,
                    Edad = edad,
                    Email = email
                };

                appContext.Add(alumno);
                appContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't insert values on the table");
                return false;
            }
        }

        public bool UpdateAlumno(int id, string dni, string nombre, string direccion, int edad, string email)
        {
            try
            {

                var alumno = SelectId(id);
                if (alumno == null)
                {

                    return false;
                }

                alumno.Dni = dni;
                alumno.Nombre = nombre;
                alumno.Direccion = direccion;
                alumno.Edad = edad;
                alumno.Email = email;

                appContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't Update Value");
                return false;
            }
        }
    }
}
