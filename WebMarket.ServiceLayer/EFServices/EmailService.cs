using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace WebMarket.ServiceLayer.EFServices
{
    public class EmailService : IIdentityMessageService, Interfaces.IEmailService //
    {
        public Task SendAsync ( IdentityMessage message )
        {

            // Plug in your email service here to send an email.
            return Task.FromResult ( 0 );
        }


        public async Task<bool> SendMailByAttach(string subject, string body, string attachment, params string[] toMails)
        {
            try
            {
                var mailMsg = new System.Net.Mail.MailMessage ();
                mailMsg.BodyEncoding = System.Text.Encoding.UTF8;
                mailMsg.HeadersEncoding = System.Text.Encoding.UTF8;
                mailMsg.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMsg.Priority = System.Net.Mail.MailPriority.High;
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = true;
                mailMsg.From = new System.Net.Mail.MailAddress ( "Omid.green.b13@gmail.com" ,"سید امید رفیعی" ,
                                                                 System.Text.Encoding.UTF8 );
                mailMsg.Sender = new System.Net.Mail.MailAddress ( "Omid.green.b13@gmail.com" ,"سید امید رفیعی" ,
                                                                   System.Text.Encoding.UTF8 );
                mailMsg.Attachments.Add ( new System.Net.Mail.Attachment ( attachment ) );
                foreach ( var mail in toMails )
                {
                    mailMsg.To.Add ( new System.Net.Mail.MailAddress ( mail ) );
                }
                var smtp = new System.Net.Mail.SmtpClient ( "smtp.gmail.com" ,587 );
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential ( "Omid.green.b13@gmail.com" ,"GreenRoad10." );
                await smtp.SendMailAsync(mailMsg);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendMailByAttach(string subject,string body ,System.Net.Mail.Attachment attachment ,params string [ ] toMails )
        {
            try
            {
                var mailMsg = new System.Net.Mail.MailMessage ();
                mailMsg.BodyEncoding = System.Text.Encoding.UTF8;
                mailMsg.HeadersEncoding = System.Text.Encoding.UTF8;
                mailMsg.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMsg.Priority = System.Net.Mail.MailPriority.High;
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = true;
                mailMsg.From = new System.Net.Mail.MailAddress ( "Omid.green.b13@gmail.com" ,
                                                                 "سید امید رفیعی" ,
                                                                 System.Text.Encoding.UTF8 );
                mailMsg.Sender = new System.Net.Mail.MailAddress ( "Omid.green.b13@gmail.com" ,
                                                                   "سید امید رفیعی" ,
                                                                   System.Text.Encoding.UTF8 );
                mailMsg.Attachments.Add ( attachment );
                foreach ( var mail in toMails )
                {
                    mailMsg.To.Add ( new System.Net.Mail.MailAddress ( mail ) );
                }
                var smtp = new System.Net.Mail.SmtpClient ( "smtp.gmail.com" ,
                                                            587 );
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential ( "Omid.green.b13@gmail.com" ,
                                                                      "GreenRoad10." );
                await smtp.SendMailAsync(mailMsg);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendMailByAttachList(string subject,
                                                  string body ,
                                                  string toMail ,
                                                  params string [ ] attachments )
        {
            try
            {
                var mailMsg = new System.Net.Mail.MailMessage ();
                mailMsg.BodyEncoding = System.Text.Encoding.UTF8;
                mailMsg.HeadersEncoding = System.Text.Encoding.UTF8;
                mailMsg.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMsg.Priority = System.Net.Mail.MailPriority.High;
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = true;
                mailMsg.From = new System.Net.Mail.MailAddress ( "Omid.green.b13@gmail.com" ,
                                                                 "سید امید رفیعی" ,
                                                                 System.Text.Encoding.UTF8 );
                mailMsg.Sender = new System.Net.Mail.MailAddress ( "Omid.green.b13@gmail.com" ,
                                                                   "سید امید رفیعی" ,
                                                                   System.Text.Encoding.UTF8 );
                mailMsg.To.Add ( new System.Net.Mail.MailAddress ( toMail ) );
                foreach ( var item in attachments )
                {
                    mailMsg.Attachments.Add ( new System.Net.Mail.Attachment ( item ) );
                }
                var smtp = new System.Net.Mail.SmtpClient ( "smtp.gmail.com" ,
                                                            587 );
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential ( "Omid.green.b13@gmail.com" ,
                                                                      "GreenRoad10." );
                await smtp.SendMailAsync(mailMsg);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendMailByAttachList2(string subject,
                                                   string body ,
                                                   string toMail ,
                                                   params System.Net.Mail.Attachment [ ] attachments )
        {
            try
            {
                var mailMsg = new System.Net.Mail.MailMessage ();
                mailMsg.BodyEncoding = System.Text.Encoding.UTF8;
                mailMsg.HeadersEncoding = System.Text.Encoding.UTF8;
                mailMsg.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMsg.Priority = System.Net.Mail.MailPriority.High;
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = true;
                mailMsg.From = new System.Net.Mail.MailAddress ( "Omid.green.b13@gmail.com" ,
                                                                 "سید امید رفیعی" ,
                                                                 System.Text.Encoding.UTF8 );
                mailMsg.Sender = new System.Net.Mail.MailAddress ( "Omid.green.b13@gmail.com" ,
                                                                   "سید امید رفیعی" ,
                                                                   System.Text.Encoding.UTF8 );
                mailMsg.To.Add ( new System.Net.Mail.MailAddress ( toMail ) );
                foreach ( var item in attachments )
                {
                    mailMsg.Attachments.Add ( item );
                }
                var smtp = new System.Net.Mail.SmtpClient ( "smtp.gmail.com" ,
                                                            587 );
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential ( "Omid.green.b13@gmail.com" ,
                                                                      "GreenRoad10." );
                await smtp.SendMailAsync(mailMsg);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendMail(string smtp,string fromMail ,string password ,string fromName ,string subject ,
                                      string body ,
                                      params string [ ] toMails )
        {
            try
            {
                var mailMsg = new System.Net.Mail.MailMessage ();
                mailMsg.BodyEncoding = System.Text.Encoding.UTF8;
                mailMsg.HeadersEncoding = System.Text.Encoding.UTF8;
                mailMsg.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMsg.Priority = System.Net.Mail.MailPriority.High;
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = true;
                mailMsg.From = new System.Net.Mail.MailAddress ( fromMail ,
                                                                 fromName ,
                                                                 System.Text.Encoding.UTF8 );
                mailMsg.Sender = new System.Net.Mail.MailAddress ( fromMail ,
                                                                   fromName ,
                                                                   System.Text.Encoding.UTF8 );
                foreach ( var mail in toMails )
                {
                    mailMsg.To.Add ( new System.Net.Mail.MailAddress ( mail ) );
                }
                var smtpServer = new System.Net.Mail.SmtpClient ( smtp.Split ( ':' ) [0] ,
                                                                  System.Convert.ToInt32 ( smtp.Split ( ':' ) [1] ) );
                smtpServer.EnableSsl = true;
                smtpServer.Credentials = new System.Net.NetworkCredential ( fromMail ,password );
                await smtpServer.SendMailAsync(mailMsg);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task < bool > SendMail ( string subject ,string body ,params string [ ] toMails )
        {
            try
            {
                var mailMsg = new System.Net.Mail.MailMessage();
                mailMsg.BodyEncoding = System.Text.Encoding.UTF8;
                mailMsg.HeadersEncoding = System.Text.Encoding.UTF8;
                mailMsg.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMsg.Priority = System.Net.Mail.MailPriority.High;
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = true;
                mailMsg.From = new System.Net.Mail.MailAddress("Omid.green.b13@gmail.com", "سید امید رفیعی", System.Text.Encoding.UTF8);
                mailMsg.Sender = new System.Net.Mail.MailAddress("Omid.green.b13@gmail.com", "سید امید رفیعی", System.Text.Encoding.UTF8);
                foreach (var mail in toMails)
                {
                    mailMsg.To.Add(new System.Net.Mail.MailAddress(mail));
                }
                var smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("Omid.green.b13@gmail.com", "GreenRoad10.");
                await smtp.SendMailAsync(mailMsg);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}