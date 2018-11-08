using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Contoso.Data;
using Contoso.Models;

namespace Contoso.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly Contoso.Data.ApplicationDbContext _context;

        public DetailsModel(Contoso.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseId == id);

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
