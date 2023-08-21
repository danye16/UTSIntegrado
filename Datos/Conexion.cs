using System.Data.SqlClient;
namespace UTS.Datos
{
    public class Conexion
    {

        private string AulasUTSContext = string.Empty;
        public Conexion() 
        {
            var builder = new ConfigurationBuilder().SetBasePath(
                Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            AulasUTSContext = builder.GetSection("ConnectionStrings:cadenaSql").Value;
        }

        public string getAulasUTSContext()
        {
            return AulasUTSContext;
        }

    }
}
