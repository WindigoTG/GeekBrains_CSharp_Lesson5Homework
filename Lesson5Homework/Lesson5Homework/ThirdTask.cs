using System;
using System.Linq;

/*3. * Для двух строк написать метод, определяющий, является ли одна строка перестановкой другой. Регистр можно не учитывать:
а) с использованием методов C#;
б) *разработав собственный алгоритм.
Например:
badc являются перестановкой abcd.*/

namespace Lesson5Homework
{
    class ThirdTask
    {
        public void Run()
        {
            View view = new View();
            view.Print("Задача 3. Определение, является ли одна строка перестановкой другой.");

            string s1 = view.GetString("\nВведите первую строку");
            string s2 = view.GetString("\nВведите вторую строку");

            if (s1.Equals(s2))
                view.Print("\nСтроки равны изначально");
            else {
                view.Print("\nОпределение с использованием методов C#:");
                if (CompareWithCSharp(s1, s2))
                    view.Print("Вторая строка является перестановкой первой");
                else
                    view.Print("Вторая строка НЕ является перестановкой первой");

                view.Print("\n" + new string('-', 50) + "\n");

                view.Print("Определение с использованием собственного метода");
                if (CompareWithCustomMethod(s1, s2))
                    view.Print("Вторая строка является перестановкой первой");
                else
                    view.Print("Вторая строка НЕ является перестановкой первой");
            }

            view.Pause();
        }

        private bool CompareWithCSharp(string s1, string s2)
        {
            if (s1.Select(Char.ToLower).OrderBy(a => a).SequenceEqual(s2.Select(Char.ToLower).OrderBy(a => a)))
                return true;
            else return false;
        }

        private bool CompareWithCustomMethod(string s1, string s2)
        {
            char[] str1 = s1.ToLower().ToArray();
            char[] str2 = s2.ToLower().ToArray();
            if (str1.Length == str2.Length)
            {
                Array.Sort(str1);
                Array.Sort(str2);
                for (int i = 0; i < str1.Length; i++)
                    if (str1[i] != str2[i])
                        return false;
                return true;
            }
            else
                return false;
        }
    }
}
