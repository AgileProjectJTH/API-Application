using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Service.Services;
using Common.Models;

namespace Service.Test
{
    [TestClass]
    public class TestUserServices
    {
        [TestMethod]
        public void Test_UserServices_Add_New_User()
        {
            try
            {
                /* Creates new User */
                UserServices us = new UserServices();
                UserModel NewUser = new UserModel()
                {
                    UserName = "Jojo1234",
                    firstname = "Kalle",
                    lastname = "Anka"
                };
                us.Post(NewUser);

            }
            catch(Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }
    }
}
