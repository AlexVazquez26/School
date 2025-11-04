using AccesoDatos.Joins;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Operations
{
    public class AlumnoDAO
    {
        public List<Alumno> SelectAll()
        {
            using var appContext = new AppRegistryContext();
            return appContext.Alumnos.ToList();
        }
        public Alumno SelectId(int id)
        {
            using var appContext = new AppRegistryContext();
            return appContext.Alumnos.FirstOrDefault(a => a.Id == id)!;
        }
        public bool InsertAlumno(string dni, string nombre, string direccion, int edad, string email)
        {
            try
            {
                using var appContext = new AppRegistryContext();
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
        public bool? UpdateAlumno(int id, string dni, string nombre, string direccion, int edad, string email)
        {
            try
            {
                using var ctx = new AppRegistryContext();

                var alumno = ctx.Alumnos.Where(a => a.Id == id).FirstOrDefault()!;
                if (alumno is null) return null;

                var sinCambios =
                    alumno.Dni == dni &&
                    alumno.Nombre == nombre &&
                    alumno.Direccion == direccion &&
                    alumno.Edad == edad &&
                    alumno.Email == email;

                if (sinCambios == null)
                    return false;


                alumno.Dni = dni;
                alumno.Nombre = nombre;
                alumno.Direccion = direccion;
                alumno.Edad = edad;
                alumno.Email = email;

                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't Update Value: {ex.Message}");
                return false;
            }
        }
        public bool DeleteAlumno(int id)
        {
            try
            {
                using var ctx = new AppRegistryContext();

                var alumno = ctx.Alumnos.Find(id); // usa la PK, primero cache, luego SQL
                if (alumno is null) return false;

                ctx.Remove(alumno);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't Delete");
                return false;
            }
        }
        public List<Profesor> SelectListProfesors()
        {
            using var appContext = new AppRegistryContext();
            return appContext.Profesors.ToList();

        }
        public List<AlumnosAsignatura> SelectAlumnosAsignaturasList()
        {
            using var appContext = new AppRegistryContext();
            var query = from a in appContext.Alumnos
                        join m in appContext.Matriculas on a.Id equals m.AlumnoId
                        join asig in appContext.Asignaturas on m.AsignaturaId equals asig.Id
                        select new AlumnosAsignatura
                        {
                            NombreAlumno = a.Nombre,
                            NombreAsignatura = asig.Nombre
                        };
            return query.ToList();
        }

        public List<AlumnoProfesor> JoinAlumnoProfesors(string user)
        {
            using var appcontext = new AppRegistryContext();
            var query = from a in appcontext.Alumnos
                        join m in appcontext.Matriculas on a.Id equals m.AlumnoId
                        join asig in appcontext.Asignaturas on m.AsignaturaId equals asig.Id
                        where asig.Profesor == user
                        select new AlumnoProfesor()
                        {
                            Id = a.Id,
                            Dni = a.Dni,
                            Name = a.Nombre,
                            Edad = a.Edad,
                            Email = a.Email,
                            Direccion = a.Direccion,
                            Asignatura = asig.Nombre

                        };
            return query.ToList();

        }

        public Alumno GetAlumnoById(int id)
        {
            using var appContext = new AppRegistryContext();
            return appContext.Alumnos.FirstOrDefault(a => a.Id == id)!;
        }

        public Alumno? SelectByDni(string dni)
        {
            using var appContext = new AppRegistryContext();
            return appContext.Alumnos.FirstOrDefault(a => a.Dni == dni);
        }

        public bool AddAndMatriculate(string dni, string nombre, string direccion, int edad, string email, int id_asig)
        {
            try
            {
                var exist = SelectByDni(dni);
                if (exist == null) //Sinigica que no existe
                {
                    InsertAlumno(dni, nombre, direccion, edad, email);
                    var inserted = SelectByDni(dni);

                    Matricula m = new Matricula();
                    m.AlumnoId = inserted.Id;
                    m.AsignaturaId = id_asig;
                    using var appContext = new AppRegistryContext();
                    appContext.Matriculas.Add(m);
                    appContext.SaveChanges();


                }
                else
                {
                    var m = new Matricula();
                    m.AlumnoId = exist.Id;
                    m.AsignaturaId = id_asig;
                    using var appContext = new AppRegistryContext();
                    appContext.Matriculas.Add(m);
                    appContext.SaveChanges();

                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteAlumnoDAO(int id)
        {
            try
            {
                using var appContext = new AppRegistryContext();
                var alumno = appContext.Alumnos.FirstOrDefault(a => a.Id == id)!;
                if (alumno == null)
                    return false;
                var matriculas = appContext.Matriculas.Where(m => m.AlumnoId == id);
                foreach (Matricula m in matriculas)
                {
                    var calificaciones = appContext.Calificacions.Where(c =>c.MatriculaId == m.Id);
                    appContext.Calificacions.RemoveRange(calificaciones);
                }
                appContext.Matriculas.RemoveRange(matriculas);
                appContext.Alumnos.Remove(alumno);
                appContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}
