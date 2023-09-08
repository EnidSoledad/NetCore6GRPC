using Grpc.Core;
using Microservice.Persona.Models;
using Microservice.Persona.Protos;
using System.Threading.Tasks;
using static Microservice.Persona.Protos.PersonaProtoService;

namespace Microservice.Persona.Services
{
    public class PersonaService: PersonaProtoServiceBase
    {
        //Inyectar el repositorio
        private readonly IPersona _personaRepository;
        public PersonaService(IPersona personaRepository)
        {
            _personaRepository = personaRepository;
        }
        public override async Task<personaResponse> GetPersona(personaRequest request, ServerCallContext context)
        {
            var persona = await _personaRepository.GetPersonaByEmail(request.Email);

            var personaModel = new PersonaModel
            {
                PersonaId = persona.PersonaId,
                Nombre = persona.Nombre,
                Direccion = persona.Direccion,
                Telefono = persona.Telefono,
                Email = persona.Email
            };

            var personaResponse = new personaResponse
            {
                Persona = personaModel
            };

            return personaResponse;
        }
    }
}
