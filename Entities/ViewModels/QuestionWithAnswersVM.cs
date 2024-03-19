using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class QuestionWithAnswersVM
    {
        public int QuestionID { get; set; }

        public string QuestionContent { get; set; }

        //Add by ken
        public List<Answer> Answers { get; set; }

        //add by Ken, for test result
        public int UserAnswerID { get; set; }
    }
}
