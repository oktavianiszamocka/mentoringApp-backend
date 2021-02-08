using MentorApp.DTOs.Requests;
using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IProjectService
    {
        Task<ProjectInfoDTO> GetProjectInfoById(int idProject);
    }
}
