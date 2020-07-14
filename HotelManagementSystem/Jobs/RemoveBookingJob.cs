using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;
using Quartz;

namespace HotelManagementSystem.Jobs
{
    public class RemoveBookingJob : IJob
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        private readonly IRoomStatusService _roomStatusService;

        public RemoveBookingJob(IBookingService bookingService, IRoomService roomService,
            IRoomStatusService roomStatusService)
        {
            _bookingService = bookingService;
            _roomService = roomService;
            _roomStatusService = roomStatusService;
        }

        Task IJob.Execute(IJobExecutionContext context)
        {
            var bookingId = int.Parse(context.Trigger.Key.Name);
            var booking = _bookingService.GetById(bookingId);
            var room = _roomService.GetById(booking.RoomId);
            room.RoomStatusId = _roomStatusService.GetAll().First(roomStatus => roomStatus.Name == "Free").Id;
            if (booking.Payment == false
            ) //If the reservation is not paid, it is deleted and the room status changes to "Free"
            {
                _bookingService.Delete(bookingId);
                _roomService.Update(room);
            }

            return Task.CompletedTask;
        }
    }
}