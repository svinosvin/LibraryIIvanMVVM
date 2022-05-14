using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.AunthenticationService
{
    public static class HashHelper
    {
        public static string ConvertPasswordFormat(string passwordHash, byte formatMarker)
        {
            var bytes = Encoding.UTF8.GetBytes(passwordHash);
            var bytesWithMarker = new byte[bytes.Length + 1];
            bytesWithMarker[0] = formatMarker;
            bytes.CopyTo(bytesWithMarker, 1);
            return Convert.ToBase64String(bytesWithMarker);
        }
    }
}
