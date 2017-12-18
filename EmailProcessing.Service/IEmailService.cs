using EmailProcessing.DAL.Entities;
using EmailProcessing.Model.EmailProcessingViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailProcessing.Service
{
   public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task<List<Message>> PaerserEmailAsync(Setting setting, ISoapService soapService);
        Task MarkAsRead(Setting setting, List<Message> messages);
    }
}
