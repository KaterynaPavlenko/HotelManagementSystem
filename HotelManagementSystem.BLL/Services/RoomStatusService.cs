using System.Collections.Generic;
using System.Web.ModelBinding;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;

namespace HotelManagementSystem.BLL.Services
{
    public class RoomStatusService : IRoomStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelStateDictionary _modelState;

        /// <summary>
        ///     Initializes a new instance of the Unit Of Work
        /// </summary>
        /// <param name="uow"></param>
        public RoomStatusService(IUnitOfWork uow, ModelStateDictionary modelState)
        {
            _modelState = modelState;
            _unitOfWork = uow;
        }

        /// <summary>
        ///     Get all room statuses from the DB
        /// </summary>
        /// <returns>Room Statuses Collection</returns>
        public IEnumerable<RoomStatus> GetAll()
        {
            return _unitOfWork.RoomStatuses.GetAll();
        }

        /// <summary>
        ///     Get Customer Request by id from the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoomStatus GetById(int id)
        {
            return _unitOfWork.RoomStatuses.GetById(id);
        }

        /// <summary>
        ///     Adding a room status to the database
        /// </summary>
        /// <param name="roomStatus">RoomStatus object</param>
        public bool Create(RoomStatus roomStatus)
        {
            // Validation 
            if (!ValidateRoomStatus(roomStatus))
                return false;

            try
            {
                _unitOfWork.RoomStatuses.Create(roomStatus);
                _unitOfWork.Save();

            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Updating a room status in the database
        /// </summary>
        /// <param name="roomStatus">RoomStatus entity</param>
        public bool Update(RoomStatus roomStatus)
        {
            try
            {
                _unitOfWork.RoomStatuses.Update(roomStatus);
                _unitOfWork.Save();

            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Removing a room status by id in the database
        /// </summary>
        /// <param name="id">Room Status id</param>
        public bool Delete(int id)
        {
            try
            {
                _unitOfWork.RoomStatuses.Delete(id);
                _unitOfWork.Save();

            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Validation
        /// </summary>
        /// <param name="roomStatusToValidate">RoomStatus object</param>
        /// <returns></returns>
        protected bool ValidateRoomStatus(RoomStatus roomStatusToValidate)
        {
            if (roomStatusToValidate.Name.Trim().Length == 0)
                _modelState.AddModelError("Name", "Name is required.");
            return _modelState.IsValid;
        }
    }
}