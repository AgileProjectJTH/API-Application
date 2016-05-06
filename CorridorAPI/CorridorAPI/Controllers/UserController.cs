using Common.Models;
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
            StaffModels staffs = new StaffModels();
            staffs.staffModels = CustomMapper.MapTo.StaffModel(Repository.Repositories.StaffRepository.List());
            return Json(staffs);
        }

        /* GET: Api/User
           Returns: all users from a specific corridor */
        public IHttpActionResult GET(int corridorNr)
        {
            StaffModels staffs = new StaffModels();
            staffs.staffModels = CustomMapper.MapTo.StaffModel(Repository.Repositories.StaffRepository.List(corridorNr));
            return Json(staffs);
        }

        /* PUT: Api/User
           Updates a user */
        public IHttpActionResult PUT(StaffModel staffModel)
        {
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
        public IHttpActionResult DELETE(string username)
        {
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