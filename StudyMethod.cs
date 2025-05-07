using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicBites
{
    public class StudyMethod
    {
        public StudyMethod() { }
    }

    public class FlashCardMethod : StudyMethod
    {
        public string Id { get; set; } = String.Empty;
        public double Progress { get; set; } = 0.0;
        List<FlashCard> FlashCards { get; set; } = new List<FlashCard>();
        public FlashCardMethod() { }
    }


    public class QuizMethod : StudyMethod
    {
    }

}
