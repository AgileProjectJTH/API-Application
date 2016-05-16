using Common.Models;
using Repository.Interface;
using Repository.Repositories;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserServices: IUserServices
    {
        IUserRepository _userRepository;

        public UserServices()
        {
            _userRepository = new UserRepository();
        }

        /// <summary>
        /// Adds a new user
        /// </summary>
        /// <param name="staff"></param>
        public void Post(UserModel userModel)
        {
            try
            {
                _userRepository.Post(CustomMapper.MapTo.Staff(userModel));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
