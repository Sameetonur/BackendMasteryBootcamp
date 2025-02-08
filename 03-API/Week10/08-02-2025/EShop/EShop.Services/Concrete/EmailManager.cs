using System;
using System.Net.Mail;
using EShop.Services.Abstract;
using EShop.Shared.Configurations.Email;
using EShop.Shared.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EShop.Services.Concrete;

public class EmailManager : IEmailService
{
    private readonly EmailConfig _emailconfig;

    public EmailManager(IOptions<EmailConfig> emailconfig)
    {
        _emailconfig = emailconfig.Value;
    }

    public async Task<ResponseDto<NoContent>> SendEmailAsync(string emailTo, string subject, string htmlBody)
    {
        try
        {
            if (string.IsNullOrEmpty(_emailconfig.SmtpServer))
            {
                return ResponseDto<NoContent>.Fail("Smtp Sunucu adresi yapılandırılmamış!",StatusCodes.Status500InternalServerError);
                
            }
            if (string.IsNullOrEmpty(_emailconfig.SmtpUser))
            {
                return ResponseDto<NoContent>.Fail("Smtp Kullanıcı adı bilgisi tanımlanmamış", StatusCodes.Status500InternalServerError);

            }
            if (string.IsNullOrEmpty(_emailconfig.SmtpPasword))
            {
                return ResponseDto<NoContent>.Fail("Smtp Şifresi yapılandırılmamış!", StatusCodes.Status500InternalServerError);

            }
            if (string.IsNullOrEmpty(emailTo))
            {
                return ResponseDto<NoContent>.Fail("Alıcı email adresi boş olamaz!", StatusCodes.Status400BadRequest);

            }
            if (!IsValidEmail(emailTo))
            {
                return ResponseDto<NoContent>.Fail(" Gersiz email formatı!", StatusCodes.Status400BadRequest);

            }

        }
        catch (System.Exception)
        {
            
            throw;
        }
        
    }

        private bool IsValidEmail(string emailAddress)
        {
            try
            {
                var addr = new MailAddress(emailAddress);
                return addr.Address==emailAddress;
            }
            catch (System.Exception)
            {
                return false;
                
            }
        }
}
