using System.Collections.Generic;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.BLL.Interfaces.ServiceInterfaces;

namespace HotelManagementSystem.BLL.Services
{
    public class CustomerRequestService : ICustomerRequestServices
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Initializes a new instance of the Unit Of Work
        /// </summary>
        /// <param name="uow"></param>
        public CustomerRequestService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }


        /// <summary>
        ///     Get all customer requests from the DB
        /// </summary>
        /// <returns>Customer Requests Collection</returns>
        public IEnumerable<CustomerRequest> GetAll()
        {
            return _unitOfWork.CustomerRequests.GetAll();
        }

        /// <summary>
        ///     Get Customer Request by id from the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CustomerRequest entity</returns>
        public CustomerRequest GetById(int id)
        {
            return _unitOfWork.CustomerRequests.GetById(id);
        }

        /// <summary>
        ///     Adding a customer request to the database
        /// </summary>
        /// <param name="customerRequest">CustomerRequest entity</param>
        public bool Create(CustomerRequest customerRequest)
        {
            try
            {
                _unitOfWork.CustomerRequests.Create(customerRequest);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Updating a customer request in the database
        /// </summary>
        /// <param name="customerRequest">CustomerRequest entity</param>
        public bool Update(CustomerRequest customerRequest)
        {
            try
            {
                _unitOfWork.CustomerRequests.Update(customerRequest);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Removing a customer request by id in the database
        /// </summary>
        /// <param name="id">CustomerRequest id</param>
        public bool Delete(int id)
        {
            try
            {
                _unitOfWork.CustomerRequests.Delete(id);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}