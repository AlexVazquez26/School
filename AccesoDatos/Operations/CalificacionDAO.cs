using AccesoDatos.Models;

namespace AccesoDatos.Operations
{
    public class CalificacionDAO
    {
        public List<Calificacion>? SelectCalificacions(int id)
        {
            try
            {
                using var appContext = new AppRegistryContext();
                {
                    var calificaciones = appContext.Calificacions.Where(c => c.MatriculaId == id).ToList();
                    return calificaciones;
                }
            }
            catch (Exception e)
            {
                return null;
            }


        }
        public bool CreateCalif(Calificacion calif)
        {
            try
            {
                using var appContext = new AppRegistryContext();
                {
                    appContext.Calificacions.Add(calif);
                }

                appContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;

            }
        }

        public bool DeleteCalif(int id)
        {
            try
            {
                using var appContext = new AppRegistryContext();
                {
                    var calif = appContext.Calificacions.FirstOrDefault(c => c.Id == id);
                    if (calif == null)
                        return false;

                    appContext.Calificacions.Remove(calif);
                    appContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
