using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
    public enum RequestType
    {
        DownloadImage = 1,
        UploadImage = 2,
        DeleteImage = 3,
        ListBlobDirectories = 4,
        ListBlobsInDirectory = 5
    }
}
