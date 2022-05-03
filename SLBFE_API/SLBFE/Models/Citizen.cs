using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLBFE.Models
{
    /// <summary>
    /// This is the Object Model For citizen
    /// </summary>
    public class Citizen : User
    { 
        /// <summary>
        /// Location of the map as Location{Latitudes, Longitudes}
        /// </summary>
        public Location MapLocation { get; set; }
        /// <summary>
        /// Current profession if exisist Default is Empty String
        /// </summary>
        public string CurrentProfession { get; set; } = "";
        /// <summary>
        /// Current Affiliation if exisist Default is Empty String
        /// </summary>
        public string Affiliation { get; set; } = "";
        /// <summary>
        /// Qialification list of the users
        /// </summary>
        public List<string> Qualifications { get; set; } 
        /// <summary>
        /// File path of the users CV
        /// </summary>
        public string FilePathCV { get; set; }
        /// <summary>
        /// File Path of the users other qualification documents Default is an Empty List of String
        /// </summary>
        public List<string> FilePathQualifications { get; set; } = new List<string>();
    }
}