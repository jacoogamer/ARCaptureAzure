// ***********************************************************************
// Assembly         : WorkerRole1
// Author           : Jason Coombes
// Created          : 07-26-2017
//
// Last Modified By : Jason Coombes
// Last Modified On : 07-26-2017
// ***********************************************************************
// <copyright file="RequestType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The WorkerRole1 namespace.
/// </summary>
namespace WorkerRole1
{
    /// <summary>
    /// Enum RequestType
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// The download image
        /// </summary>
        DownloadImage = 1,

        /// <summary>
        /// The upload image
        /// </summary>
        UploadImage = 2,

        /// <summary>
        /// The delete image
        /// </summary>
        DeleteImage = 3,

        /// <summary>
        /// The list BLOB directories
        /// </summary>
        ListBlobDirectories = 4,

        /// <summary>
        /// The list blobs in directory
        /// </summary>
        ListBlobsInDirectory = 5,

        DownloadAllImages
    }
}
