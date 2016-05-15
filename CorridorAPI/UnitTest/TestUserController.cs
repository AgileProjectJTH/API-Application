using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CorridorAPI.Controllers;
using System.Web.Http;
using Common.Models;

namespace CorridorAPI.Test
{
    [TestClass]
    public class TestUserController
    {

        [TestMethod]
        public void TEST_UserController_Add_User()
        {
            StaffModel staff = new StaffModel();

            UserController controller = new UserController();
            // null:
            // Configuration
            // Request
            // Url

            staff.firstname = "Test";
            staff.lastname = "Testson";
            staff.email = "test@test.test";
            staff.isAdmin = false;
            staff.mobile = "0123456789";
            staff.password = "test123";
            staff.roomNr = "1234";
            staff.signature = "TT";
            staff.username = "TeTe1337";

            IHttpActionResult msg = controller.PUT(staff);
            
        }
    }
}
