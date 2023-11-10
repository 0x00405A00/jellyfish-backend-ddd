using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Handler.Device.Extension
{
    public static class StreamExtension
    {
        public static async Task<byte[]> ReadAllStream(this Stream stream)
        {
            byte[] bytes = null;
            int pos = 0;
            int i = 0;
            int c = (int)stream.Length;
            while (pos != -1)
            {
                pos = await stream.ReadAsync(bytes, i, c);
                i++;
            }
            return bytes;
        }
    }
}
