using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicBites;

namespace TopicBites.Misc
{
    public enum NounAttribute
    {
        Singular,
        Plural
    }

    public class StudyMethodResource {
        public string Id { get; set; } = String.Empty;
        public StudyMethodResouceDifficulty Difficulty { get; set; } = StudyMethodResouceDifficulty.Beginner;
        
    }

    public enum StudyMethodResouceDifficulty
    {
        Beginner,
        Applied,
        InDepth
    }
    public enum StudyMethodStadisticsMetadata
    {
        Try,
        Correct,
        Incorrect,
        Abandoned
    }
    public class StudyMethodStadistics
    {
        public string Id { get; set; } = String.Empty;
        public Dictionary<DateTime, StudyMethodStadisticsMetadata> Tries { get; set; } = new Dictionary<DateTime, StudyMethodStadisticsMetadata>();
        public int CorrectAnswers { get; } 
        public int IncorrectAnswers { get; } 
        public int TotalTries { get;  }

    }

    public class FlashCard : StudyMethodResource { 
        public string Question { get; set; } = String.Empty;
        public string Answer { get; set; } = String.Empty;
        public FlashCard() {

        }
        public FlashCard(string id, string question, string answer)
        {
            Id = id;
            Question = question;
            Answer = answer;
        }
    }
    public class QuizQuestion : StudyMethodResource {
        public string Question { get; set; } = String.Empty;
        public List<string> Answers { get; set; } = new List<string>();
        public int CorrectAnswerIndex { get; set; } = 0;
        public QuizQuestion()
        {
        }
        public QuizQuestion(string id, string question, List<string> answers, int correctAnswerIndex)
        {
            Id = id;
            Question = question;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }
}
