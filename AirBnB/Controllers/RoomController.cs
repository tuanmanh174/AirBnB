using AirBnB.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AirBnB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomsController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms(int guests, int page = 1, int pageSize = 10)
        {
            var rooms = await _roomRepository.SearchRoomsAsync(guests, page, pageSize);
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            return room != null ? Ok(room) : NotFound();
        }
    }
}
