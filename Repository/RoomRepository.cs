using AirBnB.DTO;
using AirBnB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnB.Repository
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> SearchRoomsAsync(int guests, int page, int pageSize);
        Task<Room> GetByIdAsync(int id);
    }

    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> SearchRoomsAsync(int guests, int page, int pageSize)
        {
            return await _context.Rooms
                .Where(r => r.IsAvailable && r.Capacity >= guests)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }
    }
}
