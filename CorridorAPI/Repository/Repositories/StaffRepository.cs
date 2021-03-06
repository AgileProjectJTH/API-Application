﻿using Common.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StaffRepository : IStaffRepository
    {   
        /// <summary>
        /// returns a staff with staffId = staffId
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public Staff Get(int staffId)
        {
            try
            {
                Staff staff = new Staff();
                using (var db = new CorridorDBEntities())
                {
                    staff = db.Staffs.Find(staffId);
                }
                    return staff;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        /// <summary>
        /// Return user with username = username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Repository.Staff Get(string username)
        {
            try
            {
                Repository.Staff staff = new Staff();
                using (var db = new CorridorDBEntities())
                {
                    staff = db.Staffs.Where(x => x.username == username).First();
                }
                return staff;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// returns a list of ALL users
        /// </summary>
        /// <returns>returns a list of ALL users</returns>
        public List<Repository.Staff> List()
        {
            try
            {
                List<Repository.Staff> LStaff = new List<Staff>();
                using (var db = new CorridorDBEntities())
                {
                    LStaff.AddRange(db.Staffs.ToList());
                }
                return LStaff;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns user from corridor with id = corridorId
        /// </summary>
        /// <param name="corridorId"></param>
        /// <returns>Returns user from corridor with id = corridorId</returns>
        public List<Repository.Staff> List(int corridorId)
        {
            try
            {
                List<Repository.Staff> LStaff = new List<Staff>();
                using (var db = new CorridorDBEntities())
                {
                    List<Staff_Corridor> LStaffCorridor = db.Staff_Corridor.Where(x => x.corridorId == corridorId).ToList();
                    foreach (Staff_Corridor sc in LStaffCorridor)
                    {
                        LStaff.Add(db.Staffs.Where(x => x.staffId == sc.staffId).First());
                    }
                }
                return LStaff;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Deletes staff with username = username
        /// </summary>
        /// <param name="username"></param>
        public void Delete(string username)
        {
            try
            {
                using (var db = new CorridorDBEntities())
                {
                    Staff staff = db.Staffs.Where(x => x.username == username).First();
                    List<int> taskIdList = new List<int>();
                    foreach (Staff_Task sT in db.Staff_Task.Where(x => x.staffId == staff.staffId))
                    {
                        taskIdList.Add(Convert.ToInt32(sT.taskId));
                        db.Staff_Task.Attach(sT);
                        db.Staff_Task.Remove(sT);                        
                    }

                    db.SaveChanges();

                    foreach (int id in taskIdList)
                    {
                        if (db.Staff_Task.Where(x => x.staffId == id).Count() < 1)
                        {
                            Task task = db.Tasks.Where(x => x.taskId == id).First();
                            db.Tasks.Attach(task);
                            db.Tasks.Remove(task);
                        }
                    }

                    foreach (Staff_Corridor sC in db.Staff_Corridor.Where(x => x.staffId == staff.staffId))
                    {
                        db.Staff_Corridor.Attach(sC);
                        db.Staff_Corridor.Remove(sC);
                    }

                    db.Staffs.Attach(staff);
                    db.Staffs.Remove(staff);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// updates staff with username = updatedStaff.username
        /// </summary>
        /// <param name="updatedStaff"></param>
        public void Update(Staff updatedStaff)
        {
            try
            {
                using (var db = new CorridorDBEntities())
                {
                    Staff staff = db.Staffs.Where(x => x.username == updatedStaff.username).First();
                    staff.firstname = updatedStaff.firstname;
                    staff.lastname = updatedStaff.lastname;
                    staff.isAdmin = Convert.ToBoolean(updatedStaff.isAdmin);
                    staff.mobile = updatedStaff.mobile;
                    staff.roomNr = updatedStaff.roomNr;
                    staff.email = updatedStaff.email;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
