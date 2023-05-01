using EduConnect.DTOs;
using MediatR;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IServices
{
    public interface IEmailSender
    {
        Task<Unit> SendEmail(EmailDto requestModel);
    }
}
