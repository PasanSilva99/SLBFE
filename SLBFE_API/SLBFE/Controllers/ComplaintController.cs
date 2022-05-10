using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SLBFE.Controllers
{
    public class ComplaintController : ApiController
    {
        /// <summary>
        /// This function will return all the complaints 
        /// </summary>
        /// <returns>Complaints List</returns>
        // GET: api/Complaint
        public List<Models.Complaint> Get()
        {
            return Models.DataStore.GetComplaints();
        }

        /// <summary>
        /// This will return the complaints as a list
        /// where the email is equal
        /// </summary>
        /// <param name="email">Email of the User/ Company</param>
        /// <returns></returns>
        // GET: api/Complaint/email.sample.com
        public List<Models.Complaint> Get(string email)
        {
            var complaints = Models.DataStore.GetComplaints();
            return complaints.Where(c => c.Email == email).ToList();
        }

        // POST: api/Complaint
        public int Post([FromBody]Models.Complaint value)
        {
            return Models.DataStore.NewComplaint(value);
        }

        /// <summary>
        /// Reply for the complaint
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT: api/Complaint/5
        public int Put(string id, [FromBody]Models.ComplaintReply value)
        {
            return Models.DataStore.ReplyComplaint(id, value);
        }

        // DELETE: api/Complaint/5
        public int Delete(string id)
        {
            return Models.DataStore.DeleteComplaint(id);
        }

        // DELETE: api/Complaint/5
        public int Delete(string id, string replyID)
        {
            return Models.DataStore.DeleteComplaintReply(id, replyID);
        }
    }
}
