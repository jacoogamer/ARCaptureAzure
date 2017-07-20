// ***********************************************************************
// Assembly         : WorkerRole1
// Author           : Jason Coombes
// Created          : 07-20-2017
//
// Last Modified By : Jason Coombes
// Last Modified On : 07-20-2017
// ***********************************************************************
// <copyright file="Settings.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <summary>
/// The WorkerRole1 namespace.
/// </summary>
namespace WorkerRole1
{
    /// <summary>
    /// Class Settings. This class cannot be inherited.
    /// </summary>
    public sealed class Settings
	{
        /// <summary>
        /// The socket credentials username
        /// </summary>
        public static string SocketCredentialsUsername = "jason";
        /// <summary>
        /// The socket credentials password
        /// </summary>
        public static string SocketCredentialsPassword = "jasonPW";
	}
}