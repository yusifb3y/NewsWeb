using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Repository
{
    public interface IUserRepository
    {
        public User Login(UserLoginModel user);
    }
}
