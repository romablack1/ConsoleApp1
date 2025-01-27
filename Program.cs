using System;
using System.Collections.Generic;
using System.Linq;

class Abiturient
{
    public string FIO { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string EducationForm { get; set; }
    public int MathScore { get; set; }
    public int RussianScore { get; set; }
    public int InformaticsScore { get; set; }
    public int TotalScore => MathScore + RussianScore + InformaticsScore;

    public override string ToString()
    {
        return $"{FIO}, {Gender}, {DateOfBirth.ToShortDateString()}, {EducationForm}, " +
               $"Математика: {MathScore}, Русский: {RussianScore}, Информатика: {InformaticsScore}, " +
               $"Итого: {TotalScore}";
    }
}

class Program
{
    static List<Abiturient> abiturients = new List<Abiturient>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Добавить абитуриента");
            Console.WriteLine("2. Изменить абитуриента");
            Console.WriteLine("3. Удалить абитуриента");
            Console.WriteLine("4. Показать всех абитуриентов");
            Console.WriteLine("5. Показать количество абитуриентов");
            Console.WriteLine("6. Показать количество абитуриентов с баллами выше 150");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddAbiturient();
                    break;
                case "2":
                    EditAbiturient();
                    break;
                case "3":
                    DeleteAbiturient();
                    break;
                case "4":
                    ShowAbiturients();
                    break;
                case "5":
                    ShowTotalAbiturients();
                    break;
                case "6":
                    ShowAbiturientsAboveScore(150);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Некорректный ввод, попробуйте еще раз.");
                    break;
            }
        }
    }

    static void AddAbiturient()
    {
        var abiturient = new Abiturient();
        Console.Write("Введите ФИО: ");
        abiturient.FIO = Console.ReadLine();
        Console.Write("Введите пол (Мужской/Женский): ");
        abiturient.Gender = Console.ReadLine();
        Console.Write("Введите дату рождения (дд.мм.гггг): ");
        abiturient.DateOfBirth = DateTime.Parse(Console.ReadLine());
        Console.Write("Введите форму обучения (Очное/Очно-заочное/Заочное): ");
        abiturient.EducationForm = Console.ReadLine();

        abiturient.MathScore = GetScore("математике");
        abiturient.RussianScore = GetScore("русскому");
        abiturient.InformaticsScore = GetScore("информатике");

        abiturients.Add(abiturient);
        Console.WriteLine("Абитуриент добавлен.");
    }

    static int GetScore(string subject)
    {
        int score;
        while (true)
        {
            Console.Write($"Введите баллы по {subject}: ");
            if (int.TryParse(Console.ReadLine(), out score) && score >= 0)
            {
                return score;
            }
            Console.WriteLine("Некорректный ввод, попробуйте еще раз.");
        }
    }
    static void EditAbiturient()
    {
        Console.Write("Введите индекс абитуриента для редактирования (0-{0}): ", abiturients.Count - 1);
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index >= abiturients.Count)
        {
            Console.WriteLine("Некорректный индекс.");
            return;
        }

        var abiturient = abiturients[index];

        abiturient.FIO = GetUpdatedValue("Введите новое ФИО (оставьте пустым для сохранения): ", abiturient.FIO);
        abiturient.Gender = GetUpdatedValue("Введите новый пол (оставьте пустым для сохранения): ", abiturient.Gender);

        string dobString = GetUpdatedValue("Введите новую дату рождения (дд.мм.гггг, оставьте пустым для сохранения): ", abiturient.DateOfBirth.ToShortDateString());
        if (DateTime.TryParse(dobString, out DateTime dob))
            abiturient.DateOfBirth = dob;

        abiturient.EducationForm = GetUpdatedValue("Введите новую форму обучения (оставьте пустым для сохранения): ", abiturient.EducationForm);

        abiturient.MathScore = GetUpdatedScore("математике", abiturient.MathScore);
        abiturient.RussianScore = GetUpdatedScore("русскому", abiturient.RussianScore);
        abiturient.InformaticsScore = GetUpdatedScore("информатике", abiturient.InformaticsScore);


        Console.WriteLine("Абитуриент обновлён.");
    }


    static string GetUpdatedValue(string prompt, string currentValue)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? currentValue : input;
    }

    static int GetUpdatedScore(string subject, int currentScore)
    {
        Console.Write($"Введите новые баллы по {subject} (оставьте пустым для сохранения, текущее значение: {currentScore}): ");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int newScore) && newScore >= 0)
        {
            return newScore;
        }
        return currentScore; // If input is invalid, return the current value
    }


    static void DeleteAbiturient()
    {
        Console.Write("Введите индекс абитуриента для удаления (0-{0}): ", abiturients.Count - 1);
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index >= abiturients.Count)
        {
            Console.WriteLine("Некорректный индекс.");
            return;
        }
        abiturients.RemoveAt(index);
        Console.WriteLine("Абитуриент удалён.");
    }

    static void ShowAbiturients()
    {
        Console.WriteLine("Список абитуриентов:");
        foreach (var abiturient in abiturients)
        {
            Console.WriteLine(abiturient);
        }
    }

    static void ShowTotalAbiturients()
    {
        Console.WriteLine($"Количество абитуриентов: {abiturients.Count}");
    }

    static void ShowAbiturientsAboveScore(int score)
    {
        var filtered = abiturients.Where(a => a.TotalScore > score).ToList();
        Console.WriteLine($"Количество абитуриентов с баллами выше {score}: {filtered.Count}");
    }
}