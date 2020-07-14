using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;
using HotelManagementSystem.BLL.Services;
using Ninject.Modules;

namespace HotelManagementSystem.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IRoomService>().To<RoomService>();
            Bind<ICustomerRequestServices>().To<CustomerRequestService>();
            Bind<IRoomTypeServices>().To<RoomTypeService>();
            Bind<IRoomStatusService>().To<RoomStatusService>();
            Bind<ICustomerRequestServices>().To<CustomerRequestService>();
            Bind<ICustomerRequestStatusService>().To<CustomerRequestStatusService>();
            Bind<IBookingService>().To<BookingService>();
        }
    }
}