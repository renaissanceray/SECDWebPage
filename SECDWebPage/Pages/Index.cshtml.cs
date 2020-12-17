using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SECDWebPage.Services;

namespace SECDWebPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string Text;

        public IndexModel(ILogger<IndexModel> logger, GoogleSheetsService sheetsService)
        {
            _logger = logger;
            SheetsService = sheetsService;
        }

        public GoogleSheetsService SheetsService { get; }

        public void OnGet()
        {
            //Text = SheetsService.Connect();
        }
    }
}
