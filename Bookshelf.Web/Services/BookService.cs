using Bookshelf.Web.ViewModels.Book;

namespace Bookshelf.Web.Services
{
    public class BookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BookSearchViewModel>> SearchBooksAsync(string query)
        {
            var response = await _httpClient.GetAsync($"v1/book?query={query}");
            var books = await response.Content.ReadFromJsonAsync<List<BookSearchViewModel>>();

            return books;
        }
    }
}
