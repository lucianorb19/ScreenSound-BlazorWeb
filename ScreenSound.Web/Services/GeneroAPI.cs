using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services;

public class GeneroAPI
{
    //PROPRIEDADES E CAMPOS
    private readonly HttpClient _httpClient;


    //CONSTRUTOR
    public GeneroAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }


    //DEMAIS MÉTODOS
    public async Task<List<GeneroResponse>?> GetGenerosAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<GeneroResponse>>("generos");
    }

    public async Task<GeneroResponse?> GetGeneroPorNomeAsync(string nome)
    {
        return await _httpClient.GetFromJsonAsync<GeneroResponse>($"generos/{nome}");
    }
}
