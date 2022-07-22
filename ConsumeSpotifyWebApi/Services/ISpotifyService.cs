using ConsumeSpotifyWebApi.Models;

namespace ConsumeSpotifyWebApi.Services
{
    public interface ISpotifyService
    {
        Task<IEnumerable<Releases>> GetNewReleases(string countryCode, int limit, string accessToken);
    }
}
