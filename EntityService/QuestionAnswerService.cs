using Entities;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityService
{
    public class QuestionAnswerService
    {
        SchoolSystemContext schoolSystemContext = new SchoolSystemContext();

        public List<QuestionWithAnswersVM> GetAllQuestionsWithAnswers()
        {
            var questionsWithAnswers = new List<QuestionWithAnswersVM>();

            var questions = schoolSystemContext.Questions.ToList();
            foreach (var question in questions)
            {
                var answers = schoolSystemContext.Answers
                    .Where(a => a.QuestionID == question.QuestionID)
                    .Select(a => new Answer
                    {
                        AnswerID = a.AnswerID,
                        AnswerOption = a.AnswerOption,
                        IsTheCorrectOption = a.IsTheCorrectOption
                    })
                    .ToList();

                var questionWithAnswers = new QuestionWithAnswersVM
                {
                    QuestionID = question.QuestionID,
                    QuestionContent = question.QuestionContent,
                    Answers = answers
                };

                questionsWithAnswers.Add(questionWithAnswers);
            }

            return questionsWithAnswers;
        }


        public QuestionWithAnswersVM GetQuestionWithAnswersById(int questionId)
        {
            var question = schoolSystemContext.Questions.FirstOrDefault(q => q.QuestionID == questionId);
            if (question == null)
            {
                return null;
            }

            var answers = schoolSystemContext.Answers.Where(a => a.QuestionID == questionId).ToList();

            var questionWithAnswers = new QuestionWithAnswersVM
            {
                QuestionID = question.QuestionID,
                QuestionContent = question.QuestionContent,
                Answers = answers
            };

            return questionWithAnswers;
        }

        //Modified by Ken
        public string AddQuestionWithAnswers(QuestionWithAnswersVM questionVM)
        {
            //check if it already exist 
            if (schoolSystemContext.Questions.FirstOrDefault(c => c.QuestionContent == questionVM.QuestionContent) != null)
            {
                return "The question already exist";
            }

            //add question first
            Question question = new() {
                QuestionContent = questionVM.QuestionContent
            };
            
            schoolSystemContext.Questions.Add(question);
            schoolSystemContext.SaveChanges();

            //get new questionID
            Question newAddedQuestion = schoolSystemContext.Questions.FirstOrDefault(c => c.QuestionContent == questionVM.QuestionContent);
            if (newAddedQuestion == null)
            {
                return "The question failed to be added";
            }

            //add answers
            foreach(Answer ans in questionVM.Answers)
            {
                ans.QuestionID = newAddedQuestion.QuestionID;
                schoolSystemContext.Answers.Add(ans);
            }
            schoolSystemContext.SaveChanges();

            return "success";
        }

        public string UpdateQuestionWithAnswers(QuestionWithAnswersVM questionVM)
        {
            // Check if the question exists
            var existingQuestion = schoolSystemContext.Questions.FirstOrDefault(q => q.QuestionID == questionVM.QuestionID);
            if (existingQuestion == null)
            {
                return "Question not found";
            }

            // Update question content
            existingQuestion.QuestionContent = questionVM.QuestionContent;
            schoolSystemContext.SaveChanges();

            // Delete existing answers for the question
            var existingAnswers = schoolSystemContext.Answers.Where(a => a.QuestionID == questionVM.QuestionID).ToList();
            schoolSystemContext.Answers.RemoveRange(existingAnswers);
            schoolSystemContext.SaveChanges();

            // Add new answers
            foreach (var ans in questionVM.Answers)
            {
                ans.QuestionID = questionVM.QuestionID;
                schoolSystemContext.Answers.Add(ans);
            }
            schoolSystemContext.SaveChanges();

            return "success";
        }

        public string DeleteQuestionWithAnswers(int questionId)
        {
            var questionToDelete = schoolSystemContext.Questions.Find(questionId);
            if (questionToDelete != null)
            {
                var answersToDelete = schoolSystemContext.Answers.Where(a => a.QuestionID == questionId).ToList();
                schoolSystemContext.Answers.RemoveRange(answersToDelete);
                schoolSystemContext.Questions.Remove(questionToDelete);
                schoolSystemContext.SaveChanges();
                return "success";
            }
            return "Question not found";
        }
    }
}
