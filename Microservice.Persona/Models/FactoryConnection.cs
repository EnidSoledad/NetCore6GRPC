using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Microservice.Persona.Models
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;
        private readonly IOptions<ConexionConfiguracion> _config;

        //Constructor para inyectar 
        public FactoryConnection(IOptions<ConexionConfiguracion> config)
        {
            _config = config;
        }
        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            //Si no está creada, se crea..
            if (_connection == null ) {
                _connection = new OracleConnection(_config.Value.DefaultConnection);
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            return _connection;

        }
    }
}
