namespace Pronia_BackEnd_Project.Services.Interfaces
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
