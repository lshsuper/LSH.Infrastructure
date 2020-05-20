using LSH.Infrastructure.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Demo
{

    [Register(typeof(UserService))]
    public class UserServiceImpl : UserService
    {
        public string Get(int id)
        {
            return "lsh";
        }
    }
}
