using ConsumeSpotifyWebApi.Models;
using ConsumeSpotifyWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConsumeSpotifyWebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        private readonly ISpotifyService _spotifyService;

        public HomeController(ISpotifyAccountService spotifyAccountService, IConfiguration configuration, ISpotifyService spotifyService)
        {
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
            _spotifyService = spotifyService;
        }

        public async Task<IActionResult> Index()
        {
            var releases = await GetReleases();
            return View(releases);
        }

        private async Task<IEnumerable<Releases>> GetReleases()
        {
            try
            {
                var token = await _spotifyAccountService.GetToken(_configuration["Spotify:ClientId"], _configuration["Spotify:ClientSecret"]);

                var newReleases = await _spotifyService.GetNewReleases("MX", 50, token);

                return newReleases;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Enumerable.Empty<Releases>();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}