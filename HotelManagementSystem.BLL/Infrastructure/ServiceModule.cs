using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;
using HotelManagementSystem.BLL.Services;
using Ninject.Modules;

namespace HotelManagementSystem.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private readonly string connectionString;

        public ServiceModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IRoomService>().To<RoomService>().WithConstructorArgument(connectionString);
            Bind<IRoomTypeServices>().To<RoomTypeService>().WithConstructorArgument(connectionString);
            Bind<IRoomStatusService>().To<RoomStatusService>().WithConstructorArgument(connectionString);
            Bind<ICustomerRequestServices>().To<CustomerRequestService>().WithConstructorArgument(connectionString);
            Bind<IBookingService>().To<BookingService>().WithConstructorArgument(connectionString);
            Bind<ICustomerRequestStatusService>().To<CustomerRequestStatusService>()
                .WithConstructorArgument(connectionString);
            Bind<IConfirmationService>().To<ConfirmationService>().WithConstructorArgument(connectionString);
        }
    }
}
