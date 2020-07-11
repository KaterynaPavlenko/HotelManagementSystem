using System.Collections.Generic;
using HotelManagementSystem.BLL.Data;

namespace HotelManagementSystem.BLL.Interfaces.ServiceInterfaces
{
    public interface IConfirmationService
    {
        /// <summary>
        ///     Get all confirmation from the DB
        /// </summary>
        /// <returns>Confirmation Collection</returns>
        IEnumerable<Confirmation> GetAll();

        /// <summary>
        ///     Get confirmation by id from the DB
        /// </summary>
        /// <param name="id">Confirmation id</param>
        Confirmation GetById(int id);

        /// <summary>
        ///     Adding a confirmation to the database
        /// </summary>
        /// <param name="confirmation">Confirmation object</param>
        bool Create(Confirmation confirmation);

        /// <summary>
        ///     Updating a confirmation in the database
        /// </summary>
        /// <param name="confirmation">Confirmation object</param>
        bool Update(Confirmation confirmation);

        /// <summary>
        ///     Removing a confirmation by id in the database
        /// </summary>
        /// <param name="id">Confirmation id</param>
        bool Delete(int id);
    }
}