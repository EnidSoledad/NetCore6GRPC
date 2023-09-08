using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservice.Persona.Models
{
    public interface IPersona
    {
        Task<IEnumerable<Persona>> GetAllPersona();
        Task<Persona> GetPersonaById(int id);
        Task<int> AddPersona(string nombre, string apellido, string direccion, string telefono, string email);
        Task<int> UpdatePersona(int id,string nombre, string apellido, string direccion, string telefono, string email);
        Task<int> DeletePersonaById(int id);
        Task<Persona> GetPersonaByEmail(string email);
    }
}
