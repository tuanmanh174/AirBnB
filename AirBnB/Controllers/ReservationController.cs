using AirBnB.DTO;
using AirBnB.Model;
using AirBnB.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AirBnB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
        {
            var reservation = await _reservationService.CreateReservationAsync(reservationDto);
            return CreatedAtAction(nameof(CreateReservation), new { id = reservation.Id }, reservation);
        }
    }

}
