using Contoso.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Contoso.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly Contoso.Data.ApplicationDbContext _context;

        public IndexModel(Contoso.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        #region Properties

        public IList<Student> Student { get;set; }

        public PaginatedList<Student> Students { get; set; }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }


        #endregion


        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            
            //Sort
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            currentFilter = searchString;



            //sort
            IQueryable<Student> studentsIQ = from e in _context.Students select e;

            //Filter   
            CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                studentsIQ = studentsIQ.Where(s => s.LastName.ToUpper().Trim().Contains(searchString.ToUpper().Trim()) || 
                             s.FirstMidName.ToUpper().Trim().Contains(searchString.ToUpper().Trim()));
            }


         

            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(e => e.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(e => e.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(e => e.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(e => e.LastName);
                    break;
            }

            int pageSize = 3;


            Students = await PaginatedList<Student>.CreateAsync(studentsIQ.AsNoTracking(),pageIndex ?? 1, pageSize);

            //Student = await  studentsIQ.AsNoTracking().ToListAsync();
        }
    }
}
