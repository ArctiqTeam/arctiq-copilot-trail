using chessweb_csharp.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace chessweb_csharp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public string Fen { get; set; } = ChessConstants.StartFen;

        [BindProperty]
        public string GameId { get; set; } = string.Empty;

        [BindProperty]
        public string FenPosition { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAddGameAsync()
        {
            var content = new StringContent($"{{\"fen\":\"{Fen}\"}}", Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Chess/addGame", content);
            if (response.IsSuccessStatusCode)
            {
                // Handle success
            }
            return Page();
        }

        public async Task<IActionResult> OnGetGetGameByIdAsync()
        {
            var response = await _httpClient.GetAsync($"/Chess/getGameById/{GameId}");
            if (response.IsSuccessStatusCode)
            {
                // Handle success
            }
            return Page();
        }

        public async Task<IActionResult> OnGetGetPositionAsync()
        {
            var response = await _httpClient.GetAsync($"/Chess/getPosition?fen={FenPosition}");
            if (response.IsSuccessStatusCode)
            {
                // Handle success
            }
            return Page();
        }
    }
}