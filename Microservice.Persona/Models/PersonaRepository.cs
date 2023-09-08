using Dapper;
using Microservice.Persona.Helper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Microservice.Persona.Models
{
    public class PersonaRepository : IPersona
    {

        private readonly IFactoryConnection _factoryConnection;
        public PersonaRepository(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }

        public Task<int> AddPersona(string nombre, string apellido, string direccion, string telefono, string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeletePersonaById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Persona>> GetAllPersona()
        {
            var storeProcedure = "USP_GETPERSONAS";
            IEnumerable<Persona> personaList = null;

            try
            {
                //El parámetro de salida
                var pms = new OracleDynamicParameters();
                pms.Add("PERSONACURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                var connection = _factoryConnection.GetConnection();
                personaList = await connection.QueryAsync<Persona>(storeProcedure, pms, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw new Exception($"Error en {storeProcedure}");
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }

            return personaList;
        }

        public async Task<Persona> GetPersonaByEmail(string email)
        {
            var storeProcedure = "USP_GETPERSONA_EMAIL";
            Persona persona;

            try
            {
                var pms = new OracleDynamicParameters();
                //Parámetro de entrada
                pms.Add("EMAILPER", OracleDbType.Varchar2, ParameterDirection.Input, email);
                //Parámetro de salida
                pms.Add("PERSONACURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                var connection = _factoryConnection.GetConnection();
                persona = await connection.QueryFirstAsync<Persona>(storeProcedure, pms, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw new Exception($"Error en {storeProcedure}");
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }

            return persona;
        }

        public async Task<Persona> GetPersonaById(int id)
        {
            var storeProcedure = "USP_GETPERSONA";
            Persona persona;

            try
            {
                var pms = new OracleDynamicParameters();
                //Parámetro de entrada
                pms.Add("ID", OracleDbType.Int32, ParameterDirection.Input, id);
                //Parámetro de salida
                pms.Add("PERSONACURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                var connection = _factoryConnection.GetConnection();
                persona = await connection.QueryFirstAsync<Persona>(storeProcedure, pms, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw new Exception($"Error en {storeProcedure}");
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }

            return persona;
        }

        public Task<int> UpdatePersona(int id, string nombre, string apellido, string direccion, string telefono, string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
