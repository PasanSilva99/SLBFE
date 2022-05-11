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
        /// Gets a list of Available Bureau Officers
        /// </summary>
        /// <returns>Returns a list of Available Bureau Officers</returns>
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
        /// Checking the Officer Details According to the NationalID 
        /// </summary>
        /// <param name="nationalID"></param>
        /// <returns>Returns Officers Details Related to the Given NationalID</returns>
        public List<Models.Bureau> Get(string nationalID)
        {
            var list = new List<Models.Bureau>();
            var bureaulist = Models.DataStore.GetBureaus();

            if (bureaulist != null)
            {
                foreach (var burea in bureaulist)
                {
                    var bur = burea;
                    bur.Password = "";
                    list.Add(bur);
                }

                return list.Where(b => b.NationalID == nationalID).ToList();
            }
            else
            {
                return new List<Models.Bureau>();
            }
        }


        /// <summary>
        /// Get the Officer that Belongs to Entered Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Returns a List Related to the Entered Email</returns>
        [Route("api/FindOfficer")]
        [HttpGet]
        public List<Models.Bureau> GetOfficerFromEmail(string email)
        {
            var list = new List<Models.Bureau>();
            var officerlist = Models.DataStore.GetBureaus();

            if (officerlist != null)
            {
                foreach (var officer in officerlist)
                {
                    var offi = officer;
                    offi.Password = "";
                    list.Add(offi);
                }

                return list.Where(b => b.Email == email).ToList();
            }
            else
            {
                return new List<Models.Bureau>();
            }
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
        /// Updates the Citizen's Validation
        /// </summary>
        /// <param name="validationData"></param>
        /// <returns>Returns the Validated Citizen</returns>
        // POST: api/Bureau
        [Route("api/ValidateUser")]
        [HttpPost]
        public int Post([FromBody] Models.UserValidation validationData)
        {
            return Models.DataStore.ValidateCitizen(validationData);
        }
        
        
        /// <summary>
        /// Checking Whether Entered Email Belongs to an Officer
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Returns Whether the Entered Email is Belongs to a Officer</returns>
        [Route("api/isOfficer")]
        [HttpGet]
        public int isOfficer(string email)
        {
            return Models.DataStore.IsOfficer(email);
        }

        /// <summary>
        /// DELETE Request
        /// </summary>
        /// <param name="nationalID">NationalID of the Officer that Needs to Delete</param>
        /// <param name="requestedBy">The officer that deleted the account</param>
        // DELETE: api/Bureau/5
        public void Delete(string nationalID, string requestedBy)
        {
           Models.DataStore.DeleteOfficer( nationalID, requestedBy);
        }
    }
}
