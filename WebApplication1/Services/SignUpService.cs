using Presentation;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface ISignUpService
{
    Task<SignUpResult> SignUpAsync(SignUpFormData formData);
}
public class SignUpService(AccountGrpcService.AccountGrpcServiceClient accountClient) : ISignUpService
{
    private readonly AccountGrpcService.AccountGrpcServiceClient _accountClient = accountClient;

    public async Task<SignUpResult> SignUpAsync(SignUpFormData formData)
    {
        var request = new CreateAccountRequest
        {
            Email = formData.Email,
            Password = formData.Password
        };
        try
        {
            var result = await _accountClient.CreateAccountAsync(request);
            return result.Succeeded ? new SignUpResult
            {
                Success = result.Succeeded,
                Message = result.Message,
                UserId = result.UserId
            } : new SignUpResult
            {
                Success = result.Succeeded,
                Message = result.Message,
            };
        }
        catch
        {
            return new SignUpResult
            {
                Success = false,
                Message = "An error occurred while processing your request."
            };  
        }
    }
}
