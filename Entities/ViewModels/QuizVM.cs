using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class QuizVM
    {
        public User CurrentUser { get; set; }

        public List<QuestionWithAnswersVM> TestResults { get; set; }

    }
}
