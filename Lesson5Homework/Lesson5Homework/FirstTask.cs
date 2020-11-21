using System.Text.RegularExpressions;

//Олесов Максим

/*1.Создать программу, которая будет проверять корректность ввода логина. Корректным логином будет строка от 2 до 10 символов,
    содержащая только буквы латинского алфавита или цифры, при этом цифра не может быть первой:
а) без использования регулярных выражений;
б) с использованием регулярных выражений.*/

namespace Lesson5Homework
{
    class FirstTask
    {
        public void Run()
        {
            View view = new View();
            view.Print("Задача 1. Проверка корректности ввода логина");

            string login = view.GetString("Пожалуйста, ввведите логин\nЛогин должен содержать от 2 до 10 символов, состоять только из букв латинского алфавита и цифр, цифра не может быть первой\n");
            CheckWithoutRegex(login);
            CheckWithRegex(login);

            view.Pause();
        }

        private void CheckWithoutRegex(string login)
        {
            View view = new View();
            view.Print("\nПроверка без использования регулярных выражений");
            bool correct = true;


            if (login.Length >= 2 && login.Length <= 10)
                if (!char.IsDigit(login[0]))
                {
                    foreach (char ch in login)
                        if (!(ch >= 'a' && ch <= 'z') && !(ch >= 'A' && ch <= 'Z') && !(ch >= '0' && ch <= '9'))
                            correct = false;
                }
                else
                    correct = false;
            else
                correct = false;

            if (correct)
                  view.Print("Верно");
              else
                  view.Print("Логин имеет неверный формат");
        }

        private void CheckWithRegex(string login)
        {
            View view = new View();
            view.Print("\nПроверка c использованием регулярных выражений");
            Regex regex = new Regex("^[a-zA-Z][a-zA-Z0-9]{1,9}$", RegexOptions.Singleline);
            if (regex.IsMatch(login))
                view.Print("Верно");
            else
                view.Print("Логин имеет неверный формат");
        }
    }
}
