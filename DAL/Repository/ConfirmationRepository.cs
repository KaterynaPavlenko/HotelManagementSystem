using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagementSystem.BLL.Data;
using HotelManagementSystem.BLL.Interfaces;
using HotelManagementSystem.DAL.Context;
using HotelManagementSystem.DAL.Entity;

namespace HotelManagementSystem.DAL.Repository
{
    public class ConfirmationRepository : IRepository<Confirmation>
    {
        private readonly HotelDbContext _hotelDbContext;

        /// <summary>
        ///     Initializes a new instance of the RoomTypeRepository
        /// </summary>
        /// <param name="context">The context</param>
        public ConfirmationRepository(HotelDbContext context)
        {
            _hotelDbContext = context;
        }

        /// <summary>
        ///     Get all confirmation from the DB
        /// </summary>
        /// <returns>Confirmation Collection</returns>
        public IEnumerable<Confirmation> GetAll()
        {
            var entities = _hotelDbContext.Confirmations.Include(x => x.CustomerRequest).Include(x => x.Room).ToList();
            var confirmations = new List<Confirmation>();
            foreach (var confirmationEntity in entities)
            {
                var confirmation = new Confirmation(confirmationEntity.Id, confirmationEntity.RoomId,
                    confirmationEntity.CustomerRequestId);
                confirmations.Add(confirmation);
            }

            return confirmations;
        }

        /// <summary>
        ///     Get confirmation by id from the DB
        /// </summary>
        /// <param name="id">Confirmation id</param>
        /// <returns>Confirmation entity</returns>
        public Confirmation GetById(int id)
        {
            var confirmationEntity = _hotelDbContext.Confirmations.Find(id);
            var confirmation = new Confirmation
            {
                Id = confirmationEntity.CustomerRequestId,
                CustomerRequestId = confirmationEntity.CustomerRequestId,
                RoomId = confirmationEntity.RoomId
            };
            return confirmation;
        }

        /// <summary>
        ///     Adding a confirmation to the database
        /// </summary>
        /// <param name="confirmation">Confirmation entity</param>
        public void Create(Confirmation confirmation)
        {
            var entity = new ConfirmationEntity
            {
                Id = confirmation.CustomerRequestId,
                CustomerRequestId = confirmation.CustomerRequestId,
                RoomId = confirmation.RoomId
            };
            _hotelDbContext.Confirmations.Add(entity);
            _hotelDbContext.SaveChanges();
        }

        /// <summary>
        ///     Updating a confirmation in the database
        /// </summary>
        /// <param name="confirmation">Confirmation entity</param>
        public void Update(Confirmation confirmation)
        {
            var entity = new Confirmation
            {
                Id = confirmation.CustomerRequestId,
                CustomerRequestId = confirmation.CustomerRequestId,
                RoomId = confirmation.RoomId
            };
            _hotelDbContext.Entry(entity).State = EntityState.Modified;
            _hotelDbContext.SaveChanges();
        }

        /// <summary>
        ///     Removing a confirmation by id in the database
        /// </summary>
        /// <param name="id">Confirmation id</param>
        public void Delete(int id)
        {
            var confirmationEntity = _hotelDbContext.Confirmations.Find(id);
            if (confirmationEntity != null)
                _hotelDbContext.Confirmations.Remove(confirmationEntity);
            _hotelDbContext.SaveChanges();
        }
    }
}