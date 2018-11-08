using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contoso.Data;
using Contoso.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Contoso.Pages
{
    public class AboutModel : PageModel
    {
     private readonly ApplicationDbContext db;

        public AboutModel(ApplicationDbContext db)
        {
            this.db = db;
        }


        public IList<EnrollmentDateGroup> Student { get; set; }

        public async Task OnGetAsync()
        {


            IQueryable<EnrollmentDateGroup> data = from student
                                                   in db.Students
                                                    group student 
                                                    by student.EnrollmentDate 
                                                    into dateGroup
                                                    select new EnrollmentDateGroup()
                                                    {
                                                        EnrollmentData = dateGroup.Key,
                                                        StudentCount = dateGroup.Count()
                                                    };

                Student = await data.AsNoTracking().ToListAsync();
        }

       
    }
}
