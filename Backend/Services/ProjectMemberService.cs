﻿using AutoMapper;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
using MentorApp.Repository;
using MentorApp.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class ProjectMemberService : IProjectMemberService
    {
        private readonly IProjectMemberRepository _projectMemberRepository;
        private readonly IMapper _mapper;
        public ProjectMemberService(IProjectMemberRepository projectMemberRepository, IMapper mapper)
        {
            _projectMemberRepository = projectMemberRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectDTO>> GetProjectsNameByIdUser(int IdUser)
        {
            var projectList =  await _projectMemberRepository.GetProjectName(IdUser);
            var projectDTOList = _mapper.Map<List<ProjectDTO>>(projectList);
            return projectDTOList;
        }

        public async Task<List<ProjectWrapper>> GetProjectsByIdUser(int IdUser)
        {
            var projectList = await _projectMemberRepository.GetProjectName(IdUser);
            var projectDTOList = projectList
                                 .Select(project => new ProjectWrapper
                                 {
                                     IdProject = project.IdProject,
                                     Name = project.Name,
                                     Description = project.Description,
                                     StartDate = project.StartDate,
                                     EndDate = project.EndDate,
                                     SuperviserFullName = project.SuperviserNavigation.FirstName + " " + project.SuperviserNavigation.LastName
                                 }).ToList();
            //var projectDTOList = _mapper.Map<List<ProjectWrapper>>(projectList);
            return projectDTOList;

        }

    }
}
