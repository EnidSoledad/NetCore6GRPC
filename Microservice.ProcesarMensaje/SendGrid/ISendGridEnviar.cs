using System.Threading.Tasks;

namespace Microservice.ProcesarMensaje.SendGrid
{
    public interface ISendGridEnviar
    {
        Task<(bool resultado, string errorMenssage)> EnviarMail(SendGridData data);
    }
}
