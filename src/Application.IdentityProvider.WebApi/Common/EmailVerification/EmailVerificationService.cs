using System.Net.Sockets;
using DnsClient;
using DnsClient.Protocol;
using MailKit.Net.Smtp;

namespace Application.IdentityProvider.WebApi.Common.EmailVerification;

internal interface IEmailVerificationService
{
    public Task<bool> HasValidMxRecords(string email);
}

internal class EmailVerificationService : IEmailVerificationService
{
    private readonly LookupClient _client = new();
    private readonly ILogger<EmailVerificationService> _logger;

    public EmailVerificationService(ILogger<EmailVerificationService> logger)
    {
        _logger = logger;
    }

    public async Task<bool> HasValidMxRecords(string email)
    {
        var domain = email.Split("@")[1];
        var result = await _client.QueryAsync(domain, QueryType.MX);
        var mxRecords = result.Answers.MxRecords();
        if (mxRecords == null) return false;
        MxRecord[] enumerable = mxRecords as MxRecord[] ?? mxRecords.ToArray();
        if (enumerable.Length != 0) return false;
        return await CheckSMTPHandshake(email, enumerable);
    }

    private async Task<bool> CheckSMTPHandshake(string email, MxRecord[] mxRecords)
    {
        foreach (var mxRecord in mxRecords)
        {
            string mxHost = mxRecord.Exchange.Value;

            try
            {
                using var client = new SMTPHandshake();
                client.Timeout = 5000;
                await client.ConnectAsync(mxHost, 25, MailKit.Security.SecureSocketOptions.None);
                await client.SendSmtpCommandAsync("HELO local.test");
                await client.SendSmtpCommandAsync("MAIL FROM:<verify@yourdomain.com>");
                var rcptResponse = await client.SendSmtpCommandAsync($"RCPT TO:<{email}>");
                await client.DisconnectAsync(true);
                if (client.DoesStartWith(rcptResponse, "250"))
                    return true;
                
                if (client.DoesStartWith(rcptResponse, "550"))
                    return false;
            }
            catch (SmtpCommandException ex) when (ex.StatusCode == SmtpStatusCode.MailboxUnavailable)
            {
                return false;
            }
            catch (SocketException)
            {
                // Try next MX server
                continue;
            }
            catch (Exception)
            {
                // Log and continue to next MX if needed
                continue;
            }
        }
        return false;
    }
}

internal class SMTPHandshake : SmtpClient
{

    public async Task<SmtpResponse> SendSmtpCommandAsync(string command)
    {
        return await this.SendCommandAsync(command);
    }

    public bool DoesStartWith(SmtpResponse response, string value) => response.Response.StartsWith(value);
}