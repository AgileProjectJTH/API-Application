using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserServices
    {
        /// <summary>
        /// Adds a new user
        /// </summary>
        /// <param name="staff"></param>
        void Post(StaffModel staff);
    }
}
