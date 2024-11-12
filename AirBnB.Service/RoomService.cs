using AirBnB.DTO;
using AirBnB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnB.Service
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(RoomSearchDto searchDto);
        Task<Room> GetRoomByIdAsync(int id);
    }
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(RoomSearchDto searchDto)
        {
            return await _roomRepository.SearchRoomsAsync(searchDto);
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }
    }
}
