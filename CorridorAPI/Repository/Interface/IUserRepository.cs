using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a new user
        /// </summary>
        /// <param name="user"></param>
        void Post(Staff staff);
    }
}
