using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoSignalR_RazorPage.Models;
using DemoSignalR_RazorPage.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace DemoSignalR_RazorPage.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly DemoSignalR_RazorPage.Models.PRN211_1Context _context;
        private readonly IHubContext<ServerHub> hubContext;

        public CreateModel(DemoSignalR_RazorPage.Models.PRN211_1Context context,IHubContext<ServerHub>hub)
        {
            _context = context;
            hubContext = hub;
        }

        public IActionResult OnGet()
        {
        ViewData["DepartId"] = new SelectList(_context.Departments, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Students == null || Student == null)
            {
                return Page();
            }

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("LoadAll");

            return RedirectToPage("./Index");
        }
    }
}
