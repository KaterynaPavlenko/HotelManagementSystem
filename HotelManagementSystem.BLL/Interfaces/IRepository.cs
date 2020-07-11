using System.Collections.Generic;

namespace HotelManagementSystem.BLL.Interfaces
{
    /// <summary>
    ///     Generic repository interface provide all base needed methods
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        ///     Get first entity
        /// </summary>
        /// <returns>T collection</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        ///     Get entity by id
        /// </summary>
        /// <param name="id">T id</param>
        /// <returns>T entity</returns>
        T GetById(int id);

        /// <summary>
        ///     Add T entity
        /// </summary>
        /// <param name="item">T entity</param>
        bool Create(T item);

        /// <summary>
        ///     Update T entity
        /// </summary>
        /// <param name="item">T entity</param>
        bool Update(T item);

        /// <summary>
        ///     Delete T entity
        /// </summary>
        /// <param name="id"> T entity</param>
        bool Delete(int id);
    }
}