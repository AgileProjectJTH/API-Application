using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CorridorAPI.Controllers
{
    public class CorridorController : ApiController
    {
        /* POST: Api/Corridor            
         * Param: corridorName (string)
         * Create a new corridor */
        [Authorize]
        public IHttpActionResult POST(string corridorName)
        {
            try
            {
                Repository.Repositories.CorridorRepository.Post(corridorName);
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
            try
            {
                Repository.Repositories.CorridorRepository.Post(corridorId, username);
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
            try
            {
                //--------------------------------
                //DELETE ON CASCADE BEHÖVER IMPLEMENTERAS
                //--------------------------------
                Repository.Repositories.CorridorRepository.Delete(corridorId);
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
            try
            {
                Repository.Repositories.CorridorRepository.Delete(corridorId, username);
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
            try
            {
                Repository.Repositories.CorridorRepository.Update(CustomMapper.MapTo.corridor(corridorModel));
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
            try
            {
                List<CorridorModel> corridorModel = CustomMapper.MapTo.corridorModel(Repository.Repositories.CorridorRepository.List());
                return Json(corridorModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
