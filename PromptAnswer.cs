using System.Collections.Generic;

namespace QuizFactory
{
    public class PromptAnswer
    {
        public string Prompt{get; set;}
        public List<Answers> Answer = new List<Answers>();

        public PromptAnswer(string prompt, string answer)
        {
            this.Prompt = prompt;
 
            this.Answer.Add(new Answers{
                Value = answer,
                IsCorrect = true
            });
        }
        
    }
}