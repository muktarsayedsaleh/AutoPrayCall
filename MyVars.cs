using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AthanManager
{
    public static class myVars
    {
        public static AthanManager.Form1 myMainForm = null;
        // Returns JSON string
        public static string update_pray_times(string city)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://muslimsalat.com/" + city + "/daily.json?key=12ff669e618426b179ef1cd9f4ae5a99");
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
    }
}
