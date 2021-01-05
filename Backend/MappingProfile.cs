﻿using AutoMapper;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDTO>();
        }
    }
}
