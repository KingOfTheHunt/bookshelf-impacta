﻿using Bookshelf.Web.ViewModels.Account;
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
        var response = await _httpClient.PostAsJsonAsync("/v1/account/sign-up", viewModel);
        response.EnsureSuccessStatusCode();
    }

    public async Task<string> LoginAsync(LoginAccountViewModel viewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("v1/account/sign-in", viewModel);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<ProfileAccountViewModel> GetAccount(string userName, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            token);
        var response = await _httpClient.GetAsync($"v1/account/{userName}");
        response.EnsureSuccessStatusCode();

        var profileAccountViewModel = await response.Content.ReadFromJsonAsync<ProfileAccountViewModel>();

        return profileAccountViewModel;
    }
}
