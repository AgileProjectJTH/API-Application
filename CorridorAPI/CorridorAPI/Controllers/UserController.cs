using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Service.Interface;
using Service.Services;

namespace CorridorAPI.Controllers
{
    public class UserController : ApiController
    {        
        /* GET: Api/User
           Returns: all users */
        [Authorize]
        public IHttpActionResult GET()
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;

            try
            {
                StaffModels staffs = new StaffModels();
                staffs.staffModels = CustomMapper.MapTo.StaffModel(Repository.Repositories.StaffRepository.List());
                return Json(staffs);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }

        /* GET: Api/User
           Returns: all users from a specific corridor */
        [Authorize]
        public IHttpActionResult GET(int corridorNr)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;

            try
            {
                StaffModels staffs = new StaffModels();
                staffs.staffModels = CustomMapper.MapTo.StaffModel(Repository.Repositories.StaffRepository.List(corridorNr));
                return Json(staffs);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        /* PUT: Api/User
           Updates a user */
        [Authorize]
        public IHttpActionResult PUT(StaffModel staffModel)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;

            try
            {
                Repository.Repositories.StaffRepository.Update(CustomMapper.MapTo.Staff(staffModel));
                
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }

        /* DELETE: Api/User
           Deletes a user */
        [Authorize]
        public IHttpActionResult DELETE(string username)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;

            try
            {
                Repository.Repositories.StaffRepository.Delete(username);
                AuthRepository _repo = new AuthRepository();
                _repo.Delete(username);
                return Ok("deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}