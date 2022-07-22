using ConsumeSpotifyWebApi.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ConsumeSpotifyWebApi.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly HttpClient _httpClient;

        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Releases>> GetNewReleases(string countryCode, int limit, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"browse/new-releases?country={countryCode}&limit={limit}");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<GetNewReleasedResult>(responseStream);

            return responseObject?.albums?.items.Select(i=>new Releases
            {
                Name = i.name,
                Date = i.release_date,
                ImageUrl = i.images.FirstOrDefault().url,
                Link = i.external_urls.spotify,
                Artist = string.Join(", ", i.artists.Select(a => a.name))
            });
        }
    }
}