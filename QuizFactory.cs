using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuizFactory
{
    public static class QuizFactory
    
    { 
            //   TODO; pull from CSV file

        public static List<PromptAnswer> quizData = new List<PromptAnswer>{};

        // public static List<PromptAnswer> CountryData = new List<PromptAnswer> {
        //     new PromptAnswer("Afghanistan", "Kabul"),
        //     new PromptAnswer("Albaia", "Tirana"),
        //     new PromptAnswer("Algeria", "Algiers"),
        //     new PromptAnswer("Andorra", "Andorra La Vella"),
        //     new PromptAnswer("Angola", "Luanda"),
        //     new PromptAnswer("Antigua & Barbuda", "Saint Johns"),
        //     new PromptAnswer("Argentina", "Buenos Aires"),
        //     new PromptAnswer("Armenia", "Yerevan"),
        //     new PromptAnswer("Australia", "Canberra"),
        //     new PromptAnswer("Austria", "Vienna"),
        //     new PromptAnswer("Azerbaijan", "Baku"),
        //     new PromptAnswer("Bahamas", "Nassau"),
        //     new PromptAnswer("Bahrain", "Manama"),
        //     new PromptAnswer("Bangladesh", "Bridgetown"),
        //     new PromptAnswer("Belarus", "Minsk"),
        //     new PromptAnswer("Belgium", "Brussels"),
        //     new PromptAnswer("Belize", "Belmopan"),
        //     new PromptAnswer("Benin", "Porto-Novo"),
        //     new PromptAnswer("Bhutan", "Thimphu"),
        // };

        public static void GenerateQuiz(int quizType)
        {   
            // List<PromptAnswer> quizData = new List<PromptAnwser>();
            // if 1 generate state capital
            if (quizType == 1)
            {
                // call a method to read StatesCapitals csv file
                ReadQuizData("./StatesCapitals.csv");
            }
            else if(quizType == 2)
            {
                ReadQuizData("./CountryCapital.csv");
            }
        }

        public static void ReadQuizData(string fileName)
        {
            var quizData = new List<PromptAnswer>();
            using(var reader = new StreamReader(fileName))
            {
                string line = "";
                while((line = reader.ReadLine()) != null)
                {
                   
                    string[] values = line.Split(',');
                    var promptAnswer = new PromptAnswer(values[0], values[1]);
                    quizData.Add(promptAnswer);
                }
            }

            AddIncorrectAnswers(quizData);
        }

        public static void AddIncorrectAnswers(List<PromptAnswer> quizData)
        {
 
            var possibleAnswers = quizData.Select(data => data.Answer.First(x => x.IsCorrect)).ToList();
            // Gives a list<string> of the correct answers
            var vAnswer = possibleAnswers.Select(v => v.Value).ToList();
            // Gives a list<string> of the prompts
            var questions = quizData.Select(q => q.Prompt).ToList();
            // Gives a list<sttring> of incorrect answers 
            var foil = possibleAnswers.Where(f => f.Value != vAnswer[1])
                                    .Select(f2 => f2.Value).ToList();

            PrintToFile(questions, vAnswer, foil);
        }

        public static void PrintToFile(List<string> questions, List<string> vAnswer, List<string> foil)
        {
            var curDir = Directory.GetCurrentDirectory();
            string path = @curDir + "/MyTest.txt";

            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine();
                sw.WriteLine(" Study Quiz Set");
                sw.WriteLine(" Gray-Baum Acadamy ");
                sw.WriteLine();
                
                var rnd = new Random();
                List<string> foilAnswers = new List<string>();

                for(var i = 0; i < questions.Count(); i++)
                {
                    // ensure the wrong answer list is clear
                    foilAnswers.Clear();
                    // add three wrong answers to the list
                    for (int j= 0; j < 3; j++)
                    {
                        int item = rnd.Next(0, foil.Count());
                        foilAnswers.Add(foil[item]);
                    }
                    // add one correct answer to the list and then
                    // sort the multiple choise answers so they are not
                    // all in the same order
                    foilAnswers.Add(vAnswer[i]);
                    foilAnswers.Sort();
                    sw.WriteLine("What is the capital of " + questions[i] + "?");
                    sw.WriteLine("A. " + foilAnswers[0]);
                    sw.WriteLine("B. " + foilAnswers[1]);
                    sw.WriteLine("C. " + foilAnswers[2]);
                    sw.WriteLine("D. " + foilAnswers[3]);
                    sw.WriteLine();
                }
            }
        }
    }
}