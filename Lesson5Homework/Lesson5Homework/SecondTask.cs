using System;
using System.IO;
using System.Text;

//Олесов Максим

/*2. Разработать класс Message, содержащий следующие статические методы для обработки текста:
а) Вывести только те слова сообщения, которые содержат не более n букв.
б) Удалить из сообщения все слова, которые заканчиваются на заданный символ.
в) Найти самое длинное слово сообщения.
г) Сформировать строку с помощью StringBuilder из самых длинных слов сообщения.
Продемонстрируйте работу программы на текстовом файле с вашей программой.*/

namespace Lesson5Homework
{
    class SecondTask
    {
        public void Run()
        {
            View view = new View();
            view.Print("Задача 2. Класс Message для обработки текстового сообщения\n");

            if (Message.LoadMessage("Task2Example.txt"))
            { 
                Message.ShowMessage();

                view.Print("\n" + new string('-', 50) + "\n");
                int maxLength = view.GetInt("Слова не длинней скольки букв нужно отобразить?");
                Message.ShowWordsByMaxLength(maxLength);

                view.Print("\n" + new string('-', 50) + "\n");
                string endChar= view.GetString("На какой символ должны заканчиваться слова, которые нужно удалить?");
                Message.RemoveWordsByEndSymbol(endChar[0]);

                view.Print("\n" + new string('-', 50) + "\n");
                Message.LongestWord();
            }

            view.Pause();
        }

    }

    class Message
    {
        private static string _message;
        private static  View view = new View();
        public static bool LoadMessage(string path)
        {
            try
            {
             using StreamReader sr = new StreamReader(path, Encoding.UTF8);
                _message = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                view.Print(ex.Message);
                view.Print("Что-то пошло не так");
                return false;
            }
            return true;
        }

        public static void ShowMessage()
        {
            view.Print(_message);
        }

        public static void ShowWordsByMaxLength(int maxLength)
        {
            string[] words = _message.Split(' ', '.', ',', ':');
            view.Print("");
            foreach (string word in words)
                if (word.Length <= maxLength)
                    view.Print($"{word} ", false);
            
        }

        public static void RemoveWordsByEndSymbol(char ch)
        {
            string[] words = _message.Split(' ', '.', ',');
            view.Print("");
            foreach (string word in words)
                if (!word.EndsWith(ch.ToString()))
                    view.Print($"{word} ", false);
        }

        public static void LongestWord()
        {
            string[] words = _message.Split(' ', '.', ',');
            view.Print("");
            int maxLength = words[0].Length;
            for (int i = 1; i < words.Length; i++)
                if (words[i].Length > maxLength)
                    maxLength = words[i].Length;

            StringBuilder longestWords = new StringBuilder();
            foreach (string word in words)
                if (word.Length == maxLength)
                    longestWords.Append(word + " ");

            view.Print($"Самые длиные слова в сообщении:\n{longestWords}");
        }
    }


}
