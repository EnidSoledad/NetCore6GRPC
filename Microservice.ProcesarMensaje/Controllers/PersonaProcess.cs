using Grpc.Net.Client;
using Microservice.Persona.Protos;
using Microservice.ProcesarMensaje.SendGrid;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Microservice.ProcesarMensaje.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaProcess : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISendGridEnviar _sendGridEnviar;

        public PersonaProcess(ISendGridEnviar sendGridEnviar,
                              IConfiguration configuration)
        {
            _sendGridEnviar = sendGridEnviar;
            _configuration = configuration;
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<PersonaModel>> GetByEmail(string email)
        {
            var puertoServer = "http://localhost:5000";
            using var canal = GrpcChannel.ForAddress(puertoServer);

            var client = new PersonaProtoService.PersonaProtoServiceClient(canal);//Abrir el canal
            var request = new personaRequest { Email = email };

            var response = await client.GetPersonaAsync(request);

            var objData = new SendGridData
            {
                Contenido = "Enviar el mensaje a la dirección " + response.Persona.Direccion,
                EmailDestinatario = response.Persona.Email,
                NombreDestinatario = response.Persona.Nombre,
                Titulo = "Mensaje para: " + response.Persona.Nombre,
                SendGridApiKey = _configuration["SendGrid:ApiKey"]
            };

            await _sendGridEnviar.EnviarMail(objData);

            return Ok(response);
        }
    }
}
