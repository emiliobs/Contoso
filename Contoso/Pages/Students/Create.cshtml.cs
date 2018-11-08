using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Contoso.Data;
using Contoso.Models;

namespace Contoso.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly Contoso.Data.ApplicationDbContext _context;

        public CreateModel(Contoso.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyStudent = new Student();

            if (await TryUpdateModelAsync<Student>(emptyStudent , "student", s => s.FirstMidName, s => s.LastName,
                s => s.EnrollmentDate))
            {
            _context.Students.Add(emptyStudent);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

            }

            return null;
        }
    }
}