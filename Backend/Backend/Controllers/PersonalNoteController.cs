﻿using System.Linq;
using System.Threading.Tasks;
using Backend.Extensions;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalNoteController : ControllerBase
    {
        private const int DefaultPageSize = 10;
        private readonly s17874Context _context;

        public PersonalNoteController(s17874Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonalNoteList(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            return Ok(await _context.PersonalNote
                         .OrderByDescending(Note => Note.CreatedOn)
                         .GetPage(pageNumber, pageSize)
                         .ToListAsync());
        }
    }
}