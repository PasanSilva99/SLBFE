using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SLBFE.Controllers
{
    /// <summary>
    /// This Controller contains the functions for the Citizens. 
    /// </summary>
    public class CitizenController : ApiController
    {
        //// GET: api/Citizen
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        /// <summary>
        /// Gets a list of available Citizens
        /// </summary>
        /// <returns></returns>
        public List<Models.Citizen> Get()
        {
            var list = new List<Models.Citizen>();
            var citizenlist = Models.DataStore.GetCitizens();

            if (citizenlist != null)
            {
                foreach (var citizen in citizenlist)
                {
                    var cit = citizen;
                    cit.Password = "";
                    list.Add(cit);
                }

                return list;
            }
            else
            {
                return new List<Models.Citizen>();
            }
        }

        public List<Models.Citizen> Get(string nationalID)
        {
            var list = new List<Models.Citizen>();
            var citizenlist = Models.DataStore.GetCitizens();

            if (citizenlist != null)
            {
                foreach (var citizen in citizenlist)
                {
                    var cit = citizen;
                    cit.Password = "";
                    list.Add(cit);
                }

                return list.Where(c => c.NationalID == nationalID).ToList();
            }
            else
            {
                return new List<Models.Citizen>();
            }
        }

        /// <summary>
        /// Validates the user 
        /// </summary>
        /// <param name="data">Login Data</param>
        /// <returns></returns>
        [Route("api/Citizen/Register")]
        [HttpGet]
        public int CitizenLogin([FromBody] Models.LoginData data)
        {
            return 210;
        }

        /// <summary>
        /// POST Request 
        /// Register new Citizen
        /// </summary>
        /// <param name="value">Citizen Data as an Single Citizen Object</param>
        public int Post([FromBody] Models.Citizen value)
        {
            Models.DataStore.RegisterCitizen(value);
            return 200;
        }

        /// <summary>
        /// POST Request 
        /// Register new Citizen
        /// </summary>
        /// <param name="value">Citizen Data as an Single Citizen Object</param>
        /// <param name="nationalID">National ID number of the Citizen that needs to be updated</param>
        public void Post(string nationalID, [FromBody] Models.Citizen value)
        {
            Models.DataStore.UpdateCitizen(nationalID, value);
        }

        /// <summary>
        /// DELETE Requst
        /// Delete a Citizen from the database
        /// </summary>
        /// <param name="nationalID">National ID of the Citizen that needs to be deleted</param>
        /// <param name="requestedBy">The Officer that deleted the account</param>
        public void Delete(string nationalID, string requestedBy)
        {
            Models.DataStore.DeleteCitizen(nationalID, requestedBy);
        }
    }
}
