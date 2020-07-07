using System.Data.Entity;
using HotelManagementSystem.DAL.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelManagementSystem.DAL.Context
{
    public class HotelDbContext : IdentityDbContext<HotelUserEntity>
    {
        static HotelDbContext()
        {
            Database.SetInitializer(new HotelContextInitializer());
        }

        public IDbSet<RoomTypeEntity> RoomTypes { get; set; }
        public IDbSet<RoomEntity> Rooms { get; set; }
        public IDbSet<BookingEntity> Bookings { get; set; }
        public IDbSet<RoomStatusEntity> RoomStatuses { get; set; }
        public IDbSet<CustomerRequestEntity> CustomerRequests { get; set; }
        public IDbSet<CustomerRequestStatusEntity> CustomerRequestStatuses { get; set; }
        public IDbSet<ConfirmationEntity> Confirmations { get; set; }

        public static HotelDbContext Create()
        {
            return new HotelDbContext();
        }
    }
}