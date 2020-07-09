﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.DAL.Context;
using HotelManagementSystem.DAL.Entity;

namespace HotelManagementSystem.DAL.Repository
{
    public class CustomerRequestRepository : IRepository<CustomerRequest>

    {
        private readonly HotelDbContext _hotelDbContext;

        /// <summary>
        ///     Initializes a new instance of the CustomerRequestRepository
        /// </summary>
        /// <param name="context">The context</param>
        public CustomerRequestRepository(HotelDbContext context)
        {
            _hotelDbContext = context;
        }

        /// <summary>
        ///     Get all customer requests from the DB
        /// </summary>
        /// <returns>Customer Requests Collection</returns>
        public IEnumerable<CustomerRequest> GetAll()
        {
            var entities = _hotelDbContext.CustomerRequests.Include(x => x.RoomType).Include(y => y.HotelUser)
                .Include(x => x.Confirmation)
                .Include(x => x.CustomerRequestStatus).ToList();
            var requests = new List<CustomerRequest>();
            foreach (var requestEntity in entities)
            {
                var request = new CustomerRequest
                {
                    Id = requestEntity.Id,
                    DateFrom = requestEntity.DateFrom,
                    DateTo = requestEntity.DateTo,
                    Sleeps = requestEntity.Sleeps,
                    RoomTypeId = requestEntity.RoomTypeId,
                    HotelUserId = requestEntity.HotelUserId,
                    CustomerRequestStatusId = requestEntity.CustomerRequestStatusId,
                    CustomerRequestStatus = requestEntity.CustomerRequestStatus.Name,
                    RoomType = requestEntity.RoomType.Name
                };
                requests.Add(request);
            }

            return requests;
        }

        /// <summary>
        ///     Get Customer Request by id from the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CustomerRequest entity</returns>
        public CustomerRequest GetById(int id)
        {
            var requestEntity = _hotelDbContext.CustomerRequests.Find(id);
            var request = new CustomerRequest
            {
                Id = requestEntity.Id,
                DateFrom = requestEntity.DateFrom,
                DateTo = requestEntity.DateTo,
                Sleeps = requestEntity.Sleeps,
                RoomTypeId = requestEntity.RoomTypeId,
                HotelUserId = requestEntity.HotelUserId,
                CustomerRequestStatusId = requestEntity.CustomerRequestStatusId,
                CustomerRequestStatus = requestEntity.CustomerRequestStatus.Name,
                RoomType = requestEntity.RoomType.Name
            };
            return request;
        }

        /// <summary>
        ///     Adding a customer request to the database
        /// </summary>
        /// <param name="customerRequest">CustomerRequest entity</param>
        public void Create(CustomerRequest customerRequest)
        {
            var entity = new CustomerRequestEntity
            {
                Id = customerRequest.Id,
                DateFrom = customerRequest.DateFrom,
                DateTo = customerRequest.DateTo,
                HotelUserId = customerRequest.HotelUserId,
                RoomTypeId = customerRequest.RoomTypeId,
                CustomerRequestStatusId = customerRequest.CustomerRequestStatusId,
                Sleeps = customerRequest.Sleeps
            };
            _hotelDbContext.CustomerRequests.Add(entity);
            _hotelDbContext.SaveChanges();
        }

        /// <summary>
        ///     Updating a customer request in the database
        /// </summary>
        /// <param name="customerRequest">CustomerRequest entity</param>
        public void Update(CustomerRequest customerRequest)
        {
            var entity = new CustomerRequestEntity
            {
                Id = customerRequest.Id,
                DateFrom = customerRequest.DateFrom,
                DateTo = customerRequest.DateTo,
                HotelUserId = customerRequest.HotelUserId,
                RoomTypeId = customerRequest.RoomTypeId,
                CustomerRequestStatusId = customerRequest.CustomerRequestStatusId,
                Sleeps = customerRequest.Sleeps
            };
            _hotelDbContext.CustomerRequests.AddOrUpdate(entity);
            _hotelDbContext.SaveChanges();
        }

        /// <summary>
        ///     Removing a customer request by id in the database
        /// </summary>
        /// <param name="id">CustomerRequest id</param>
        public void Delete(int id)
        {
            var customerRequestEntity = _hotelDbContext.CustomerRequests.Find(id);
            if (customerRequestEntity != null)
                _hotelDbContext.CustomerRequests.Remove(customerRequestEntity);
            _hotelDbContext.SaveChanges();
        }
    }
}