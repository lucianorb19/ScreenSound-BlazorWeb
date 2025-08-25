using ScreenSound.Web.Requests;
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

        //MÉTODO QUE RETORNA UMA COLEÇÃO DE ArtistaResponse DA API
        //FILTRANDO PELO NOME
        public async Task<ICollection<ArtistaResponse>?> GetArtistasDeNomeAsync(string nome)
        {
            return await
                _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>($"artistas/{nome}");
        }
        
        //MÉTODO QUE FAZ UM CADASTRO DE ARTISTA NA API A PARTIR DE UM OBJETO
        //ArtistaRequest
        public async Task AddArtistaAsync(ArtistaRequest artista)
        {
            await _httpClient.PostAsJsonAsync("artistas", artista);
        }

        //MÉTODO QUE RETORNA UM ArtistaResponse DADO UMA STRING Nome
        public async Task<ArtistaResponse?> GetArtistaPorNomeAsync(string nome)
        {
            return await _httpClient
                .GetFromJsonAsync<ArtistaResponse>($"/artistas/{nome}");
        }

        public async Task DeleteArtistaAsync(int id)
        {
            await _httpClient
                .DeleteAsync($"artistas/{id}");
        }

        public async Task UpdateArtistaAsync (ArtistaRequestEdit artista)
        {
            await _httpClient
                .PutAsJsonAsync($"artistas", artista);
        }

    }
}
