﻿using System;
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
        // GET: api/Citizen/5
        public List<Models.Citizen> Get()
        {
            var list = new List<Models.Citizen>();
            var citizenlist = Models.DataStore.GetCitizens();

            foreach (var citizen in citizenlist)
            {
                var cit = citizen;
                cit.Password = "";
                list.Add(cit);
            }

            return list;
        }

        /// <summary>
        /// POST Request 
        /// Register new Citizen
        /// </summary>
        /// <param name="value">Citizen Data as an Single Citizen Object</param>
        // POST: api/Citizen
        public void Post([FromBody]Models.Citizen value)
        {
            Models.DataStore.RegisterCitizen(value);
        }

        /// <summary>
        /// DELETE Requst
        /// Delete a Citizen from the database
        /// </summary>
        /// <param name="nationalID">National ID of the Citizen that needs to be deleted</param>
        /// <param name="requestedBy">The Officer that deleted the account</param>
        // DELETE: api/Citizen/5
        public void Delete(string nationalID, string requestedBy)
        {
            Models.DataStore.DeleteCitizen(nationalID, requestedBy);
        }
    }
}