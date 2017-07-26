using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
    public class ClientRequest
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
        /// The request type
        /// </summary>
        public RequestType RequestType;
    }
}
