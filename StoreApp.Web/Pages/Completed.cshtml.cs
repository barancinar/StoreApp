using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace StoreApp.Web.Pages
{
    public class Completed : PageModel
    {
        private readonly ILogger<Completed> _logger;

        public Completed(ILogger<Completed> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}