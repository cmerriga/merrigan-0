using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Merrigan0 {
    [Untested]
    public static class Http {
        public static string Get(string url) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream)) {
                string contents = reader.ReadToEnd();
                return contents;
            }
        }
    }
}
