using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services
{
    public class ArtistaAPI
    {
        //PROPRIEDADES - CAMPOS
        private readonly HttpClient _httpClient;

        //CONSTRUTOR
        public ArtistaAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        //MÉTODO QUE RETORNA UMA COLEÇÃO DE ArtistaResponse DA API
        public async Task<ICollection<ArtistaResponse>?> GetArtistasAsync()
        {
            return await
                _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("artistas");
        }
    }
}
