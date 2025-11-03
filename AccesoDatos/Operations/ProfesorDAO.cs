using AccesoDatos.Models;

namespace AccesoDatos.Operations
{
    public class ProfesorDAO
    {

        public Profesor login(string usuario, string pass)
        {
            using var appContext = new AppRegistryContext();
            var prof = appContext.Profesors.Where(p => p.Usuario == usuario && p.Pass == pass);
            return prof.FirstOrDefault();
        }
    }
}
