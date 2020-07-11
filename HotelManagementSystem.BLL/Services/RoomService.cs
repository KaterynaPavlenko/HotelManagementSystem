using System.Collections.Generic;
using System.Web.ModelBinding;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;

namespace HotelManagementSystem.BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ModelStateDictionary _modelState;

        /// <summary>
        ///     Initializes a new instance of the Unit Of Work
        /// </summary>
        /// <param name="uow"></param>
        public RoomService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        /// <summary>
        ///     Get rooms from the DB
        /// </summary>
        /// <returns>RoomsCollection</returns>
        public IEnumerable<Room> GetAll()
        {
            return _unitOfWork.Rooms.GetAll();
        }

        /// <summary>
        ///     Get rooms by id from the DB
        /// </summary>
        /// <param name="id">Room id</param>
        /// <returns>Room entity</returns>
        public Room GetById(int id)
        {
            return _unitOfWork.Rooms.GetById(id);
        }

        /// <summary>
        ///     Adding a room to the database
        /// </summary>
        /// <param name="room">Room entity</param>
        public bool Create(Room room)
        {
            // Validation 
            if (!ValidateRoom(room))
                return false;

            try
            {
                _unitOfWork.Rooms.Create(room);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Updating a room in the database
        /// </summary>
        /// <param name="room">Room entity</param>
        public bool Update(Room room)
        {
            try
            {
                _unitOfWork.Rooms.Update(room);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Removing a room by id in the database
        /// </summary>
        /// <param name="id">Room id</param>
        public bool Delete(int id)
        {
            try
            {
                _unitOfWork.Rooms.Delete(id);
            }
            catch
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Validation
        /// </summary>
        /// <param name="roomToValidate">Room object</param>
        /// <returns></returns>
        protected bool ValidateRoom(Room roomToValidate)
        {
            if (roomToValidate.RoomType.Trim().Length == 0)
                _modelState.AddModelError("RoomType", "Room type is required.");
            if (roomToValidate.RoomDescription.Trim().Length == 0)
                _modelState.AddModelError("RoomDescription", "Room description is required.");
            if (roomToValidate.RoomStatus.Trim().Length == 0)
                _modelState.AddModelError("RoomStatus", "Room status is required.");
            if (roomToValidate.RoomNumber.Trim().Length == 0)
                _modelState.AddModelError("RoomNumber", "Room number is required.");
            if (roomToValidate.Sleeps < 0)
                _modelState.AddModelError("Sleeps", "Sleeps cannot be less than zero.");
            if (roomToValidate.RoomPriceForOneNight < 0)
                _modelState.AddModelError("RoomPriceForOneNight", "Room price cannot be less than zero.");
            return _modelState.IsValid;
        }
    }
}