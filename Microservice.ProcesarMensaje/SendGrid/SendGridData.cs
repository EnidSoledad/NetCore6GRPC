namespace Microservice.ProcesarMensaje.SendGrid
{
    public class SendGridData
    {
        public string SendGridApiKey { get; set; }  
        public string EmailDestinatario { get; set; }
        public string NombreDestinatario { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }

    }
}
