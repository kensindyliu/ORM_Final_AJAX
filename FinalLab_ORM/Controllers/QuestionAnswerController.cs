using Entities;
using Entities.ViewModels;
using EntityService;
using Microsoft.AspNetCore.Mvc;

namespace FinalLab_ORM.Controllers
{
    public class QuestionAnswerController : Controller
    {
        QuestionAnswerService questionAnswerService = new QuestionAnswerService();

        // GET: QuestionAnswer
        public IActionResult Index()
        {
            var questionsWithAnswers = questionAnswerService.GetAllQuestionsWithAnswers();
            return View(questionsWithAnswers);
        }

        
        [HttpPost]
        public IActionResult AddQuestionWithAnswers([FromBody] QuestionWithAnswersVM questionVM)  //modified by Ken
        {
            var result = questionAnswerService.AddQuestionWithAnswers(questionVM);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateQuestionWithAnswers([FromBody] QuestionWithAnswersVM questionVM)
        {
            var result = questionAnswerService.UpdateQuestionWithAnswers(questionVM);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult DeleteQuestionWithAnswers(int questionId)
        {
            var result = questionAnswerService.DeleteQuestionWithAnswers(questionId);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetQuestionWithAnswersById(int questionId)
        {
            var questionWithAnswers = questionAnswerService.GetQuestionWithAnswersById(questionId);
            if (questionWithAnswers == null)
            {
                return NotFound();
            }
            return Ok(questionWithAnswers);
        }
    }
}
