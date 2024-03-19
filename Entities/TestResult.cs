using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TestResult
    {
        //to save the testResult
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestReusltID {  get; set; }
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        public int UserID { get; set; }
    }
}
