using Entities;
using Entities.ViewModels;
using EntityService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using static System.Formats.Asn1.AsnWriter;

namespace FinalLab_ORM.Controllers
{
    public class QuizController : Controller
    {

        public IActionResult Index(int id)
        {
            TestService testService = new TestService();
            QuizVM qvm = testService.GetTestResults(id);
            return View(qvm);
        }

        public ActionResult UpdateTestResult([FromBody]  List<TestResult> results)
        {
            if(results.Count == 0)
            {
                return Ok(new { message = "Submition successfully!\n Your score is 0." });
            }
            TestService ts = new TestService();
            if(ts.UpdateTestResult(results))
            {
                int questionCount = 0;
                string scores = GetScores(results, out questionCount);
                return Ok(new { message = $"Submition successfully!\n{scores}" });
            }
            return NotFound(new { message = "Submition Failed!" });

        }

        public ActionResult ShowScores(int id)
        {
            SchoolSystemContext ssc = new SchoolSystemContext();
            List<TestResult> results = ssc.TestResults.Where(rs => rs.UserID== id).ToList();
            int questionCount = 0;
            string scores = GetScores(results, out questionCount);
            return Ok(new { message = $"{scores}" });
        }

        private string GetScores(List<TestResult> results, out int questionCount)
        {
            int scores = 0;
            QuestionAnswerService questionAnswerService = new QuestionAnswerService();
            List<QuestionWithAnswersVM> qvms = questionAnswerService.GetAllQuestionsWithAnswers();
            //Mark the answers and show score
            foreach (TestResult result in results)
            {
                QuestionWithAnswersVM qvm = qvms.FirstOrDefault(qvm => qvm.QuestionID == result.QuestionID);
                if (qvm != null)
                {
                    Answer answer = qvm.Answers.FirstOrDefault(ans => ans.IsTheCorrectOption);
                    if (result.AnswerID == answer.AnswerID)
                    {
                        scores++;  //correct answer
                    }
                }
            }
            questionCount = qvms.Count;
            return $"Your score is {scores}/{questionCount}.";
        }

        //public IActionResult CreateQuiz()
        //{
        //    return View();
        //}

        //public IActionResult GetQuizes()
        //{
        //    List<Quize> quizes = new List<Quize>();
        //    SchoolSystemContext context = new SchoolSystemContext();
        //    quizes = context.Quizes.ToList();
        //    return RedirectToAction("index",(quizes));
        //}

        //public List<Question> TakeQuiz(int quizId)
        //{
        //    using (SchoolSystemContext context = new SchoolSystemContext())
        //    {
        //        var quizWithQuestions = context.Quizes
        //            .Include(q => q.Questions)
        //            .FirstOrDefault(q => q.QuizID == quizId);

        //        return quizWithQuestions?.Questions.ToList();
        //    }
        //}

        //public bool AddQuiz(Quiz quiz, List<Question> questions)
        //{
        //    using (SchoolSystemContext context = new SchoolSystemContext())
        //    {
        //        try
        //        {
        //            context.Quizes.Add(quiz);
        //            context.SaveChanges();

        //            foreach (var question in questions)
        //            {
        //                question.QuizID = quiz.QuizID;
        //                context.Questions.Add(question);
        //            }

        //            context.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            return false;
        //        }
        //    }
        //}


        //public bool UpdateQuiz(Quiz quiz)
        //{
        //    SchoolSystemContext context = new SchoolSystemContext();
        //    var entityToUpdate = context.Quizes.FirstOrDefault(e => e.QuizID == quiz.QuizID);
        //    if (entityToUpdate != null)
        //    {
        //        entityToUpdate.UserID = quiz.UserID;
        //        context.SaveChanges();
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        //public bool DeleteQuiz(int quizId)
        //{
        //    SchoolSystemContext context = new SchoolSystemContext();
        //    var entityToDelete = context.Quizes.FirstOrDefault(e => e.QuizID == quizId);
        //    if (entityToDelete != null)
        //    {
        //        context.Quizes.Remove(entityToDelete);
        //        context.SaveChanges();
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        ////private void SetCookie(string userInfo)
        ////{
        ////    HttpCookie cookie = new HttpCookie("UserName");

        ////    // Set cookie value
        ////    cookie.Value = "MyCookieValue";

        ////    // Set cookie expiration (optional)
        ////    cookie.Expires = DateTime.Now.AddDays(1); // Expires in 1 day

        ////    // Add the cookie to the response
        ////    Response.Cookies.Add(cookie);
        ////} 
    }
}
