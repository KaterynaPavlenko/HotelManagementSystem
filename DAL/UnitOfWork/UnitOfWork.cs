using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.DAL.Context;
using HotelManagementSystem.DAL.Repository;

namespace HotelManagementSystem.DAL.UnitOfWork
{
     public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelDbContext _hotelDbContext;
        private RoomRepository _roomRepository;
        private BookingRepository _bookingRepository;
        private CustomerRequestRepository _customerRequestRepository;
        private ConfirmationRepository _confirmationRepository;
        private CustomerRequestStatusRepository _customerRequestStatusRepository;
        private RoomTypeRepository _roomTypeRepository;
        private RoomStatusRepository _roomStatusRepository;

        /// <summary>
        ///  Initializes a new instance of the UnitOfWork
        /// </summary>
        /// <param name="hotelDbContext">The context</param>
        public UnitOfWork(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        /// <summary>
        /// Gets the specified repository for the Room
        /// </summary>
        public IRepository<Room> Rooms
        {
            get
            {
                if (_roomRepository == null)
                    _roomRepository = new RoomRepository(_hotelDbContext);
                return _roomRepository;
            }
        }

        /// <summary>
        /// Gets the specified repository for the Booking
        /// </summary>
        public IRepository<Booking> Bookings
        {
            get
            {
                if (_bookingRepository == null)
                    _bookingRepository = new BookingRepository(_hotelDbContext);
                return _bookingRepository;
            }
        }

        /// <summary>
        /// Gets the specified repository for the CustomerRequest
        /// </summary>
        public IRepository<CustomerRequest> CustomerRequests
        {
            get
            {
                if (_customerRequestRepository == null)
                    _customerRequestRepository = new CustomerRequestRepository(_hotelDbContext);
                return _customerRequestRepository;
            }
        }

        /// <summary>
        /// Gets the specified repository for the Confirmation
        /// </summary>
        public IRepository<Confirmation> Confirmations
        {
            get
            {
                if (_confirmationRepository == null)
                    _confirmationRepository = new ConfirmationRepository(_hotelDbContext);
                return _confirmationRepository;
            }
        }

        /// <summary>
        /// Gets the specified repository for the CustomerRequestStatus
        /// </summary>
        public IRepository<CustomerRequestStatus> CustomerRequestStatuses
        {
            get
            {
                if (_customerRequestStatusRepository == null)
                    _customerRequestStatusRepository = new CustomerRequestStatusRepository(_hotelDbContext);
                return _customerRequestStatusRepository;
            }
        }

        /// <summary>
        /// Gets the specified repository for the RoomType
        /// </summary>
        public IRepository<RoomType> RoomTypes
        {
            get
            {
                if (_roomTypeRepository == null)
                    _roomTypeRepository = new RoomTypeRepository(_hotelDbContext);
                return _roomTypeRepository;
            }
        }

        /// <summary>
        ///  Gets the specified repository for the RoomStatus
        /// </summary>
        public IRepository<RoomStatus> RoomStatuses
        {
            get
            {
                if (_roomStatusRepository == null)
                    _roomStatusRepository = new RoomStatusRepository(_hotelDbContext);
                return _roomStatusRepository;
            }
        }

        /// <summary>
        /// Saves all updates to the data source
        /// </summary>
        public void Save()
        {
            _hotelDbContext.SaveChanges();
        }

        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">The disposing</param>
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _hotelDbContext.Dispose();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
