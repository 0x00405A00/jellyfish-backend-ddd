using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Handler.Device.Extension
{
    public static class FileResultExtension
    {
        public static async Task<byte[]> ReadAllBytes(this FileResult fileResult)
        {
            byte[] data = null;
            if (fileResult != null)
            {
                using Stream sourceStream = await fileResult.OpenReadAsync();
                data = await sourceStream.ReadAllStream();
            }
            return data;
        }
    }
}
