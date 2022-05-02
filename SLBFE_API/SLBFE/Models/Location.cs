using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLBFE.Models
{
    /// <summary>
    /// This is for storing the location as Latlng
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Creates new location data with values
        /// </summary>
        /// <param name="lat">Latitude as float</param>
        /// <param name="lng">Longitude as float</param>
        public Location(float lat, float lng)
        {
            Latitude = lat;
            Longitude = lng;
        }

        /// <summary>
        /// Latitude Value as float
        /// </summary>
        public float Latitude { get; set; }
        /// <summary>
        /// Longitude Value as float
        /// </summary>
        public float Longitude { get; set; }
    }
}