using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public interface IProjectMemberRepository
    {
        Task<List<string>> GetProjectName(int IdUser);
    }
}
