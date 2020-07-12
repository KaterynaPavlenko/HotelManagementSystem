using System.Collections.Generic;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;

namespace HotelManagementSystem.BLL.Services
{
    public class CustomerRequestStatusService : ICustomerRequestStatusService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Initializes a new instance of the Unit Of Work
        /// </summary>
        /// <param name="uow"></param>
        public CustomerRequestStatusService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        /// <summary>
        ///     Get all customer requests statuses from the DB
        /// </summary>
        /// <returns>CustomerRequestStatus Collection</returns>
        public IEnumerable<CustomerRequestStatus> GetAll()
        {
            return _unitOfWork.CustomerRequestStatuses.GetAll();
        }

        /// <summary>
        ///     Get Customer Request statuses by id from the DB
        /// </summary>
        /// <param name="id">CustomerRequestStatus id</param>
        /// <returns></returns>
        public CustomerRequestStatus GetById(int id)
        {
            return _unitOfWork.CustomerRequestStatuses.GetById(id);
        }

        /// <summary>
        ///     Adding a customer request status to the database
        /// </summary>
        /// <param name="customerRequestStatus">CustomerRequestStatus entity</param>
        public bool Create(CustomerRequestStatus customerRequestStatus)
        {
            try
            {
                _unitOfWork.CustomerRequestStatuses.Create(customerRequestStatus);
                _unitOfWork.Save();

            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Updating a customer request status in the database
        /// </summary>
        /// <param name="customerRequestStatus">CustomerRequestStatus entity</param>
        public bool Update(CustomerRequestStatus customerRequestStatus)
        {
            try
            {
                _unitOfWork.CustomerRequestStatuses.Update(customerRequestStatus);
                _unitOfWork.Save();

            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Removing a customer request status by id in the database
        /// </summary>
        /// <param name="id">CustomerRequestStatus id</param>
        public bool Delete(int id)
        {
            try
            {
                _unitOfWork.CustomerRequestStatuses.Delete(id);
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