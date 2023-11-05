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

    public async Task<bool> AddReadingAsync(string token, AddReadingViewModel viewModel)
    {
        _httpClient.DefaultRequestHeaders.Authorization = SetAuthorization(token);

        var response = await _httpClient.PostAsJsonAsync($"v1/reading/new", viewModel);

        if (response.StatusCode == System.Net.HttpStatusCode.Created)
            return true;

        return false;
    }

    public async Task<ReadingDetailsViewModel> GetReadingAsync(string token, int readingId)
    {
        _httpClient.DefaultRequestHeaders.Authorization = SetAuthorization(token);

        var response = await _httpClient.GetAsync($"v1/reading/get-reading/{readingId}");
        response.EnsureSuccessStatusCode();

        var reading = await response.Content.ReadFromJsonAsync<ReadingDetailsViewModel>();

        return reading;
    }

    public async Task<bool> UpdateReadingAsync(string token, UpdateReadingViewModel viewModel)
    {
        _httpClient.DefaultRequestHeaders.Authorization = SetAuthorization(token);

        var response = await _httpClient.PutAsJsonAsync("/v1/reading/update", viewModel);

        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            return true;

        return false;
    }

    private AuthenticationHeaderValue SetAuthorization(string token)
    {
        return new AuthenticationHeaderValue("Bearer", token);
    }
}
