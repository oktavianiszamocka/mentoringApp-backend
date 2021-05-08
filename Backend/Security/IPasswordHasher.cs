using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Security
{
    interface IPasswordHasher
    {
        string Hash(string password);

        //tuples feature
        bool Check(string hash, string password);
    }
}
