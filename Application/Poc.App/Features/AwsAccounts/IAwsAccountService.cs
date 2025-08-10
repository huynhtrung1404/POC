namespace Poc.App.Features.AwsAccounts;

public interface IAwsAccountService
{
    Task<bool> CreateAnAccount();
    Task<bool> CreateMultipleAccount();
}