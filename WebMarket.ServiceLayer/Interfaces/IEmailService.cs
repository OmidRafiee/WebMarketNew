using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;
using WebMarket.ViewModel.Admin.Setting;

namespace WebMarket.ServiceLayer.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendMailByAttach(string subject, string body, string attachment, params string[] toMails);
        Task<bool> SendMailByAttach(string subject, string body, System.Net.Mail.Attachment attachment,
                                         params string[] toMails);

        Task<bool> SendMailByAttachList(string subject, string body, string toMail, params string[] attachments);

        Task<bool> SendMailByAttachList2(string subject, string body, string toMail,
            params System.Net.Mail.Attachment[] attachments);

        Task<bool> SendMail(string smtp, string fromMail, string password, string fromName, string subject,
                                                string body,
                                                params string[] toMails);

        Task < bool > SendMail ( string subject ,string body ,params string [ ] toMails );



    }
}
