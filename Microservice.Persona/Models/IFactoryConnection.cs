using System.Data;

namespace Microservice.Persona.Models
{
    public interface IFactoryConnection
    {
        void CloseConnection();
        IDbConnection GetConnection();
    }
}
