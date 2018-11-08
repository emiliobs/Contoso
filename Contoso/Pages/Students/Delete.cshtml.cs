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
    public class DeleteModel : PageModel
    {
        private readonly Contoso.Data.ApplicationDbContext _context;

        public DeleteModel(Contoso.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound("El Id del registro no Existe!");
            }

            Student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.StudentId.Equals(id));
            //Student = await _context.Students.FirstOrDefaultAsync(m => m.StudentId == id);

            if (Student == null)
            {
                return NotFound("Student no encontrado  con ese Id.");
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound("El Id del registro no Existe!");
            }

            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(e => e.StudentId.Equals(id));
            //Student = await _context.Students.FindAsync(id);

            if (Student == null)
            {
                return NotFound("Student no encontrado  con ese Id.");
            }

            try
            {
                _context.Students.Remove(Student);
                await _context.SaveChangesAsync();
                 return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                ErrorMessage = ex.Message;

                return RedirectToAction("./Delete", new { id, saveChangesError = true });
            }

        }
    }
}
