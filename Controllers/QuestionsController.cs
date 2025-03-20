using JwtAuthApi.Data;
using JwtAuthApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JwtAuthApi.Models;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtAuthApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public QuestionsController(AppDbContext context) {
           _context = context;

        }

        [HttpGet("getAllQuestion")]
        
        public IActionResult GetAllQuestions() {

            var questions = _context.Questions.ToList();
            return Ok(questions);

        }

        

        [HttpPost("addQuestion")]
        public async Task<IActionResult> AddQuestion([FromBody] QuestionDto questionDto) {

            var question = new Question
            {
                Title = questionDto.Title,
                Answers = questionDto.Answers.Select(a => new Answer
                {
                    Text = a.Text,
                    isCorrect = a.isCorrect
                }).ToList(),
            };
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return Ok(question);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id) {
            var question = await _context.Questions.Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
                return NotFound();
            return Ok(question);

        }

        // update the Questions
     

       
    }
}
