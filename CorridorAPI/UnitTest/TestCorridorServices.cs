using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Service.Services;
using Common.Models;
using Repository;

namespace Service.Test
{
    [TestClass]
    public class TestCorridorSercives
    {

        [TestInitialize]
        public void TestSetup()
        {
        }


        /* Checks if able to add new Corridor. */
        [TestMethod]
        public void Test_CorridorServices_Add_New_Corridor()
        {
            try
            {
                /* Creating new Corridor */
                CorridorServices cs = new CorridorServices();
                string NewCorridorName = "T1234";
                cs.Post(NewCorridorName);
            }
            catch(Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        /* Checks if able to have multiple Corridors with the same name */
        /*!! Problems with local database under test !!*/
        [TestMethod, Ignore]
        public void Test_CorridorServices_Add_Two_Corridors_With_Same_Name()
        {
            try
            {
                /* Creating new Corridor */
                CorridorServices cs = new CorridorServices();
                string NewCorridorName = "T1234";
                cs.Post(NewCorridorName);

                /* Add second Corridor with same name */
                cs.Post(NewCorridorName);

                /* Check if there is 2 Corridors. If there is, test failed */
                List<CorridorModel> CorridorList = new List<CorridorModel>();
                CorridorList = cs.List();
                Assert.AreEqual(2, CorridorList.Count);
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
        }


        /* Creates new Corridor and then gets the Corridor's ID from the database */
        [TestMethod]
        public void Test_CorridorServices_Get_CorridorID_From_New_Corridor()
        {
            try
            {
                /* Creating new Corridor */
                CorridorServices cs = new CorridorServices();
                string NewCorridorName = "T1234";
                cs.Post(NewCorridorName);

                /* Checks if the Get() function returns the right corridor */
                List<CorridorModel> CorridorList = new List<CorridorModel>();
                CorridorModel cm = new CorridorModel();
                CorridorList = cs.List();
                cm = CorridorList.Find(x => x.corridorName == NewCorridorName);
                
                Assert.AreEqual(NewCorridorName, cm.corridorName);
            }
            catch(Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        /* Creates new Corridor, gets the Corridor's ID from the database and then delete the new Corridor */
        [TestMethod]
        public void Test_CorridorServices_Delete_Corridor()
        {
            try
            {
                /* Creating new Corridor */
                CorridorServices cs = new CorridorServices();
                string NewCorridorName = "T1234";
                cs.Post(NewCorridorName);

                /* Get the new Corridor's ID */
                List<CorridorModel> CorridorList = new List<CorridorModel>();
                CorridorModel NewCorridor = new CorridorModel();
                CorridorList = cs.List();
                NewCorridor = CorridorList.Find(x => x.corridorName == NewCorridorName);

                /* Try to delete the new Corridor */
                cs.Delete(NewCorridor.corridorId);
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }

        /* Adds a new Corridor and Updates the corridorName */
        [TestMethod]
        public void Test_CorridorServices_Update_Corridor()
        {
            try {
                /* Creating new Corridor */
                CorridorServices cs = new CorridorServices();
                string CorridorName = "E1234";
                cs.Post(CorridorName);

                /* Get the Corridor's ID */
                List<CorridorModel> CorridorList = new List<CorridorModel>();
                CorridorModel Corridor = new CorridorModel();
                CorridorList = cs.List();
                Corridor = CorridorList.Find(x => x.corridorName == CorridorName);

                /* Update the name of the Corridor from 'E1234' to 'J2134' */
                CorridorModel NewCorridor = new CorridorModel()
                {
                    corridorName = "J2134",
                    corridorId =  Corridor.corridorId };
                cs.Update(NewCorridor);

                /* Check that the Corridor has updated */
                CorridorList = cs.List();
                Corridor = CorridorList.Find(x => x.corridorId == NewCorridor.corridorId);
                Assert.AreEqual(NewCorridor.corridorId, Corridor.corridorId);

            }
            catch(Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        /* Creates a new Corridor and User. Adds the User to the Corridor */
        [TestMethod]
        public void Test_CorridorServices_Add_Staff_To_New_Corridor()
        {
            try
            {
                /* Creating new Corridor */
                CorridorServices cs = new CorridorServices();
                string NewCorridorName = "T1234";
                cs.Post(NewCorridorName);

                /* Get the created CorridorModel from the database */
                List<CorridorModel> CorridorList = new List<CorridorModel>();
                CorridorModel Corridor = new CorridorModel();
                CorridorList = cs.List();
                Corridor = CorridorList.Find(x => x.corridorName == NewCorridorName);

                /* Creates the new User */
                UserServices us = new UserServices();
                UserModel NewUser = new UserModel()
                {
                    UserName = "Jojo1122",
                    firstname = "Johan",
                    lastname = "Johansson"
                };
                us.Post(NewUser);

                /* Adds a User to the Corridor */
                cs.Post(Corridor.corridorId, NewUser.UserName);

            }
            catch(Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        [TestMethod, Ignore]
        public void Test_CorridorServices_Delete_User_From_Corridor()
        {
            try
            {
                /* Creating new Corridor */
                CorridorServices cs = new CorridorServices();
                string NewCorridorName = "T1234";
                cs.Post(NewCorridorName);

                /* Get the created CorridorModel from the database */
                List<CorridorModel> CorridorList = new List<CorridorModel>();
                CorridorModel Corridor = new CorridorModel();
                CorridorList = cs.List();
                Corridor = CorridorList.Find(x => x.corridorName == NewCorridorName);

                /* Creates the new User */
                UserServices us = new UserServices();
                UserModel NewUser = new UserModel()
                {
                    UserName = "Jojo1122",
                    firstname = "Johan",
                    lastname = "Johansson"
                };
                us.Post(NewUser);

                /* Adds a User to the Corridor */
                cs.Post(Corridor.corridorId, NewUser.UserName);

                /* Delete User from Corridor */
                cs.Delete(Corridor.corridorId, NewUser.UserName);

            }
            catch(Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        [TestCleanup]
        public void TestCleanup()
        {
        }
    }
}
