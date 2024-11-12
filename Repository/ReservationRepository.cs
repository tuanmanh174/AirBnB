using AirBnB.DTO;
using AirBnB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnB.Repository
{
    public interface IReservationRepository
    {
        Task<Reservation> AddAsync(Reservation reservation);
    }

    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation> AddAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }


    }


    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(ReservationDto reservationDto);
    }

    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;

        public ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository, IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }

        public async Task<Reservation> CreateReservationAsync(ReservationDto reservationDto)
        {
           
            var room = await _roomRepository.GetByIdAsync(reservationDto.RoomId);
            if (room == null || !room.IsAvailable)
                throw new Exception("Room is not available.");

            var user = await _userRepository.GetUserByEmailAsync(reservationDto.CustomerEmail);
            if (user == null)
            {
               
                user = new User
                {
                    Name = reservationDto.CustomerName,
                    Email = reservationDto.CustomerEmail,
                    PhoneNumber = reservationDto.CustomerPhoneNumber
                };
                await _userRepository.AddAsync(user);
            }
            else
            {
              
                user.Name = reservationDto.CustomerName;
                user.PhoneNumber = reservationDto.CustomerPhoneNumber;
                await _userRepository.UpdateAsync(user);
            }

        
            var reservation = new Reservation
            {
                RoomId = reservationDto.RoomId,
                UserId = user.Id,
                CheckInDate = reservationDto.CheckInDate,
                CheckOutDate = reservationDto.CheckOutDate,
                Guests = reservationDto.Guests,
                TotalPrice = room.Price * (reservationDto.CheckOutDate - reservationDto.CheckInDate).Days
            };

            return await _reservationRepository.AddAsync(reservation);
        }
    }

}
