using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Service.Services;
using Common.Models;
using System.Collections.Generic;

namespace Service.Test
{
    [TestClass]
    public class TestStaffServices
    {

        /* Adds a new User to the database. Then receives the User through its username */
        [TestMethod]
        public void Test_StaffServices_Get_Staff_Through_Username()
        {
            try
            {
                /* Creates new User */
                UserServices us = new UserServices();
                UserModel NewUser = new UserModel()
                {
                    UserName = "Anka1337",
                    firstname = "Kalle",
                    lastname = "Anka"
                };
                us.Post(NewUser);

                /* Receives the User' ID from the database */
                StaffServices ss = new StaffServices();
                StaffModel User = ss.Get(NewUser.UserName);
                Assert.AreEqual(NewUser.UserName, User.username);

            }
            catch(Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        /* Adds a new User to the databse. Then receives the User throught its ID */
        [TestMethod]
        public void Test_StaffServices_Get_Staff_Through_ID()
        {
            try
            {
                /* Creates new User */
                UserServices us = new UserServices();
                UserModel NewUser = new UserModel()
                {
                    UserName = "Anka1338",
                    firstname = "Kalle",
                    lastname = "Anka"
                };
                us.Post(NewUser);

                /* Receives the User from the database through username */
                StaffServices ss = new StaffServices();
                StaffModel UserUsername = ss.Get(NewUser.UserName);

                /* Receives the User from the database through ID */
                StaffModel UserID = ss.Get(UserUsername.staffId);

                /* Checks if it is the same user */
                Assert.AreEqual(NewUser.UserName, UserID.username);

            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        /* Creates two new users and checks through the List-function that they are created */
        [TestMethod]
        public void Test_StaffServices_List_All_Users()
        {
            try
            {
                /* Create two new User */
                UserServices us = new UserServices();
                UserModel NewUser1 = new UserModel()
                {
                    UserName = "Anka1338",
                    firstname = "Kalle",
                    lastname = "Anka"
                };
                UserModel NewUser2 = new UserModel()
                {
                    UserName = "Anjo6666",
                    firstname = "Joakim",
                    lastname = "von Anka"
                };
                us.Post(NewUser1);
                us.Post(NewUser2);

                /* Get a list of all users and check if the two new users are included */
                StaffServices ss = new StaffServices();
                List<StaffModel> StaffList = new List<StaffModel>();
                StaffList = ss.List();
                StaffModel Staff1 = StaffList.Find(x => x.username == NewUser1.UserName);
                StaffModel Staff2 = StaffList.Find(x => x.username == NewUser2.UserName);

                Assert.AreEqual(NewUser1.UserName, Staff1.username);
                Assert.AreEqual(NewUser2.UserName, Staff2.username);

            }
            catch(Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        /* Creates two new users and puts them in a new corridor. Retreives a list of alla users
           in the corridor and makes sure that the two new users are included */
        [TestMethod]
        public void Test_StaffService_List_Users_From_Same_Corridor()
        {
            try
            {
                /* Create two new User */
                UserServices us = new UserServices();
                UserModel NewUser1 = new UserModel()
                {
                    UserName = "Anka1338",
                    firstname = "Kalle",
                    lastname = "Anka"
                };
                UserModel NewUser2 = new UserModel()
                {
                    UserName = "Anjo6666",
                    firstname = "Joakim",
                    lastname = "von Anka"
                };
                us.Post(NewUser1);
                us.Post(NewUser2);

                /* Creates a new Corridor and retrieves its ID*/
                CorridorServices sc = new CorridorServices();
                string CorridorName = "Pengabinge3303";
                sc.Post(CorridorName);
                List<CorridorModel> CorridorList = new List<CorridorModel>();
                CorridorList = sc.List();
                CorridorModel Corridor = CorridorList.Find(x => x.corridorName == CorridorName);

                /* Puts the two new users in the new corridor */
                sc.Post(Corridor.corridorId, NewUser1.UserName);
                sc.Post(Corridor.corridorId, NewUser2.UserName);

                /* Get a list of all users in the new corridor and check if the two new users are included */
                StaffServices ss = new StaffServices();
                List<StaffModel> StaffList = new List<StaffModel>();
                StaffList = ss.List(Corridor.corridorId);
                StaffModel Staff1 = StaffList.Find(x => x.username == NewUser1.UserName);
                StaffModel Staff2 = StaffList.Find(x => x.username == NewUser2.UserName);

                Assert.AreEqual(NewUser1.UserName, Staff1.username);
                Assert.AreEqual(NewUser2.UserName, Staff2.username);

            }
            catch(Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        /* Creates a new user and the delete the user through the username */
        [TestMethod]
        public void Test_StaffServices_Delete_Staff()
        {
            try
            {
                /* Create new User */
                UserServices us = new UserServices();
                UserModel NewUser = new UserModel()
                {
                    UserName = "Ankn1234",
                    firstname = "Knatte",
                    lastname = "Anka"
                };
                us.Post(NewUser);

                /* Delete user through username */
                StaffServices ss = new StaffServices();
                ss.Delete(NewUser.UserName);

                /* Check if user is deleted */
                List<StaffModel> StaffList = new List<StaffModel>();
                StaffList = ss.List();
                StaffModel Staff = StaffList.Find(x => x.username == NewUser.UserName);

                Assert.IsNull(Staff);
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        /* Createss new user and then updates the firstname for the user. */
        [TestMethod]
        public void Test_StaffServices_Update_Staff()
        {
            try
            {
                /* Create new User */
                UserServices us = new UserServices();
                UserModel NewUser = new UserModel()
                {
                    UserName = "Anfn1234",
                    firstname = "Fatte",
                    lastname = "Anka"
                };
                us.Post(NewUser);

                /* Retreive users StaffModel */
                StaffServices ss = new StaffServices();
                StaffModel Staff = new StaffModel();
                Staff = ss.Get(NewUser.UserName);

                /* Update users username */
                StaffModel UpdatedUser = Staff;
                string UpdatedName = "Fnatte";
                UpdatedUser.firstname = UpdatedName;
                ss.Update(UpdatedUser);

                /* Check if user really updated */
                StaffModel Check = ss.Get(UpdatedUser.username);

                Assert.AreEqual(UpdatedName, Check.firstname);

            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }
    }
}
