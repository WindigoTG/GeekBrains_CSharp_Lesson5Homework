using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

//Олесов Максим

/*4.Задача ЕГЭ.
* На вход программе подаются сведения о сдаче экзаменов учениками 9-х классов некоторой средней
школы. В первой строке сообщается количество учеников N, которое не меньше 10, но не
превосходит 100, каждая из следующих N строк имеет следующий формат:
< Фамилия > < Имя > < оценки >,
где < Фамилия > — строка, состоящая не более чем из 20 символов, <Имя> — строка, состоящая не
более чем из 15 символов, <оценки> — через пробел три целых числа, соответствующие оценкам по
пятибалльной системе. <Фамилия> и <Имя>, а также <Имя> и <оценки> разделены одним пробелом.
Пример входной строки:
Иванов Петр 4 5 3
Требуется написать как можно более эффективную программу, которая будет выводить на экран
фамилии и имена трёх худших по среднему баллу учеников. Если среди остальных есть ученики,
набравшие тот же средний балл, что и один из трёх худших, следует вывести и их фамилии и имена.*/

namespace Lesson5Homework
{
    class FourthTask
    {
        struct Student
        {
            public string FirstName { get; private set; }
            public string LastName { get; private set; }
            public string Marks { get; private set; }
            public double Average { get; private set; }
            public Student(string data)
            {
                Regex regFName = new Regex("^[А-Я][а-яё]{1,14}");
                FirstName = regFName.Match(data).ToString();
                Regex regLName = new Regex(@"\s[А-Я][а-яё]{1,19}\s");
                LastName = regLName.Match(data).ToString().Trim(' ');
                Regex regMarks = new Regex(@"\d\s\d\s\d");
                Marks = regMarks.Match(data).ToString();
                string[] marks = Marks.Split(' ');
                Average = (double.Parse(marks[0]) + double.Parse(marks[1]) + double.Parse(marks[2])) / 3;
            }

            public override string ToString()
            {
                return $"{FirstName} {LastName},\tоценки: {Marks},\tсредний балл: {Average:0.00}";
            }
        }

        public void Run()
        {
            View view = new View();
            view.Print("Задача 4. Определение учеников, набравших худший средний балл\n");

            string path = "Students.txt";

            Student[] students;

            if (File.Exists(path))
            {
                try
                {
                    students = LoadData(path);
                    for (int i = 0; i < students.Length; i++)
                        if (students[i].Average > 0)
                            view.Print($"{i + 1}: {students[i]}");
                    
                    DisplayWorst(students);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    view.Print("Что-то пошло не так.\nПроверьте правильность заполнения файла с данными об учениках");
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

        private Student[] LoadData(string path)
        {
            View view = new View();
            Student[] students;
            using StreamReader sr = new StreamReader(path, Encoding.UTF8);

            int N = int.Parse(sr.ReadLine());
            students = new Student[N];
            Regex regex = new Regex(@"[А-Я][а-яё]{1,14}\s[А-Я][а-яё]{1,19}(\s[0-9]){3}$");
            for (int i = 0; i < N; i++)
            {
                string result = sr.ReadLine();
                if (regex.IsMatch(result))
                    students[i] = new Student(result);
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    view.Print($"Ученик номер {i + 1} - данные не соответствуют требованиям");
                    Console.ResetColor();
                }
            }
            return students;
        }

        private void DisplayWorst(Student[] students)
        {
            View view = new View();

            //Для определения трёх худших средних результатов создаем массив, содержащий средние результаты всех учеников и сортируем его по возрастанию.
            double[] results = new double[students.Length];
            for (int i = 0; i < students.Length; i++)
                results[i] = students[i].Average;
            Array.Sort(results);

            //За худший результат берем первый элемент массива
            double theWorst = results[0];
            //По умолчанию второй худший результат берем равным первому, на случай, если все результаты равны, после чего проходим по массиву, в поисках другого результата.
            double scndWorst = theWorst;
            int j = 1;
            do
            {
                if (scndWorst != results[j])
                    scndWorst = results[j];
                else
                    j++;
            } while (scndWorst == theWorst && j < results.Length);
            //Аналогично с третьим худшим результатом
            double thrdWorst = scndWorst;
            j = 2;
            do
            {
                if (thrdWorst != results[j])
                    thrdWorst = results[j];
                else
                    j++;
            } while (thrdWorst == scndWorst && j < results.Length);


            view.Print("\nХудший средний балл набрали:\n");
            foreach (Student student in students)
                if (student.Average == theWorst)
                    view.Print(student.ToString());
            view.Print("");
            foreach (Student student in students)
                if (student.Average == scndWorst)
                    view.Print(student.ToString());
            view.Print("");
            foreach (Student student in students)
                if (student.Average == thrdWorst)
                    view.Print(student.ToString());
        }
    }
}
