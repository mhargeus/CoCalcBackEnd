using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoCalcBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private static List<Car> cars = new List<Car>
            {
                new Car {id = 1, brand="Volvo", model="XC40", engine="T3", wltp= 2, },
        
            };
        private readonly DataContext _context;

        public CarController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Car>>> Get()
        {
            return Ok(await _context.Cars.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return BadRequest("Car not found.");
            return Ok(car);
        }
        [HttpPost]
        public async Task<ActionResult<List<Car>>> AddCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return Ok(await _context.Cars.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<Car>>> UpdateCar(Car request)
        {
            var car = await _context.Cars.FindAsync(request.id);
            if (car == null)
                return BadRequest("Car not found.");

            car.brand = request.brand;
            car.model = request.model;
            car.engine = request.engine;
            car.wltp = request.wltp;

            await _context.SaveChangesAsync();
            return Ok(cars);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Car>>> Delete(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return BadRequest("Car not found.");
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return Ok(cars);
        }


    }
}
