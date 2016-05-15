using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Models;
using CorridorAPI.Controllers;
using System.Web.Http;
using System.Web.Http.Results;

namespace Repository.Test
{
    [TestClass]
    public class TestCorridorController
    {
        /* Test the POST(string corridorName) function to add new corridor. */
        [TestMethod]
        public void Test_CorridorController_Create_New_Corridor()
        {

            var controller = new CorridorController();

            string NewCorridorName = "TestCorridor";

            try {

                var ret = controller.POST(NewCorridorName);

                var expected = typeof(OkResult);

                Assert.AreEqual(expected, ret);
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        [TestMethod]
        public void Test_CorridorController_Add_User_To_Corridor()
        {

        }
    }
}
