using Newtonsoft.Json;
using System;
using System.IO;
using WorkerRole1;

namespace WebSocketSharp.Server
{

    public class ClientRequest
    {
        public int id;
        public string name;
        public byte[] image;
    }

    public class ImageService : WebSocketBehavior
	{
		public int BroadcastCount;

		public int SocketCurrentUserId;

		public ImageService()
		{
		}

        protected override void OnMessage(MessageEventArgs e)
        {
            Process(e.Data);

            //Send(msg);
        }

        private void Process(string json)
        {
            ClientRequest clientRequest = JsonConvert.DeserializeObject<ClientRequest>(json);

            if(clientRequest.name == "vr_orig.png")
            {
                BlobConnector blob = new BlobConnector();
                blob.UploadImage(clientRequest.name, new MemoryStream(clientRequest.image));

                SendThis("Found name");
            }
        }

        private WebSocketSessionManager m_WebSocketSessionManager
		{
			get { return Sessions; }
		}

		public WebSocketSessionManager WebSocketSessionManager
		{
			get { return m_WebSocketSessionManager; }
		}

		public void SendThis(string message)
		{
			Send(message);
		}

		private void ProcessMessage(string data)
		{
		}

		protected override void OnOpen()
		{
			try
			{
			}
			catch (Exception exception)
			{
				var msg = exception.Message;
				throw;
			}
		}

		#region Dispose Methods

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
		}

		~ImageService()
		{
			Dispose(false);
		}

		#endregion Dispose Methods
	}
}