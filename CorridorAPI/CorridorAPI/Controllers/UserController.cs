using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CorridorAPI.Controllers
{
    public class UserController : ApiController
    {        
        /* GET: Api/User
           Returns: all users */
        public IHttpActionResult GET()
        {
            return null;
        }
        /* GET: Api/User
           Returns: all users from a specific corridor */
        public IHttpActionResult GET(string corridorNr)
        {
            return null;
        }
        /* PUT: Api/User
           Updates a user */
        public IHttpActionResult PUT()
        {
            return null;
        }
        /* DELETE: Api/User
           Deletes a user */
        public IHttpActionResult DELETE()
        {
            return null;
        }
    }
}