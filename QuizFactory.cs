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

        public static List<PromptAnswer> CountryData = new List<PromptAnswer> {
            new PromptAnswer("Afghanistan", "Kabul"),
            new PromptAnswer("Albaia", "Tirana"),
            new PromptAnswer("Algeria", "Algiers"),
            new PromptAnswer("Andorra", "Andorra La Vella"),
            new PromptAnswer("Angola", "Luanda"),
            new PromptAnswer("Antigua & Barbuda", "Saint Johns"),
            new PromptAnswer("Argentina", "Buenos Aires"),
            new PromptAnswer("Armenia", "Yerevan"),
            new PromptAnswer("Australia", "Canberra"),
            new PromptAnswer("Austria", "Vienna"),
            new PromptAnswer("Azerbaijan", "Baku"),
            new PromptAnswer("Bahamas", "Nassau"),
            new PromptAnswer("Bahrain", "Manama"),
            new PromptAnswer("Bangladesh", "Bridgetown"),
            new PromptAnswer("Belarus", "Minsk"),
            new PromptAnswer("Belgium", "Brussels"),
            new PromptAnswer("Belize", "Belmopan"),
            new PromptAnswer("Benin", "Porto-Novo"),
            new PromptAnswer("Bhutan", "Thimphu"),
        };

        public static List<PromptAnswer> StateData = new List<PromptAnswer>
        {
                new PromptAnswer("Alabama", "Montgomery"),
                new PromptAnswer("Alaska", "Juneau"),
                new PromptAnswer("Arizona", "Phoenix"),
                new PromptAnswer("Arkansas", "Little Rock"),
                new PromptAnswer("California", "Sacramento"),
                new PromptAnswer("Colorado", "Denver"),
                new PromptAnswer("Connecticut", "Hartford"),
                new PromptAnswer("Deleware", "Dover"),
                new PromptAnswer("Florida", "Tallahassee"),
                new PromptAnswer("Georgia", "Atlanta"),
                new PromptAnswer("Hawaii", "Honolulu"),
                new PromptAnswer("Idaho", "Bose"),
                new PromptAnswer("Illinois", "Springfield"),
                new PromptAnswer("Indiana", "Indianapolis"),
                new PromptAnswer("Iowa", "Des Moines"),
                new PromptAnswer("Kansas", "Topeka"),
                new PromptAnswer("Kentucky", "Frankfort"),
                new PromptAnswer("Louisiana", "Baton Rouge"),
                new PromptAnswer("Maine", "Augusta"),
                new PromptAnswer("Maryland", "Annapolis"),
                new PromptAnswer("Massachusetts", "Boston"),
                new PromptAnswer("Michigan", "Lansing"),
                new PromptAnswer("Minnesota", "Saint Paul"),
                new PromptAnswer("Mississippi", "Jackson"),
                new PromptAnswer("Missouri", "Jefferson City"),
                new PromptAnswer("Montana", "Helena"),
                new PromptAnswer("Nebraska", "Lincoln"),
                new PromptAnswer("Nevada", "Carson city"),
                new PromptAnswer("New Hampshire", "Concord"),
                new PromptAnswer("New Jersey", "Trenton"),
                new PromptAnswer("New Mexico", "Santa Fe"),
                new PromptAnswer("New York", "Albany"),
                new PromptAnswer("North Carolina", "Raleigh"),
                new PromptAnswer("North Dakata", "Bismarck"),
                new PromptAnswer("Ohio", "Columbus"),
                new PromptAnswer("Oklahoma", "Oklahoma City"),
                new PromptAnswer("Oregon", "Salem"),
                new PromptAnswer("Pennsylvania", "Harrisburg"),
                new PromptAnswer("Rhode Island", "Providence"),
                new PromptAnswer("South Carolina", "Columbia"),
                new PromptAnswer("South Dakota", "Pierre"),
                new PromptAnswer("Tennessee", "Nashville"),
                new PromptAnswer("Texas", "Austin"),
                new PromptAnswer("Urah", "Salt Lake City"),
                new PromptAnswer("Vermont", "Montpelier"),
                new PromptAnswer("Virginia", "Richmond"),
                new PromptAnswer("Washington", "Olympia"),
                new PromptAnswer("West Virginia", "Charleston"),
                new PromptAnswer("Wisconsin", "Madison"),
                new PromptAnswer("Wyoming", "Cheyenne")
        };
        public static void GenerateQuiz(int quizType)
        {   
            // List<PromptAnswer> quizData = new List<PromptAnwser>();
            // if 1 generate state capital
            if (quizType == 1)
            {
                // sets quizData to List<PromptAnser> for the State/City information
                ReadQuizData("./StatesCapitals.csv");
            }
            else if(quizType == 2)
            {
                // sets quizData to List<PromptAnswer> for the Country/Capital information
                // quizData = CountryData;
            }
            // Returns list<PromptAnser> {state/country, {capitol, IsCorrect}}
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