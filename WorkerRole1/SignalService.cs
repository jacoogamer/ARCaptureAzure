﻿// ***********************************************************************
// Assembly         : WorkerRole1
// Author           : Jason Coombes
// Created          : 07-20-2017
//
// Last Modified By : Jason Coombes
// Last Modified On : 07-24-2017
// ***********************************************************************
// <copyright file="SignalService.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WorkerRole1;

/// <summary>
/// The Server namespace.
/// </summary>
namespace WebSocketSharp.Server
{
    /// <summary>
    /// Class SignalService.
    /// </summary>
    public class SignalService : WebSocketBehavior
	{
        /// <summary>
        /// The broadcast count
        /// </summary>
        public int BroadcastCount;

        /// <summary>
        /// The socket current user identifier
        /// </summary>
        public int SocketCurrentUserId;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalService" /> class.
        /// </summary>
        public SignalService()
		{
		}

        /// <summary>
        /// Called when the <see cref="T:WebSocketSharp.WebSocket" /> used in a session receives a message.
        /// </summary>
        /// <param name="e">A <see cref="T:WebSocketSharp.MessageEventArgs" /> that represents the event data passed to
        /// a <see cref="E:WebSocketSharp.WebSocket.OnMessage" /> event.</param>
        protected override void OnMessage(MessageEventArgs e)
        {
            Process(e.Data);
        }

        private void SendImages()
        {
            BlobConnector blobConnector = new BlobConnector();
            List<string> blobDirectories = new List<string>();
            blobDirectories = blobConnector.ListBlobs("arimages");


            foreach (string blob in blobDirectories)
            {
                byte[] buffer = blobConnector.DownloadImage(blob);
                string ret = System.Text.Encoding.Default.GetString(buffer);
                ClientRequest retClientRequest = JsonConvert.DeserializeObject(ret) as ClientRequest;

                ServerResponse serverResponse = new ServerResponse()
                {
                    ResponseType = "DownloadImage",
                    name = blob,
                    featureDescription = retClientRequest.featureDescription,
                    image = retClientRequest.image
                };

                string ret2 = JsonConvert.SerializeObject(serverResponse);
                SendThis(ret2);
            }
        }

        /// <summary>
        /// Processes the specified json.
        /// </summary>
        /// <param name="json">The json.</param>
        private void Process(string json)
        {
            ClientRequest clientRequest = JsonConvert.DeserializeObject<ClientRequest>(json);

            switch (clientRequest.RequestType)
            {
                case RequestType.UploadImage:
                    {
                        break;
                    }
                case RequestType.DownloadAllImages:
                    {
                        break;
                    }
                case RequestType.DownloadImage:
                    {
                        break;
                    }
                case RequestType.DeleteImage:
                    {
                        break;
                    }
                case RequestType.DeleteAllImages:
                    {
                        break;
                    }
                case RequestType.ListBlobDirectories:
                    {
                        break;
                    }
                case RequestType.ListBlobsInDirectory:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// Gets the m_ web socket session manager.
        /// </summary>
        /// <value>The m_ web socket session manager.</value>
        private WebSocketSessionManager m_WebSocketSessionManager
		{
			get { return Sessions; }
		}

        /// <summary>
        /// Gets the web socket session manager.
        /// </summary>
        /// <value>The web socket session manager.</value>
        public WebSocketSessionManager WebSocketSessionManager
		{
			get { return m_WebSocketSessionManager; }
		}

        /// <summary>
        /// Sends the this.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendThis(string message)
		{
			Send(message);
		}

        /// <summary>
        /// Processes the message.
        /// </summary>
        /// <param name="data">The data.</param>
        private void ProcessMessage(string data)
		{
		}

        /// <summary>
        /// Called when the WebSocket connection used in a session has been established.
        /// </summary>
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

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
		}

        /// <summary>
        /// Finalizes an instance of the <see cref="SignalService" /> class.
        /// </summary>
        ~SignalService()
		{
			Dispose(false);
		}

		#endregion Dispose Methods
	}
}