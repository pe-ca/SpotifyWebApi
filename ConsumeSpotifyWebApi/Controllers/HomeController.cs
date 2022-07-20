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

        public HomeController(ISpotifyAccountService spotifyAccountService, IConfiguration configuration)
        {
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var token = await _spotifyAccountService.GetToken(_configuration["Spotify:ClientId"], _configuration["Spotify:ClientSecret"]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return View();
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