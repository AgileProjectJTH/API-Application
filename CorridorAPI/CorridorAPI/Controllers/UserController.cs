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
        IStaffServices _staffServices;
        public UserController()
        {
            _staffServices = new StaffServices();
        }

        /* GET: Api/User
           Returns: all users */
        [Authorize]
        public IHttpActionResult GET()
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;

            try
            {
                StaffModel user = _staffServices.Get(authenticatedUser);
                if (user.isAdmin)
                {
                    StaffModels staffs = new StaffModels();
                    staffs.staffModels = _staffServices.List();
                    return Json(staffs);
                }
                return BadRequest("Permission denied");

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
                StaffModel user = _staffServices.Get(authenticatedUser);
                if (user.isAdmin)
                {
                    StaffModels staffs = new StaffModels();
                    staffs.staffModels = _staffServices.List(corridorNr);
                    return Json(staffs);
                }
                return BadRequest("Permission denied");

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

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _staffServices.Update(staffModel);
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

            if (username == null)
            {
                return BadRequest("Username may not be null");
            }

            try
            {
                StaffModel user = _staffServices.Get(authenticatedUser);
                if (user.isAdmin)
                {
                    _staffServices.Delete(username);
                    AuthRepository _repo = new AuthRepository();
                    _repo.Delete(username);
                    return Ok("User Deleted");
                }
                return BadRequest("Permission denied");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}