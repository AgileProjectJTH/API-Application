using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace CorridorAPI.Models
{
    public class kronox
    {
        public static string getSchedule(string roomNr)
        {
            using (var client = new HttpClient())
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://roomandschedule.hj.se/api/Rooms/E2420");
                httpWebRequest.Method = WebRequestMethods.Http.Get;
                httpWebRequest.Accept = "application/json; charset=utf-8";
                httpWebRequest.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)httpWebRequest.GetResponse();
                string json;
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    json = sr.ReadToEnd();
                }
                
                return json;
            }
        }
    }
}