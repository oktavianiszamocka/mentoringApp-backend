using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalNoteController : ControllerBase
    {
        private readonly s17874Context _context;

        public PersonalNoteController(s17874Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult getPersonalNoteList(int pageNumber, int pageSize)
        {
            var result = _context.PersonalNote.OrderByDescending(Note => Note.CreatedOn).ToList();
            return Ok(result.ToPagedList(pageNumber, pageSize));
        }
    }
}