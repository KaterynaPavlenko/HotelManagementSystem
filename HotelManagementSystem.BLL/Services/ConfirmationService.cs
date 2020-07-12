using System.Collections.Generic;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;

namespace HotelManagementSystem.BLL.Services
{
    public class ConfirmationService : IConfirmationService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Initializes a new instance of the Unit Of Work
        /// </summary>
        /// <param name="uow"></param>
        public ConfirmationService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }


        /// <summary>
        ///     Get all confirmation from the DB
        /// </summary>
        /// <returns>Confirmation Collection</returns>
        public IEnumerable<Confirmation> GetAll()
        {
            return _unitOfWork.Confirmations.GetAll();
        }

        /// <summary>
        ///     Get confirmation by id from the DB
        /// </summary>
        /// <param name="id">Confirmation id</param>
        /// <returns>Confirmation entity</returns>
        public Confirmation GetById(int id)
        {
            return _unitOfWork.Confirmations.GetById(id);
        }

        /// <summary>
        ///     Adding a confirmation to the database
        /// </summary>
        /// <param name="confirmation">Confirmation entity</param>
        public bool Create(Confirmation confirmation)
        {
            try
            {
                _unitOfWork.Confirmations.Create(confirmation);
                _unitOfWork.Save();

            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Updating a confirmation in the database
        /// </summary>
        /// <param name="confirmation">Confirmation entity</param>
        public bool Update(Confirmation confirmation)
        {
            try
            {
                _unitOfWork.Confirmations.Update(confirmation);
                _unitOfWork.Save();

            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Removing a confirmation by id in the database
        /// </summary>
        /// <param name="id">Confirmation id</param>
        public bool Delete(int id)
        {
            try
            {
                _unitOfWork.Confirmations.Delete(id);
                _unitOfWork.Save();

            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}