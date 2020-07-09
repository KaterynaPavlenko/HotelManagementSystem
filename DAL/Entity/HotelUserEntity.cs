using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelManagementSystem.DAL.Entity
{
    public class HotelUserEntity : IdentityUser
    {
        /// <summary>
        ///     The first name of the customer
        /// </summary>
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        /// <summary>
        ///     The last name of the customer
        /// </summary>

        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        /// <summary>
        ///     Delete flag
        /// </summary>
        public bool IsDeleted { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<HotelUserEntity> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}