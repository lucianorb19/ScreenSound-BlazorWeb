

using ScreenSound.Web.Requests;
using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services
{
    public class MusicaAPI
    {
        //PROPRIEDADES E CAMPOS
        private readonly HttpClient _httpClient;


        //CONSTRUTOR
        public MusicaAPI(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        //DEMAIS MÉTODOS

        public async Task<ICollection<MusicaResponse>?> GetMusicasAsync()
        {
            return await _httpClient.GetFromJsonAsync<ICollection<MusicaResponse>>
                ("musicas");
        }

        public async Task<MusicaResponse?> GetMusicaPorNomeAsync(string nome)
        {
            return await _httpClient.GetFromJsonAsync<MusicaResponse>($"musicas/{nome}");
        }

        public async Task AddMusicaAsync(MusicaRequest musica)
        {
            await _httpClient.PostAsJsonAsync("musicas", musica);
        }

        public async Task DeleteMusicaAsync(int id)
        {
            await _httpClient.DeleteAsync($"musicas/{id}");
        }




    }
}
