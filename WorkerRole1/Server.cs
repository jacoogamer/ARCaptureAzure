// ***********************************************************************
// Assembly         : WorkerRole1
// Author           : Jason Coombes
// Created          : 07-20-2017
//
// Last Modified By : Jason Coombes
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="Server.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics;
using System.Linq;
using WebSocketSharp.Server;

/// <summary>
/// The WorkerRole1 namespace.
/// </summary>
namespace WorkerRole1
{
    /// <summary>
    /// Class Server.
    /// </summary>
    public static class Server
    {
        /// <summary>
        /// Gets the web socket server.
        /// </summary>
        /// <value>The web socket server.</value>
        public static WebSocketServer WebSocketServer
        {
            get { return m_webSocketServer; }
        }

        /// <summary>
        /// The m_web socket server
        /// </summary>
        private static WebSocketServer m_webSocketServer;

        /// <summary>
        /// Initialises this instance.
        /// </summary>
        public static void Initialise()
        {
            //var myCertificate = GetX509Certificate2();
            CreateSecureWebSocketServer();
            AddServicesToWebSocketServer();
            WebSocketServer.Start();
        }

        /// <summary>
        /// Returns the X509 certificate used for the wss connection.
        /// </summary>
        /// <returns>X509Certificate2.</returns>
        public static bool AddServicesToWebSocketServer()
        {
            var ret = false;


            if (m_webSocketServer.WebSocketServices.Hosts.FirstOrDefault(x => x.BehaviorType == typeof(EventService)) == null)
            {
                Debug.WriteLine("Adding service");
                m_webSocketServer.AddWebSocketService<EventService>("/EventService");
            }
            else
            {
                ret = true;
            }

            if (m_webSocketServer.WebSocketServices.Hosts.FirstOrDefault(x => x.BehaviorType == typeof(ImageService)) == null)
            {
                Debug.WriteLine("Adding service");
                m_webSocketServer.AddWebSocketService<ImageService>("/ImageService");
            }
            else
            {
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// Creates the secure web socket server.
        /// </summary>
        public static void CreateSecureWebSocketServer()
        //public static void CreateSecureWebSocketServer(X509Certificate2 myCertificate)
        {
            m_webSocketServer = new WebSocketServer(27000);

            //{
            //    //SslConfiguration =
            //    //{
            //    //    ServerCertificate = myCertificate,
            //    //    ClientCertificateRequired = false
            //    //},

            //    //AuthenticationSchemes = AuthenticationSchemes.Basic,
            //    //Realm = "ARCaptureWSS",

            //    //UserCredentialsFinder = id =>
            //    //{
            //    //    var name = id.Name;
            //    //    return name == Settings.SocketCredentialsUsername ? new NetworkCredential(name, Settings.SocketCredentialsPassword) : null; // If the user credentials aren't found.
            //    //},

            //};
        }
    }
}
