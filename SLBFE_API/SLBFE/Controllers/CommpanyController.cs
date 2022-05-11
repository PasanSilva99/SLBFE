using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SLBFE.Controllers
{
    public class CommpanyController : ApiController
    {
        //// GET: api/Commpany
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}


        /// <summary>
        /// Gets a List of Available Commpanies
        /// </summary>
        /// <returns>Returns a List of Companies in the System</returns>
        // GET: api/Commpany/5
        public List<Models.Commpany> Get()
        {
            var list = new List<Models.Commpany>();
            var commpanyList = Models.DataStore.GetCommpany();

            foreach (var commpany in commpanyList)
            {
                var comp = commpany; 
                comp.Password = "";
                list.Add(comp); // set company name and things to company list
            }

            return list;
        }

        /// <summary>
        /// Get a Company Belongs to Entered Email 
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Return the Related Company Belongs to that Entered Email If Exist</returns>
        public List<Models.Commpany> Get(string email)
        {
            var list = new List<Models.Commpany>();
            var companylist = Models.DataStore.GetCommpany(); // create list for adding data belongs company email

            if (companylist != null) 
            {
                foreach (var company in companylist)
                {
                    var comp = company;
                    comp.Password = "";
                    list.Add(comp);
                }

                return list.Where(c => c.Email == email).ToList(); // find every thing belongs emali which givens.
            }
            else
            {
                return new List<Models.Commpany>();
            }
        }

        /// <summary>
        /// Search Citizen by qualifications
        /// </summary>
        /// <param name="quary">Qualification quary</param>
        /// <returns></returns>
        [Route("api/SearchCitizen")]
        [HttpGet]
        public List<Models.Citizen> SearchCitizens(string quary)
        {
            var citizenList = Models.DataStore.GetCitizens();

            var matchedList = new List<Models.Citizen>(); // create list for store matched citizens

            if (citizenList != null && citizenList.Count > 0)
            {
                foreach (var citizen in citizenList)
                {
                    var qualifications = citizen.Qualifications; // store citizen qualifaction to qualifaction veriable  wich come form the citizen models

                    if (quary != null && qualifications.Where(q => q.Contains(quary)).Any()) // matching string comming form para to  qualifications veiabel values
                    {
                        matchedList.Add(citizen); 
                    }
                }
            }
            return matchedList;
        }


        /// <summary>
        /// Registering a new company 
        /// </summary>
        /// <param name="value">Commpany Data as an Single Commpany Object</param>
        // POST: api/Commpany
        public int Post([FromBody] Models.Commpany value)
        {
            Models.DataStore.RegisterCommpany(value);
            return 200;
        }


        /// <summary>
        /// Upadting a existing company in the system  
        /// </summary>
        /// <param name="value">Commpany Data as an Single Commpany Object</param>
        /// <param name="BRNumber">Commpany Data as an Single Commpany Object</param>

        // POST: api/commpany
        public int Post(string BRNumber, [FromBody] Models.Commpany value)
        {
            Models.DataStore.UpdateCompany(BRNumber, value);
            return 200;
        }

        /// <summary>
        /// Check Whether Entered Company is a Company 
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Return the Company is that Exist </returns>

        [Route("api/isCommpany")]
        [HttpGet]
        public int isCommpany(string email)
        {
            return Models.DataStore.IsCommpany(email);
        }


        /// <summary>
        /// Deleting a company from the system
        /// </summary>
        /// <param name="BRNumber">Commpany of the Officer that Need to Deleted</param>
        /// <param name="requestedBY">The Commpany that Deleted the Account</param>
        // DELETE: api/Commpany/5
        public int Delete(string BRNumber, string requestedBY)
        {
            Models.DataStore.DeleteCompany(BRNumber, requestedBY);
            return 200;
        }
    }
}
