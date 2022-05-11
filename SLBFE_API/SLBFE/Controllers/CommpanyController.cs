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
        /// Gets a list of available Commpany
        /// </summary>
        /// <returns></returns>
        // GET: api/Commpany/5
        public List<Models.Commpany> Get()
        {
            var list = new List<Models.Commpany>();
            var commpanyList = Models.DataStore.GetCommpany();

            foreach (var commpany in commpanyList)
            {
                var comp = commpany;
                comp.Password = "";
                list.Add(comp);
            }

            return list;
        }

        public List<Models.Commpany> Get(string email)
        {
            var list = new List<Models.Commpany>();
            var companylist = Models.DataStore.GetCommpany();

            if (companylist != null)
            {
                foreach (var company in companylist)
                {
                    var comp = company;
                    comp.Password = "";
                    list.Add(comp);
                }

                return list.Where(c => c.Email == email).ToList();
            }
            else
            {
                return new List<Models.Commpany>();
            }
        }


        /// <summary>
        /// POST Request
        /// </summary>
        /// <param name="value">commpany data as an single commpany object</param>
        // POST: api/Commpany
        public int Post([FromBody] Models.Commpany value)
        {
            Models.DataStore.RegisterCommpany(value);
            return 200;
        }


        /// <summary>
        /// POST Request 
        /// </summary>
        /// <param name="value">commpany data as an single commpany object</param>
        /// <param name="BRNumber">commpany data as an single commpany object</param>

        // POST: api/commpany
        public int Post(string BRNumber, [FromBody] Models.Commpany value)
        {
            Models.DataStore.UpdateCompany(BRNumber, value);
            return 200;
        }

        [Route("api/isCommpany")]
        [HttpGet]
        public int isCommpany(string email)
        {
            return Models.DataStore.IsCommpany(email);
        }


        /// <summary>
        /// DELETE Request
        /// </summary>
        /// <param name="BRNumber">Commpany of the officer that need to deleted</param>
        /// <param name="requestedBY">The Commpany that deleted the account</param>
        // DELETE: api/Commpany/5
        public int Delete(string BRNumber, string requestedBY)
        {
            Models.DataStore.DeleteCompany(BRNumber, requestedBY);
            return 200;
        }
    }
}
