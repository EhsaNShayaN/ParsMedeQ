namespace ParsMedeQ.Application.Services.OTP;
public interface IOtpServiceFactory
{
    ValueTask<IOtpService> Create();
}