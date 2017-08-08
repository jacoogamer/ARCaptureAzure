using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
    public class ServerResponse
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public int id;

        /// <summary>
        /// The name
        /// </summary>
        public string name;

        /// <summary>
        /// The image
        /// </summary>
        public byte[] image;

        /// <summary>
        /// The response type
        /// </summary>
        public string ResponseType;

        /// <summary>
        /// JSON Feature Description
        /// </summary>
        public string featureDescription;

        /// <summary>
        /// The BLOB directories
        /// </summary>
        public List<string> BlobDirectories;
    }
}
