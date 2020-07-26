using System;

namespace QuizFactory
{
    class Program
    {
        static void Main(string[] args)
 {
            int quizType = 0;
            while(quizType < 3)
            {
                Console.WriteLine("Which question set would you like to use?  ");
                Console.WriteLine(" Enter 1 for a State and Capital Quiz.");
                Console.WriteLine(" Enter 2 for a Country and National Capital Quiz");
                Console.WriteLine(" Enter 3 to quit.");

                //TODO: Error Handling TryParse
                quizType = Int32.Parse(Console.ReadLine());

                if(quizType == 1 || quizType == 2)
                {
                    QuizFactory.GenerateQuiz(quizType);
                }
            } 
        }
    }
}
