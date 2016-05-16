using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Interface;
using Service.Services;
using System.Security.Claims;

namespace CorridorAPI.Controllers
{
    public class CorridorController : ApiController
    {
        ICorridorServices _corridorServices;
        public CorridorController()
        {
            _corridorServices = new CorridorServices();
        }
        /* POST: Api/Corridor            
         * Param: corridorName (string)
         * Create a new corridor */
        [Authorize]
        public IHttpActionResult POST(string corridorName)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;

            try
            {
                _corridorServices.Post(corridorName);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /* POST: Api/Corridor            
         * Param: username (string) , corridorId (Int) 
         * Adds user with username = username to corridor with id corridorId */
        [Authorize]
        public IHttpActionResult POST(string username, int corridorId)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;

            try
            {
                _corridorServices.Post(corridorId, username);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /* DELETE: Api/Corridor            
         * Param: corridorId (Int)
         * Removes corridor with Id = corridorId */
        [Authorize]
        public IHttpActionResult DELETE(int corridorId)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;
            try
            {
                _corridorServices.Delete(corridorId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /* DELETE: Api/Corridor            
         * Param: username (string), corridorId (Int)
         * Removes user with username = username from corridor with Id = corridorId */
        [Authorize]
        public IHttpActionResult DELETE(string username, int corridorId)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;
            try
            {
                _corridorServices.Delete(corridorId, username);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /* PUT: Api/Corridor            
         * Param: corridorModel (CorridorModel) 
         * Updates updates corridor with id = corridorModel.id with corridorname = corridorModel.name  */
        [Authorize]
        public IHttpActionResult PUT(CorridorModel corridorModel)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;
            try
            {
                _corridorServices.Update(corridorModel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /* GET: Api/Corridor            
         * Param:
         * Returns a list of all corridors  */
        [Authorize]
        public IHttpActionResult GET()
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;
            try
            {
                List<CorridorModel> corridorModel = _corridorServices.List();
                return Json(corridorModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
