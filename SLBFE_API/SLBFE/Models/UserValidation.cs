using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLBFE.Models
{

    /// <summary>
    /// The user validation Data object
    /// </summary>
    public class UserValidation
    {
        /// <summary>
        /// National ID of the user
        /// </summary>
        public string NationalID { get; set; }

        /// <summary>
        /// Is this account approoved?
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Is there any changes to be made?
        /// </summary>
        public string Changes { get; set; }

        /// <summary>
        /// Who checked this account
        /// </summary>
        public string EmployeeID { get; set; }
    }
}