using Domain.DomainEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class ReservationSystemController : ControllerBase
    {
        private readonly ILogger<ReservationSystemController> _logger;

        public ReservationSystemController(ILogger<ReservationSystemController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Name = "Get")]
        public string Get()
        {
            return "helloWorld";
        }

        [HttpGet(Name = "GetReservation")]
        public Reservation GetReservation()
        {
            return new Reservation() { Start = DateTime.Now, End = DateTime.Now.AddHours(1), CustomerId = 1 };
        }

        [HttpGet(Name = "GetReservations")]
        public IEnumerable<Reservation> GetReservations()
        {
            var list = new List<Reservation>() {
                new Reservation() { Start = DateTime.Now, End = DateTime.Now.AddHours(1), CustomerId = 1 },
                new Reservation() { Start = DateTime.Now.AddHours(1), End = DateTime.Now.AddHours(2), CustomerId = 2 },
                new Reservation() { Start = DateTime.Now.AddHours(2), End = DateTime.Now.AddHours(3), CustomerId = 3 }
             };

            return list;
        }
    }
}
