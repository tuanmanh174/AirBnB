using AirBnB.DTO;
using AirBnB.Model;

namespace AirBnB.Service
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(ReservationDto reservationDto);
    }
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        public async Task<Reservation> CreateReservationAsync(ReservationDto reservationDto)
        {
            var room = await _roomRepository.GetByIdAsync(reservationDto.RoomId);
            if (room == null || !room.IsAvailable)
                throw new Exception("Room is not available.");

            var reservation = new Reservation
            {
                RoomId = reservationDto.RoomId,
                UserId = reservationDto.UserId,
                CheckInDate = reservationDto.CheckInDate,
                CheckOutDate = reservationDto.CheckOutDate,
                Guests = reservationDto.Guests,
                TotalPrice = room.Price * (reservationDto.CheckOutDate - reservationDto.CheckInDate).Days
            };

            return await _reservationRepository.AddAsync(reservation);
        }
    }
}
