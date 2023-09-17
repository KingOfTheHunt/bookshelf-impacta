using Bookshelf.Web.ViewModels.Account;

namespace Bookshelf.Web.Services;

public class AccountService
{
    private readonly HttpClient _httpClient;

    public AccountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateAccountAsync(CreateAccountViewModel viewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("/v1/account/sign-up", viewModel);
        response.EnsureSuccessStatusCode();
    }

    public async Task<string> LoginAsync(LoginAccountViewModel viewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("v1/account/sign-in", viewModel);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    }
}
