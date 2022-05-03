using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SLBFE.Controllers
{
    /// <summary>
    /// This Controller contains the functions for the Bureau.
    /// </summary>
    public class BureauController : ApiController
    {
        //// GET: api/Bureau
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        /// <summary>
        /// Gets a list of available Bureau Officers
        /// </summary>
        /// <returns></returns>
        // GET: api/Bureau/5
        public List<Models.Bureau> Get()
        {
            var list = new List<Models.Bureau>();
            var bureaulist = Models.DataStore.GetBureaus();
            
            foreach (var burea in bureaulist)
            {
                var bur = burea;
                bur.Password = "";
                list.Add(bur);
            }
            return list;

        }

        /// <summary>
        /// POST Request
        /// </summary>
        /// <param name="value">Officer data as an single Officer object</param>
        
        // POST: api/Bureau
        public void Post([FromBody]Models.Bureau value)
        {
            Models.DataStore.RegisterOfficer(value);
        }

        /// <summary>
        /// POST Request
        /// </summary>
        /// <param name="value">Officer data as an single Officer object</param>
        /// <param name="employeeID">Officer data as an single Officer object</param>

        // POST: api/Bureau
        public void Post(string employeeID, [FromBody] Models.Bureau value)
        {
            Models.DataStore.UpdateOfficer(employeeID, value);
        }


        /// <summary>
        /// DELETE Request
        /// </summary>
        /// <param name="nationalID">National ID of the officer that need to deleted</param>
        /// <param name="requestedBy">The officer that deleted the account</param>
        // DELETE: api/Bureau/5
        public void Delete(string nationalID, string requestedBy)
        {
           Models.DataStore.DeleteOfficer( nationalID, requestedBy);
        }
    }
}
