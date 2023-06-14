//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Admin.Data;
//using Models.Models;

//namespace Admin.Areas.Admin.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LecturesController : ControllerBase
//    {
//        private readonly AdminContext _context;

//        public LecturesController(AdminContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Lectures
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Lecture>>> GetLecture()
//        {
//            return await _context.Lecture.ToListAsync();
//        }

//        // GET: api/Lectures/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Lecture>> GetLecture(string id)
//        {
//            var lecture = await _context.Lecture.FindAsync(id);

//            if (lecture == null)
//            {
//                return NotFound();
//            }

//            return lecture;
//        }

//        // PUT: api/Lectures/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutLecture(string id, Lecture lecture)
//        {
//            if (id != lecture.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(lecture).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!LectureExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Lectures
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Lecture>> PostLecture(Lecture lecture)
//        {
//            _context.Lecture.Add(lecture);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (LectureExists(lecture.Id))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetLecture", new { id = lecture.Id }, lecture);
//        }

//        // DELETE: api/Lectures/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteLecture(string id)
//        {
//            var lecture = await _context.Lecture.FindAsync(id);
//            if (lecture == null)
//            {
//                return NotFound();
//            }

//            _context.Lecture.Remove(lecture);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool LectureExists(string id)
//        {
//            return _context.Lecture.Any(e => e.Id == id);
//        }
//    }
//}
