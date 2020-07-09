using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementSystem.BLL.Data;

namespace HotelManagementSystem.BLL.Interfaces
{
    /// <summary>
    /// Defines the interface for  unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the specified repository for the Room
        /// </summary>
        IRepository<Room> Rooms { get; }
        /// <summary>
        /// Gets the specified repository for the Booking
        /// </summary>
        IRepository<Booking> Bookings { get; }
        /// <summary>
        /// Gets the specified repository for the CustomerRequest
        /// </summary>
        IRepository<CustomerRequest> CustomerRequests { get; }
        /// <summary>
        /// Gets the specified repository for the Confirmation
        /// </summary>
        IRepository<Confirmation> Confirmations { get; }
        /// <summary>
        /// Gets the specified repository for the CustomerRequestStatus
        /// </summary>
        IRepository<CustomerRequestStatus> CustomerRequestStatuses { get; }
        /// <summary>
        /// Gets the specified repository for the RoomType
        /// </summary>
        IRepository<RoomType> RoomTypes { get; }
        /// <summary>
        /// Gets the specified repository for the RoomStatus
        /// </summary>
        IRepository<RoomStatus> RoomStatuses { get; }
        /// <summary>
        /// Saves all updates to the data source
        /// </summary>
        void Save();
    }
}
