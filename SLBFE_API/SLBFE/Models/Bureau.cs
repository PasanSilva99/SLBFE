using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLBFE.Models
{
    /// <summary>
    /// This is the Object Model of Bureau
    /// </summary>
    public class Bureau : User
    {
        /// <summary>
        /// Employee ID of the user
        /// </summary>
        public string EmployeeID { get; set; }

        /// <summary>
        /// File Path of the EmployeeID photo
        /// </summary>
        public string FilePathEmployeeIDPhoto { get; set; }
    }
}