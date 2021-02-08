using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;


namespace MentorApp.Services
{
    public interface IProjectPromoterService
    {
        
        Task<ProjectPromotersDTO> GetProjectPromoters(int IdProject);
    }
}
