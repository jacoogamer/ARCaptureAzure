
namespace WorkerRole1
{
    public class ClientRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string FeatureDescription { get; set; }

        public RequestType RequestType { get; set; }
    }
}
