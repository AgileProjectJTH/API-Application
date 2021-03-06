﻿using Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Repository.Repositories
{
    public class kronox: IKronox
    {
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="roomNr">number of staffs room ex E2420</param>
        /// <param name="date">Date of witch day to get schedule, yyyy-mm-dd ex 2016-04-25</param>
        /// <returns>Returns a json object with the schedule for the staff with the roomNr and Date (date may be null)</returns>
        public string getSchedule(string roomNr, string date)
        {
            using (var client = new HttpClient())
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://roomandschedule.hj.se/api/Rooms/" + roomNr + "?date=" + date);
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