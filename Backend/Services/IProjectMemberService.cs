using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IProjectMemberService
    {
        Task<List<string>> GetProjectsNameByIdUser(int IdUser);
    }
}
