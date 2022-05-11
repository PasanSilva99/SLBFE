using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLBFE.Models
{
    /// <summary>
    /// File Handling Class
    /// </summary>
    public class FileHandle
    {
        /// <summary>
        /// Name of the File
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Path of the File
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Data as a byte of array
        /// </summary>
        public byte[] Data { get; set; }
    }
}