using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;

namespace SLBFE.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class FilesController : ApiController
    {
        private const string SLBFEPath = "SLBFE_Data";

        /// <summary>
        /// Get files for related to the specific email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <returns>Returns file path, filename and its data if exist</returns>
        // GET: api/Files/5
        [Route("api/ReadFile")]
        [HttpGet]
        public Models.FileHandle GetFileFor(string email, string path, string filename)
        {
            try
            {
                var filePath = Path.Combine(SLBFEPath, path, email, filename);
                var isPathExists = File.Exists(filePath);
                if (isPathExists)
                {
                    return new Models.FileHandle() { Path = path, Name = filename, Data = File.ReadAllBytes(filePath) };
                }
            }
            catch (Exception ex)
            {
                Models.DataStore.Log("Error Saving File!");
                Models.DataStore.Log(ex.ToString());
            }
            return null;
        }

        /// <summary>
        /// Saving the file with users email as the filename
        /// </summary>
        /// <param name="email"></param>
        /// <param name="foldername"></param>
        /// <param name="filename"></param>
        /// <param name="file"></param>
        // POST: api/Files
        [Route("api/SaveFile")]
        [HttpPost]
        public void SaveFile(string email, string foldername, string filename, [FromBody]byte[] file)
        {
            try
            {
                // this will create the directory if not exists
                var dir = Path.Combine(SLBFEPath, foldername, email);
                Directory.CreateDirectory(Path.Combine(SLBFEPath, foldername));

                var filePath = Path.Combine(SLBFEPath, foldername, email, filename);

                File.WriteAllBytes(filePath, file);
            }
            catch (Exception ex)
            {
                Models.DataStore.Log("Error Saving File!");
                Models.DataStore.Log(ex.ToString());
            }
        }

        /// <summary>
        /// Delete the related file that mentioned
        /// </summary>
        /// <param name="email"></param>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        // DELETE: api/Files/5
        [Route("api/DeleteFile/{email}/{foldername}/{filename}")]
        [HttpDelete]
        public void DeleteFile(string email, string path, string filename)
        {
            try
            {

                var filePath = Path.Combine(SLBFEPath, path, email, filename);
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Models.DataStore.Log("Error Deleting File!");
                Models.DataStore.Log(ex.ToString());
            }
        }

        // Citizen
        // ProfilePic
        // Birthcertificate
        // CV
        // Passport
        // Qualification

        // Bureau
        // prfodilePic
        // EmployeeID

        // Company
        // Profile pic
        // BR

        // > File Paths
        // ProfilePic/email - Common
        // Birthcertificate/email
        // CV/email
        // Passport/email
        // Qualification/email
        // EmployeeID/email
        // BR/email

        string[] KnownFilePaths = new string[] { "ProfilePic", "Birthcertificate", "CV", "Passport", "Qualification", "EmployeeID", "BR" };

        /// <summary>
        /// Here delete the user directory
        /// </summary>
        /// <param name="email"></param>
        [Route("api/DeleteUserFiles/{email}")]
        [HttpDelete]
        public void DeleteUser(string email)
        {
            try
            {
                foreach (var filepath in KnownFilePaths)
                {
                    var path = Path.Combine(SLBFEPath, filepath, email);
                    Directory.Delete(path, true);
                }
            }
            catch (Exception ex)
            {
                Models.DataStore.Log("Error Deleting User Directory!");
                Models.DataStore.Log(ex.ToString());
            }
        }
    }
}
