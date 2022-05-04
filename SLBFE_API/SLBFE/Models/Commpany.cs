using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLBFE.Models
{
    /// <summary>
    /// This is the General Company Class(like Structures)
    /// </summary>
    public class Commpany 
    {
        /// <summary>
        /// Commpany Registeration Number
        /// </summary>
        public string BRNumber { get; set; }

        /// <summary>
        /// Commpany Registeration Docs
        /// </summary>
        public string FilePathBR { get; set; }

        /// <summary>
        /// Commpany Name
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// Commpany Category
        /// </summary>
        public string BusinessCategory { get; set; }

        /// <summary>
        /// Commpany Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Commpany Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Commpany Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Commpany Address first Line
        /// </summary>
        public string AddressL1 { get; set; }

        /// <summary>
        /// Commpany Address secound Line
        /// </summary>
        public string AddressL2 { get; set; }

        /// <summary>
        /// Commpany State or Province
        /// </summary>
        public string StateProvince { get; set; }

        /// <summary>
        /// Commpany City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Commpany Zip Code
        /// </summary>
        public string ZipCode { get; set; }
    }
}