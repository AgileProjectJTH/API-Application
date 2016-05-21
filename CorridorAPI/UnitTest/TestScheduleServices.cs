using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Service.Services;
using Common.Models;
using System.Collections.Generic;

namespace Service.Test
{
    [TestClass]
    public class TestScheduleServices
    {

        /* Creates a new user and task for the user */
        [TestMethod]
        public void Test_ScheduleServices_Post_A_Task()
        {
            try
            {
                /* Creates new user */
                UserServices us = new UserServices();
                UserModel NewUser = new UserModel()
                {
                    UserName = "Anka1234",
                    firstname = "Kajsa",
                    lastname = "Anka"
                };
                us.Post(NewUser);

                /* Creates new ScheduleModel */
                ScheduleServices ss = new ScheduleServices();
                ScheduleModel Schedule = new ScheduleModel()
                {
                    fromDateAndTime = "2016-05-21 08:00:00",
                    toDateAndTime = "2016-05-21 12:00:00",
                    available = false
                };
                ss.Post(Schedule, NewUser.UserName);


            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        /* Creates a new user and task with invalid format */
        [TestMethod]
        public void Test_ScheduleServices_Post_A_Invalid_Task_With_Wrong_Format()
        {
            try
            {
                /* Creates new user */
                UserServices us = new UserServices();
                UserModel NewUser = new UserModel()
                {
                    UserName = "Anki1234",
                    firstname = "Kicki",
                    lastname = "Anka"
                };
                us.Post(NewUser);

                /* Creates new ScheduleModel */
                ScheduleServices ss = new ScheduleServices();
                ScheduleModel Schedule = new ScheduleModel()
                {
                    fromDateAndTime = "2016-05-21 08:00",
                    toDateAndTime = "2016-05-21 12:00",
                    available = false
                };
                string Return = ss.Post(Schedule, NewUser.UserName);

                Assert.IsNotNull(Return);

            }
            catch(Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        /* Create new user with schedule. Check if Staff is avalible at three different times */
        [TestMethod]
        public void Test_ScheduleServices_Check_If_Available()
        {
            try
            {
                /* Creates new user */
                UserServices us = new UserServices();
                UserModel NewUser = new UserModel()
                {
                    UserName = "Anpi1234",
                    firstname = "Pippi",
                    lastname = "Anka"
                };
                us.Post(NewUser);

                /* Creates new ScheduleModel */
                ScheduleServices ss = new ScheduleServices();
                ScheduleModel Schedule = new ScheduleModel()
                {
                    fromDateAndTime = "2016-05-21 08:00:00",
                    toDateAndTime = "2016-05-21 12:00:00",
                    available = false
                };
                ss.Post(Schedule, NewUser.UserName);

                /* Check if Staff if Avaliable */
                string CheckTime1 = "2016-05-21 10:00:00";
                string CheckTime2 = "2016-05-21 07:59:59";
                string CheckTime3 = "2016-05-21 12:00:01";
                bool ok1 = false;
                bool ok2 = true;
                bool ok3 = true;
                ok1 = ss.Get(CheckTime1, NewUser.UserName);
                ok2 = ss.Get(CheckTime2, NewUser.UserName);
                ok3 = ss.Get(CheckTime3, NewUser.UserName);

                Assert.AreEqual(true, ok1);
                Assert.AreEqual(false, ok2);
                Assert.AreEqual(false, ok3);

            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        [TestMethod]
        public void Test_ScheduleServices_List_Schedule()
        {
            try
            {
                /* Creates new user */
                UserServices us = new UserServices();
                UserModel NewUser = new UserModel()
                {
                    UserName = "Anti1234",
                    firstname = "Titti",
                    lastname = "Anka"
                };
                us.Post(NewUser);

                /* Creates new ScheduleModel */
                ScheduleServices ss = new ScheduleServices();
                ScheduleModel NewSchedule = new ScheduleModel()
                {
                    fromDateAndTime = "2016-05-21 08:00:00",
                    toDateAndTime = "2016-05-21 12:00:00",
                    roomNr = "E1234",
                    available = false
                };
                ss.Post(NewSchedule, NewUser.UserName);

                /* Retreives a List of task for a room */
                List<Schedule> ScheduleList = new List<Common.Models.Schedule>();
                ScheduleList = ss.List(NewSchedule.roomNr);
                Schedule Schedule = ScheduleList.Find(x => x.from == NewSchedule.fromDateAndTime && x.to == NewSchedule.toDateAndTime);

                Assert.AreEqual(NewSchedule.roomNr, Schedule.room);

            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }


        [TestMethod, Ignore]
        public void Test_ScheduleServices_Post()
        {
            try
            {
                /* Creates new user */
                UserServices us = new UserServices();
                UserModel NewUser = new UserModel()
                {
                    UserName = "Opjo1234",
                    firstname = "Oppfinnar-Jocke",
                    lastname = "Johansson"
                };
                us.Post(NewUser);

                /* Creates new ScheduleModel */
                ScheduleServices ss = new ScheduleServices();

                ScheduleModel NewSchedule = new ScheduleModel()
                {
                    fromDateAndTime = "2016-05-21 07:00:00",
                    toDateAndTime = "2016-05-21 19:00:00",
                    roomNr = "A1234",
                    available = false
                };
                //ss.Post(NewSchedule, NewUser.UserName);
            }
            catch (Exception e)
            {
                Assert.Fail("Expected no exception, but got " + e.Message);
            }
        }
    }
}
