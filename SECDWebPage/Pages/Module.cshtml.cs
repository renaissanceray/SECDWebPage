using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvertSheetToPDF.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SECDWebPage.Services;

namespace SECDWebPage
{
    public class ModuleModel : PageModel
    {
        public Module Module { get; set; }
        public GoogleSheetsService SheetsService { get; }

        public ModuleModel(GoogleSheetsService sheetsService)
        {
            SheetsService = sheetsService;
        }

        public void OnGet()
        {
            Module = SheetsService.GetModuleData();
        }
    }
}