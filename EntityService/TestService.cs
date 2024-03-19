using Entities;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityService
{
    public class TestService
    {
        public QuizVM GetTestResults(int userID)
        {
            SchoolSystemContext ssc = new SchoolSystemContext();

            List<TestResult> results = ssc.TestResults.Where(tr => tr.UserID == userID).ToList();
            List<Question> questions = ssc.Questions.ToList();
            List<Answer> answers = ssc.Answers.ToList();
            QuestionAnswerService qas = new QuestionAnswerService();
            List<QuestionWithAnswersVM> testRequltsVM = qas.GetAllQuestionsWithAnswers();

            foreach (TestResult tr2 in results)
            {
                QuestionWithAnswersVM qavm = testRequltsVM.FirstOrDefault(vm => vm.QuestionID == tr2.QuestionID);
                qavm.UserAnswerID = tr2.AnswerID;
            }

            UserService userService = new UserService();
            User user = userService.GetUsers().Where(us1 => us1.UserID == userID).FirstOrDefault();

            QuizVM qvm = new QuizVM()
            {
                CurrentUser = user,
                TestResults = ShuffleTestResults(testRequltsVM)
            };
            return qvm;
        }

        private List<QuestionWithAnswersVM> ShuffleTestResults(List<QuestionWithAnswersVM> testResults)
        {
            Random rng = new Random();
            int n = testResults.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                QuestionWithAnswersVM value = testResults[k];
                testResults[k] = testResults[n];
                testResults[n] = value;
            }
            return testResults;
        }

        public bool UpdateTestResult(List<TestResult> results)
        {
            try
            {
                int userID = results[0].UserID;
                SchoolSystemContext db = new SchoolSystemContext();
                db.TestResults.RemoveRange(db.TestResults.Where(tr => tr.UserID == userID));
                db.TestResults.AddRange(results);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
