using AccesoDatos;
using AccesoDatos.Models;

namespace ConsoleApp
{
    internal class Program
    {
        public static AlumnoDAO opAlumno = new AlumnoDAO();

        static void Main(string[] args)
        {
            GiveNames();

        }

        public static void GiveNames()
        {

            var cont = 0;
            foreach (var alumno in opAlumno.SelectAll())
            {
                Console.WriteLine($"Id es {alumno.Id}, el nombre es: {alumno.Nombre}");
                cont++;
            }

            Console.WriteLine($"Registros: {cont}");

            Console.WriteLine(new string('-',50));

            Console.WriteLine(opAlumno.SelectId(1).Nombre);

            Console.WriteLine(new string('-', 50));

            //opAlumno.InsertAlumno("48456T", "Alejandro Vazquez", "Sendas residencial", 25, "avla@gmail.com"); //Insert new value


            opAlumno.UpdateAlumno(11, "48456T", "Mario Casa", "Sendas residencial", 25, "avla@gmail.com");

            foreach (var alumno in opAlumno.SelectAll())
            {
                Console.WriteLine($"El nombre es: {alumno.Nombre}");
            }

            Console.WriteLine("Showing new tables");
        }


    }
}
