using Indotalent.Infrastructures.Extensions;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Indotalent.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            this.SetupViewDataTitleFromUrl();
        }
    }
}
