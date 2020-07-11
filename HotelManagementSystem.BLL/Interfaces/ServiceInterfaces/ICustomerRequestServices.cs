using System.Collections.Generic;
using HotelManagementSystem.BLL.Data;

namespace HotelManagementSystem.BLL.Interfaces.ServiceInterfaces
{
    public interface ICustomerRequestServices
    {
        /// <summary>
        ///     Get all customer requests from the DB
        /// </summary>
        /// <returns>Customer Requests Collection</returns>
        IEnumerable<CustomerRequest> GetAll();

        /// <summary>
        ///     Get Customer Request by id from the DB
        /// </summary>
        /// <param name="id">Customer Request id</param>
        /// <returns>CustomerRequest object</returns>
        CustomerRequest GetById(int id);

        /// <summary>
        ///     Adding a customer request to the database
        /// </summary>
        /// <param name="customerRequest">CustomerRequest entity</param>
        bool Create(CustomerRequest customerRequest);

        /// <summary>
        ///     Updating a customer request in the database
        /// </summary>
        /// <param name="customerRequest">CustomerRequest entity</param>
        bool Update(CustomerRequest customerRequest);

        /// <summary>
        ///     Removing a customer request by id in the database
        /// </summary>
        /// <param name="id">CustomerRequest id</param>
        bool Delete(int id);
    }
}