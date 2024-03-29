﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLBFE.Models
{
    /// <summary>
    /// This handles tha data from the login page
    /// </summary>
    public class LoginData
    {
        /// <summary>
        /// Username of the user
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Password has of the user entered
        /// </summary>
        public string Passwordhash { get; set; }

        /// <summary>
        /// IP address of the user
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Detected country of the user
        /// </summary>
        public string Country { get; set; }
    }
}