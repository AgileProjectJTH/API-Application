using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Repositories;
using Common.Models;
using CorridorAPI.Controllers;
using System.Web.Http.Batch;
using System.Net.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using System.Web.Http.Hosting;
using System.Threading;

namespace CorridorAPI.Test
{
    [TestClass]
    public class TestStaffController
    {

        /* Test the GET function for StaffController.
        Enter a valid input and checks for no exception */
        [TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        public void TEST_StaffController_GET_Valid_Input()
        {

            //TODO:
            //Måste sätta controller.User till någonting. Kan inte vara null när man tillkallar GET().

            StaffController controller = new StaffController();
            
            try {

                //controller.Request = new HttpRequestMessage();
                //controller.Request.SetConfiguration(new HttpConfiguration());

                string testdata1 = "2016-04-26 12:00:00"; // Correct format

                var ret = controller.GET(testdata1);

                var notexpected = typeof(BadRequestErrorMessageResult);

                Assert.AreNotEqual(notexpected, ret);
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }

        }
        
        /*Test the GET function for StaffController.
        Enter a inivalid input and checks for exception */
        [TestMethod]
        //[ExpectedException(typeof(BadRequestErrorMessageResult))]
        public void TEST_StaffController_GET_Invalid_Input()
        {

            var controller = new StaffController();

            string testdata1 = "2016-13-26 12:00:00"; /* Invalid format : fel på månad */
            string testdata2 = "2016-04-33 12:00:00"; /* Invalid format : fel på dag */
            string testdata3 = "2016-04-26 25:00:00"; /* Invalid format : fel på timmar */
            string testdata4 = "2016-04-26 12:61:00"; /* Invalid format : fel på minuter */
            string testdata5 = "2016-04-26 12:00:61"; /* Invalid format : fel på sekunder */

            controller.GET(testdata1);

        
        }
    }
}
