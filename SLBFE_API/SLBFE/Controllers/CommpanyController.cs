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

        /// <summary>
        /// POST Request
        /// </summary>
        /// <param name="value">Officer data as an single Officer object</param>
        // POST: api/Commpany
        public void Post([FromBody] Models.Commpany value)
        {
            Models.DataStore.RegisterCommpany(value);
        }


        // PUT: api/Commpany/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Commpany/5
        public void Delete(int id)
        {
        }
    }
}
