using System.Collections.Generic;

namespace HotelManagementSystem.DAL.Entity
{
    public class CustomerRequestStatusEntity
    {
        public CustomerRequestStatusEntity()
        {
            CustomerRequestEntities = new List<CustomerRequestEntity>();
        }

        /// <summary>
        ///     Id of the customer request status
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Customer request status name
        /// </summary>
        public string Name { get; set; }

        public ICollection<CustomerRequestEntity> CustomerRequestEntities { get; set; }
    }
}