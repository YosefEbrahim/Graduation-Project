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
//    public class ExamsController : ControllerBase
//    {
//        private readonly AdminContext _context;

//        public ExamsController(AdminContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Exams
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Exam>>> GetExam()
//        {
//            return await _context.Exam.ToListAsync();
//        }

//        // GET: api/Exams/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Exam>> GetExam(string id)
//        {
//            var exam = await _context.Exam.FindAsync(id);

//            if (exam == null)
//            {
//                return NotFound();
//            }

//            return exam;
//        }

//        // PUT: api/Exams/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutExam(string id, Exam exam)
//        {
//            if (id != exam.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(exam).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ExamExists(id))
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

//        // POST: api/Exams
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Exam>> PostExam(Exam exam)
//        {
//            _context.Exam.Add(exam);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (ExamExists(exam.Id))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetExam", new { id = exam.Id }, exam);
//        }

//        // DELETE: api/Exams/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteExam(string id)
//        {
//            var exam = await _context.Exam.FindAsync(id);
//            if (exam == null)
//            {
//                return NotFound();
//            }

//            _context.Exam.Remove(exam);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ExamExists(string id)
//        {
//            return _context.Exam.Any(e => e.Id == id);
//        }
//    }
//}
