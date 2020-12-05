namespace CarService.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CarService.Data;
    using CarService.Data.Models;
    using CarService.Data.Models.CarElements;
    using CarService.Services.Data;
    using CarService.Web.ViewModels.Cars;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class CarsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICarService carsService;
        private readonly IModelsService modelsService;

        public CarsController(
            UserManager<ApplicationUser> userManager,
            ICarService carsService,
            IModelsService modelsService)
        {
            this.userManager = userManager;
            this.carsService = carsService;
            this.modelsService = modelsService;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.context.Cars.Include(c => c.User);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var car = await this.context.Cars
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return this.NotFound();
            }

            return this.View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            var models = this.modelsService.GetAll<ModelsDropDrownViewModel>();
            var fuelTypes = this.carsService.GetFuelTypes();
            var viewModel = new CarViewModel
            {
                Models = models,
                FuelTypes = fuelTypes,
            };
            return this.View(viewModel);
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarViewModel car)
        {
            var userId = this.User;

            var asd = new Car
            {
                Year = car.Year,
                ModelId = car.ModelId,
                FuelTypeId = car.FuelType,
                CubicCapacity = car.CubicCapacity,
                HorsePower = car.HorsePower,
                RegistrationNumber = car.RegistrationNumber,
                TransmissionId = car.TransmissionsId,
                UserId = userId.Identity.Name,
            };

            return this.RedirectToAction("/");

        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var car = await this.context.Cars.FindAsync(id);
            if (car == null)
            {
                return this.NotFound();
            }

            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", car.UserId);
            return this.View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Year,FuelType,CubicCapacity,HorsePower,RegistrationNumber,Transmission,UserId,Id,CreatedOn,ModifiedOn")] Car car)
        {
            if (id != car.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(car);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CarExists(car.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", car.UserId);
            return this.View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var car = await this.context.Cars
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return this.NotFound();
            }

            return this.View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var car = await this.context.Cars.FindAsync(id);
            this.context.Cars.Remove(car);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CarExists(string id)
        {
            return this.context.Cars.Any(e => e.Id == id);
        }
    }
}
