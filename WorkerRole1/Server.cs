
using System.Diagnostics;
using System.Linq;
using WebSocketSharp.Server;

namespace WorkerRole1
{
    public static class Server
    {
        public static WebSocketServer WebSocketServer
        {
            get { return m_webSocketServer; }
        }

        /// <summary>
        /// The m_web socket server
        /// </summary>
        private static WebSocketServer m_webSocketServer;

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
        //public static X509Certificate2 GetX509Certificate2()
        //{
        //    const string signingCertificateName = "ARCaptureWSS";
        //    X509Certificate2 myCertificate = new X509Certificate2();
        //    var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
        //    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
        //    var matchedCertificates = store.Certificates.Find(X509FindType.FindBySubjectName, signingCertificateName, true);

        //    if (matchedCertificates.Count > 0)
        //    {
        //        Debug.WriteLine("Certificate Found in StoreLocation");
        //        myCertificate = matchedCertificates[0];
        //    }
        //    else
        //    {
        //        Debug.WriteLine("StoreLocation certificate not found, assigning local certificate");
        //        myCertificate = new X509Certificate2(); // "AzureRivalityWSS.pfx", Settings.SocketCredentialsPassword);
        //    }

        //    return myCertificate;
        //}

        /// <summary>
        /// Adds the services to the websocket server.
        /// Even though the websocket server suppresses adding the same service, checks are added here for functional testing.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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
