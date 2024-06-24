using HotelStaffPortal.Web.Data;
using HotelStaffPortal.Web.Models;
using HotelStaffPortal.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelStaffPortal.Web.Controllers
{
    public class StaffController : Controller
    {
        private readonly HotelDbContext dbContext;
        public StaffController(HotelDbContext dbContext )
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStaffViewModel viewModel)
        {
            var Staff = new Staff
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,
            };
            await dbContext.AllStaff.AddAsync(Staff);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]

        public async Task<IActionResult> List()
        {
            var allstaff = await dbContext.AllStaff.ToListAsync();

            return View(allstaff);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var staff = await dbContext.AllStaff.FindAsync(id);
            return View(staff);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (Staff viewModel)
        {
           var staff = await dbContext.AllStaff.FindAsync(viewModel.Id);
            if (staff is not null)
            {
                staff.Name = viewModel.Name;
                staff.Email = viewModel.Email;
                staff.Phone = viewModel.Phone;
                staff.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Staff");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Staff viewModel)
        {
            var staff = await dbContext.AllStaff.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (staff is not null)
            {
                dbContext.AllStaff.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Staff");
        }
    }
}
