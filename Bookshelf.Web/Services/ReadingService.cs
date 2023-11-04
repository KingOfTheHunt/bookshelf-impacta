using Bookshelf.Web.ViewModels.Reading;
using System.Net.Http.Headers;

namespace Bookshelf.Web.Services;

public class ReadingService
{
    private readonly HttpClient _httpClient;

    public ReadingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ShowReadingsViewModel>> GetReadingsAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = SetAuthorization(token);

        var response = await _httpClient.GetAsync($"v1/reading/");
        response.EnsureSuccessStatusCode();

        var readings = await response.Content.ReadFromJsonAsync<List<ShowReadingsViewModel>>();

        return readings;
    }

    private AuthenticationHeaderValue SetAuthorization(string token)
    {
        return new AuthenticationHeaderValue("Bearer", token);
    }
}
