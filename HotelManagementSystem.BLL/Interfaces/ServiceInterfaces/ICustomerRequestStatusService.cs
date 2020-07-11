using System.Collections.Generic;
using HotelManagementSystem.BLL.Data;

namespace HotelManagementSystem.BLL.Interfaces.ServiceInterfaces
{
    public interface ICustomerRequestStatusService
    {
        /// <summary>
        ///     Get all customer requests statuses from the DB
        /// </summary>
        /// <returns>CustomerRequestStatus Collection</returns>
        IEnumerable<CustomerRequestStatus> GetAll();

        /// <summary>
        ///     Get Customer Request statuses by id from the DB
        /// </summary>
        /// <param name="id">CustomerRequestStatus id</param>
        /// <returns></returns>
        CustomerRequestStatus GetById(int id);

        /// <summary>
        ///     Adding a customer request status to the database
        /// </summary>
        /// <param name="customerRequestStatus">CustomerRequestStatus entity</param>
        bool Create(CustomerRequestStatus customerRequestStatus);

        /// <summary>
        ///     Updating a customer request status in the database
        /// </summary>
        /// <param name="customerRequestStatus">CustomerRequestStatus entity</param>
        bool Update(CustomerRequestStatus customerRequestStatus);

        /// <summary>
        ///     Removing a customer request status by id in the database
        /// </summary>
        /// <param name="id">CustomerRequestStatus id</param>
        bool Delete(int id);
    }
}