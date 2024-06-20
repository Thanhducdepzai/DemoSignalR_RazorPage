using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoSignalR_RazorPage.Models;
using Microsoft.AspNetCore.SignalR;
using DemoSignalR_RazorPage.Hubs;

namespace DemoSignalR_RazorPage.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly DemoSignalR_RazorPage.Models.PRN211_1Context _context;
       

        public IndexModel(DemoSignalR_RazorPage.Models.PRN211_1Context context,IHubContext<ServerHub>hub)
        {
            _context = context;
            
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Students != null)
            {
                Student = await _context.Students
                .Include(s => s.Depart).ToListAsync();
            }
        }
    }
}
