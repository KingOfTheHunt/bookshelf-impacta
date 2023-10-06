using Bookshelf.Web.ViewModels.Account;
using System.Net.Http.Headers;

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
        var response = await _httpClient.PostAsJsonAsync("v1/account/sign-up", viewModel);
        response.EnsureSuccessStatusCode();
    }

    public async Task<string> LoginAsync(LoginAccountViewModel viewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("v1/account/sign-in", viewModel);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<ProfileAccountViewModel> GetAccountAsync(string userName, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            token);
        var response = await _httpClient.GetAsync($"v1/account/{userName}");
        response.EnsureSuccessStatusCode();

        var profileAccountViewModel = await response.Content.ReadFromJsonAsync<ProfileAccountViewModel>();

        return profileAccountViewModel;
    }

    public async Task<bool> DeleteAccountAsync(string userName, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            token);

        var response = await _httpClient.DeleteAsync($"v1/account/{userName}/delete");
        response.EnsureSuccessStatusCode();

        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }

    public async Task<bool> UpdatePasswordAsync(ChangePassowordAccountViewModel viewModel,
        string userName,
        string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
           token);

        var response = await _httpClient.PutAsJsonAsync($"v1/account/{userName}/change-password",
            viewModel);
        response.EnsureSuccessStatusCode();

        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }
}
