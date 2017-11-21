
using System.Collections.Generic;

namespace WorkerRole1
{
    public class ServerResponse
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string ResponseType { get; set; }

        public string FeatureDescription { get; set; }

        public List<string> BlobDirectories { get; set; }

        public List<string> Blobs { get; set; }
    }
}