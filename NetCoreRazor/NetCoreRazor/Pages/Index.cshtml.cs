using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetCoreRazor.Pages
{
    public class IndexModel : PageModel
    {
        public string Aparems { get; private set; } = "";
       
        public void OnGet()
        {  
            Aparems = Request.Query["a"];
            
        }
    }
}
