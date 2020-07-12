using System.Collections.Generic;
using System.Web.ModelBinding;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;

namespace HotelManagementSystem.BLL.Services
{
    public class RoomTypeService : IRoomTypeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelStateDictionary _modelState;

        /// <summary>
        ///     Initializes a new instance of the Unit Of Work
        /// </summary>
        /// <param name="uow"></param>
        public RoomTypeService(IUnitOfWork uow, ModelStateDictionary modelState)
        {
            _unitOfWork = uow;
            _modelState = modelState;
        }

        /// <summary>
        ///     Get all room types from the DB
        /// </summary>
        /// <returns>Room type collection</returns>
        public IEnumerable<RoomType> GetAll()
        {
            return _unitOfWork.RoomTypes.GetAll();
        }

        /// <summary>
        ///     Get room type by id from the DB
        /// </summary>
        /// <param name="id">Room type id</param>
        /// <returns>Room type object</returns>
        public RoomType GetById(int id)
        {
            return _unitOfWork.RoomTypes.GetById(id);
        }

        /// <summary>
        ///     Adding a room type to the database
        /// </summary>
        /// <param name="roomType">RoomType object</param>
        public bool Create(RoomType roomType)
        {
            // Validation 
            if (!ValidateRoomType(roomType))
                return false;

            try
            {
                _unitOfWork.RoomTypes.Create(roomType);
                _unitOfWork.Save();

            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Updating a room type in the database
        /// </summary>
        /// <param name="roomType">RoomType object</param>
        public bool Update(RoomType roomType)
        {
            try
            {
                _unitOfWork.RoomTypes.Update(roomType);
                _unitOfWork.Save();

            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Removing a room type by id in the database
        /// </summary>
        /// <param name="id">Room Type id</param>
        public bool Delete(int id)
        {
            try
            {
                _unitOfWork.RoomTypes.Delete(id);
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
        /// <param name="roomTypeToValidate">RoomType object</param>
        /// <returns></returns>
        protected bool ValidateRoomType(RoomType roomTypeToValidate)
        {
            if (roomTypeToValidate.Name.Trim().Length == 0)
                _modelState.AddModelError("Name", "Name is required.");
            return _modelState.IsValid;
        }
    }
}