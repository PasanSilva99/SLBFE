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
        /// <summary>
        /// This is intended for debugging purposes. 
        /// This will return the saved log of this server
        /// </summary>
        /// <returns>Complete Log File</returns>
        [Route("api/ViewLog")]
        [HttpGet]
        public string[] ViewLog()
        {
            return Models.DataStore.GetLog();
        }

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
        /// Retuns the citizen that belongs this email
        /// </summary>
        /// <param name="email">Email of the citizen</param>
        /// <returns></returns>
        [Route("api/FindCitizen")]
        [HttpGet]
        public List<Models.Citizen> GetCirizenFromEmail(string email)
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

                return list.Where(c => c.Email == email).ToList();
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
        [Route("api/Citizen/Login")]
        [HttpPost]
        public int CitizenLogin([FromBody] Models.LoginData data)
        {
            var citizens = Models.DataStore.GetCitizens();
            var isCitizenValid = citizens.Where(citizen => citizen.Email == data.Email && citizen.Password == data.Passwordhash).Any();

            return isCitizenValid ? 1 : 0;
        }

        [Route("api/isCitizen")]
        [HttpGet]
        public int isCitizen(string email)
        {
            return Models.DataStore.IsCitizen(email);
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
