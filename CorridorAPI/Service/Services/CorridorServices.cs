using Common.Models;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interface;
using Repository.Repositories;

namespace Service.Services
{
    public class CorridorServices: ICorridorServices
    {
        ICorridorRepository _corridorRepository;

        public CorridorServices()
        {
            _corridorRepository = new CorridorRepository();
        }

        /// <summary>
        /// adds a new corridor to database with name = corridorName
        /// </summary>
        /// <param name="corridorName"></param>
        public void Post(string corridorName)
        {
            try
            {
                _corridorRepository.Post(corridorName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Adds user with staffId = username.StaffId to corridor with corridorId = newCorridorId
        /// </summary>
        /// <param name="newCorridorId"></param>
        /// <param name="username"></param>
        public void Post(int newCorridorId, string username)
        {
            try
            {
                _corridorRepository.Post(newCorridorId, username);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Removes user with staffId= newStaffId from corridro with corridorIt username.corridorId
        /// </summary>
        /// <param name="newCorridorId"></param>
        /// <param name="username"></param>
        public void Delete(int newCorridorId, string username)
        {
            try
            {
                _corridorRepository.Delete(newCorridorId, username);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// removes corridor with corridorId = corridorId
        /// </summary>
        /// <param name="corridorId"></param>
        public void Delete(int corridorId)
        {
            try
            {
                _corridorRepository.Delete(corridorId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// updates CorridorModel name with it updatedcorridor.corridorId
        /// </summary>
        /// <param name="updatedCorridor"></param>
        public void Update(CorridorModel updatedCorridor)
        {
            try
            {
                _corridorRepository.Update(CustomMapper.MapTo.corridor(updatedCorridor));
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// returns a list of all CorridorModels
        /// </summary>
        /// <returns>returns a list of all CorridorModels</returns>
        public List<CorridorModel> List()
        {
            try
            {
                return CustomMapper.MapTo.corridorModel(_corridorRepository.List());
            }
            catch (Exception)
            {

                throw;
            }            
        }
    }
}
