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
        (bool Verified, bool NeedsUpgrade) Check(string hash, string password);
    }
}
