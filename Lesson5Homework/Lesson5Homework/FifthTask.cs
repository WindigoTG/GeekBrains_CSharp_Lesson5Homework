using System;
using System.Text;
using System.IO;

// Олесов Максим

/*5. * *Написать игру «Верю.Не верю». В файле хранятся вопрос и ответ, правда это или нет.
    Например: «Шариковую ручку изобрели в древнем Египте», «Да».
    Компьютер загружает эти данные, случайным образом выбирает 5 вопросов и задаёт их игроку.
    Игрок отвечает Да или Нет на каждый вопрос и набирает баллы за каждый правильный ответ.*/
namespace Lesson5Homework
{
    struct Question
    { 
        public string Quest { get; private set; }
        public string Ans { get; private set; }
        public Question(string data)
        {
            string[] inputs = data.Split('/');
            Quest = inputs[0];
            Ans = inputs[1];
        }
    }
    class FifthTask
    {
        public void Run()
        {
            View view = new View();
            view.Print("Задача 5. Игhа \"Верю, Не верю\"\n");

            string path = "Questions.txt";

            Question[] questions;

            if (File.Exists(path))
            {
                try
                {
                    questions = LoadData(path);

                    Question[] selectedQuestions = new Question[5];
                    Random rnd = new Random();
                    for (int i = 0; i < selectedQuestions.Length; i++)
                        selectedQuestions[i] = questions[rnd.Next(questions.Length)];

                    int count = 0;
                    foreach (Question q in selectedQuestions)
                    {
                        view.Print(q.Quest);
                        string userAnswer;
                        do
                        {
                            userAnswer = view.GetString("Ваш ответ: \"Да\" или \"Нет\"");
                        } while (!(userAnswer.ToLower() == "да" || userAnswer.ToLower() == "нет"));
                        if (userAnswer.ToLower() == q.Ans.ToLower())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            view.Print("Верно");
                            Console.ResetColor();
                            count++;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            view.Print("Неерно");
                            Console.ResetColor();
                        }
                        view.Print("");
                    }
                    view.Print($"Вы набрали {count} балл(-ов/-а)");
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    view.Print("Что-то пошло не так.\nПроверьте правильность заполнения файла с данными");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                view.Print("Файл с данными об учениках не найден");
                Console.ResetColor();
            }

            view.Pause();
        }

        private Question[] LoadData(string path)
        {
            View view = new View();
            Question[] questions;
            using StreamReader sr = new StreamReader(path, Encoding.UTF8);

            int N = int.Parse(sr.ReadLine());
            questions = new Question[N];
            for (int i = 0; i < N; i++)
            {
                string result = sr.ReadLine();
                questions[i] = new Question(result);
            }
            return questions;
        }
    }
}
