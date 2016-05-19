using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Interface;
using Common.Models;
using Repository.Interface;
using Repository.Repositories;

namespace Service.Services
{
    public class StaffServices: IStaffServices
    {
        IStaffRepository _staffRepository;

        public StaffServices()
        {
            _staffRepository = new StaffRepository();
        }


        /// <summary>
        /// returns a StaffModel with staffId = staffId
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public StaffModel Get(int staffId)
        {
            return CustomMapper.MapTo.StaffModel(_staffRepository.Get(staffId));
        }

        /// <summary>
        /// Return StaffModel with username = username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public StaffModel Get(string username)
        {
            try
            {
                return CustomMapper.MapTo.StaffModel(_staffRepository.Get(username));
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// returns a list of ALL StaffModels
        /// </summary>
        /// <returns></returns>
        public List<StaffModel> List()
        {
            try
            {
                return CustomMapper.MapTo.StaffModel(_staffRepository.List());
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns StaffModel from corridor with id = corridorId
        /// </summary>
        /// <param name="corridorId"></param>
        /// <returns>Returns StaffModel from corridor with id = corridorId</returns>
        public List<StaffModel> List(int corridorId)
        {
            try
            {
                return CustomMapper.MapTo.StaffModel(_staffRepository.List(corridorId));
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
                _staffRepository.Delete(username);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// updates StaffModel with username = updatedStaff.username
        /// </summary>
        /// <param name="updatedStaff"></param>
        public void Update(StaffModel updatedStaff)
        {
            try
            {
                _staffRepository.Update(CustomMapper.MapTo.Staff(updatedStaff));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
