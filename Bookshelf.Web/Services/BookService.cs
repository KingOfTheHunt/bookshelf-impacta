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

        public async Task<BookDetailsViewModel> GetBookDetailsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"v1/book/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var book = await response.Content.ReadFromJsonAsync<BookDetailsViewModel>();

                return book;
            }
            
            return null;
        }
    }
}
