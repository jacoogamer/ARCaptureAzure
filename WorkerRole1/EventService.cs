using System;

namespace WebSocketSharp.Server
{
	public class EventService : WebSocketBehavior
	{
		public int BroadcastCount;

		public int SocketCurrentUserId;

		public EventService()
		{
		}

        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data == "BALUS"
                      ? "I've been balused already..."
                      : "I'm not available now.";

            Send(msg);
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

		~EventService()
		{
			Dispose(false);
		}

		#endregion Dispose Methods
	}
}