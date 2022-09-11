using ODS.HelperModels;

namespace ODS.Interfaces
{
    public interface IMailService
    {
        Task SendAsync(MailRequest mailRequest);
    }
}
