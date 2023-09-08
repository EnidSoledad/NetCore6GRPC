using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Microservice.ProcesarMensaje.SendGrid
{
    public class SendGridEnviar : ISendGridEnviar
    {
        public async Task<(bool resultado, string errorMenssage)> EnviarMail(SendGridData data)
        {
            try
            {
                var sendGridCliente = new SendGridClient(data.SendGridApiKey);
                var destinatario = new EmailAddress(data.EmailDestinatario, data.NombreDestinatario);
                var titulo = data.Titulo;
                var sender = new EmailAddress("enid.faundez.c@gmail.com", "Enid");
                var contenido = data.Contenido;
                var objEmail = MailHelper.CreateSingleEmail(sender, destinatario, titulo, contenido, contenido);
                await sendGridCliente.SendEmailAsync(objEmail);

                return (true, null);

            }
            catch (Exception e)
            {
                return (false, e.Message );
            }
        }
    }
}
