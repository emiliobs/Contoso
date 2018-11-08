using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Contoso.Data;
using Contoso.Models;

namespace Contoso.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly Contoso.Data.ApplicationDbContext _context;

        public DetailsModel(Contoso.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            Student = await _context.Students.Include(e => e.Enrollments).ThenInclude(c => c.Course).FirstOrDefaultAsync();
            //Student = await _context.Students.FirstOrDefaultAsync(m => m.StudentId == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
