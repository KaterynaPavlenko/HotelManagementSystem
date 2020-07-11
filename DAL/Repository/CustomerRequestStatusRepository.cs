using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.DAL.Context;
using HotelManagementSystem.DAL.Entity;

namespace HotelManagementSystem.DAL.Repository
{
    public class CustomerRequestStatusRepository : IRepository<CustomerRequestStatus>
    {
        private readonly HotelDbContext _hotelDbContext;

        /// <summary>
        ///     Initializes a new instance of the CustomerRequestStatusRepository
        /// </summary>
        /// <param name="context">The context</param>
        public CustomerRequestStatusRepository(HotelDbContext context)
        {
            _hotelDbContext = context;
        }

        /// <summary>
        ///     Get all customer requests statuses from the DB
        /// </summary>
        /// <returns>CustomerRequestStatus Collection</returns>
        public IEnumerable<CustomerRequestStatus> GetAll()
        {
            var entities = _hotelDbContext.CustomerRequestStatuses.ToList();
            var customerRequestStatuses = new List<CustomerRequestStatus>();
            foreach (var customerRequestStatusEntity in entities)
            {
                var status =
                    new CustomerRequestStatus(customerRequestStatusEntity.Id, customerRequestStatusEntity.Name);

                customerRequestStatuses.Add(status);
            }

            return customerRequestStatuses;
        }

        /// <summary>
        ///     Get Customer Request statuses by id from the DB
        /// </summary>
        /// <param name="id">CustomerRequestStatus id</param>
        /// <returns></returns>
        public CustomerRequestStatus GetById(int id)
        {
            var customerRequestStatusEntity = _hotelDbContext.CustomerRequestStatuses.Find(id);
            var customerRequestStatus =
                new CustomerRequestStatus(customerRequestStatusEntity.Id, customerRequestStatusEntity.Name);
            return customerRequestStatus;
        }

        /// <summary>
        ///     Adding a customer request status to the database
        /// </summary>
        /// <param name="customerRequestStatus">CustomerRequestStatus entity</param>
        public bool Create(CustomerRequestStatus customerRequestStatus)
        {
            try
            {
                var entity = new CustomerRequestStatusEntity
                {
                    Id = customerRequestStatus.Id,
                    Name = customerRequestStatus.Name
                };
                _hotelDbContext.CustomerRequestStatuses.Add(entity);
                _hotelDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
          
        }

        /// <summary>
        ///     Updating a customer request status in the database
        /// </summary>
        /// <param name="customerRequestStatus">CustomerRequestStatus entity</param>
        public bool Update(CustomerRequestStatus customerRequestStatus)
        {
            try
            {
                var entity = new CustomerRequestStatusEntity
                {
                    Id = customerRequestStatus.Id,
                    Name = customerRequestStatus.Name
                };
                _hotelDbContext.CustomerRequestStatuses.AddOrUpdate(entity);
                _hotelDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
          
        }

        /// <summary>
        ///     Removing a customer request status by id in the database
        /// </summary>
        /// <param name="id">CustomerRequestStatus id</param>
        public bool Delete(int id)
        {
            try
            {
                var status = _hotelDbContext.CustomerRequestStatuses.Find(id);
                if (status != null)
                    _hotelDbContext.CustomerRequestStatuses.Remove(status);
                _hotelDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}