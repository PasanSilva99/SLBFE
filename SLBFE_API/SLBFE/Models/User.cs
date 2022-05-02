using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLBFE.Models
{
    /// <summary>
    /// This is the General User Class
    /// </summary>
    public abstract class User
    {
        /// <summary>
        /// User's National ID
        /// </summary>
        public string NationalID { get; set; }
        /// <summary>
        /// First name of the user
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Last name of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Phone Number of the user
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// BirthDate of the user
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Password of the user
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Adress Line 1 of the user
        /// </summary>
        public string AddressL1 { get; set; }
        /// <summary>
        /// Adress Line 2 of the user
        /// </summary>
        public string AddressL2 { get; set; }
        /// <summary>
        /// State or Province of the user
        /// </summary>
        public string StateProvince { get; set; }
        /// <summary>
        /// city of the user
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Zip code of the user
        /// </summary>
        public string ZipCode { get; set; }

    }
}