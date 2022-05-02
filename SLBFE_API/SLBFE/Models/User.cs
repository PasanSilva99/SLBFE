using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLBFE.Models
{
    /// <summary>
    /// This is the general user class
    /// </summary>
    public abstract class User
    {
        /// <summary>
        /// Users National ID
        /// </summary>
        public string NationalId { get; set; }
        /// <summary>
        /// First name of the user
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Emaill Address of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Phone Number of the user
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Birthdate of the user 
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Password Hash of the user
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Address line 1 of the user
        /// </summary>
        public string AddressL1 { get; set; }
        /// <summary>
        /// Address line 2 of the user
        /// </summary>
        public string AddressL2 { get; set; }
        /// <summary>
        /// Users City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Zip Code for the city
        /// </summary>
        public string ZipCode { get; set; }
    }
}