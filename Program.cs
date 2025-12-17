using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectA1
{

    internal class Program
    {
        // мое дополнение
        static int Authorization()
        {
            Console.WriteLine("________Панель урпавления Больницы__________");
            Console.WriteLine("1 - Главный врач");
            Console.WriteLine("2 - Регистратура");
            Console.WriteLine("3 - Врач");
            Console.Write("Введите вашу роль:");

            int roleNumber = int.Parse(Console.ReadLine());

            return roleNumber;
        }

static void ShowPatientInfo(string filepath, int roleNumber)
{
    while (true)
    {
        Console.Clear();
        SetNormalColors();
        
        if (!File.Exists(filepath))
        {
            Console.WriteLine($"Файл не найден!");
            Console.ReadKey();
            return;
        }

        // Читаем файл
        string[] lines = File.ReadAllLines(filepath);
        
        Console.WriteLine("===========ИНФОРМАЦИЯ===========");
        
        // Показываем все строки
        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }
        
        Console.WriteLine("=================================");

        // Показываем меню
        if (roleNumber == 1) // Главный врач
        {
            Console.WriteLine("\n[1-6] Изменить поле");
            Console.WriteLine("[Esc] Назад к списку");
            
            ConsoleKeyInfo key = Console.ReadKey(true);
            
            if (key.Key == ConsoleKey.Escape)
            {
                return; // выходим из метода
            }
            else if (key.Key >= ConsoleKey.D1 && key.Key <= ConsoleKey.D6)
            {
                int fieldNumber = (int)key.Key - (int)ConsoleKey.D1 + 1;
                
                // Находим строку с этим номером
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith($"({fieldNumber})"))
                    {
                        Console.Clear();
                        Console.WriteLine($"Текущее значение: {lines[i]}");
                        Console.Write("Введите новое значение (всю строку): ");
                        string newValue = Console.ReadLine();
                        
                        // ПРОСТАЯ ЗАМЕНА - меняем всю строку
                        lines[i] = newValue;
                        
                        // Сохраняем в файл
                        File.WriteAllLines(filepath, lines);
                        
                        Console.WriteLine("Изменения сохранены! Нажмите любую клавишу...");
                        Console.ReadKey();
                        
                        // После сохранения выходим из цикла for
                        break;
                    }
                }
            }
        }
        else if (roleNumber == 2) // Регистратура
        {
            Console.WriteLine("\n[Enter] Выдать талон");
            Console.WriteLine("[Esc] Назад к списку");
            
            ConsoleKeyInfo key = Console.ReadKey(true);
            
            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine("ВЫДАЧА ТАЛОНА");
                
                Console.Write("Введите дату приема (дд.мм.гггг): ");
                string date = Console.ReadLine();
                
                Console.Write("Введите время приема (чч:мм): ");
                string time = Console.ReadLine();
                
                // Читаем информацию о пациенте из файла
                string[] patientInfo = File.ReadAllLines(filepath);
                string patientName = patientInfo[0].Replace("(1) ФИО: ", "");
                
                Console.WriteLine($"\nТалон выдан пациенту: {patientName}");
                Console.WriteLine($"Дата: {date}");
                Console.WriteLine($"Время: {time}");
                Console.WriteLine($"Номер талона: {new Random().Next(1000, 9999)}");
                
                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey();
            }
        }
        else if (roleNumber == 3) // Врач
        {
            Console.WriteLine("\n[Enter] Добавить запись о посещении");
            Console.WriteLine("[Esc] Назад к списку");
            
            ConsoleKeyInfo key = Console.ReadKey(true);
            
            if (key.Key == ConsoleKey.Escape)
            {
                return;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine("ДОБАВЛЕНИЕ ЗАПИСИ О ПОСЕЩЕНИИ");
                
                Console.Write("Дата (дд.мм.гггг): ");
                string date = Console.ReadLine();
                
                Console.Write("Жалобы: ");
                string complaints = Console.ReadLine();
                
                Console.Write("Диагноз: ");
                string diagnosis = Console.ReadLine();
                
                Console.Write("Назначения: ");
                string prescription = Console.ReadLine();
                
                Console.Write("Больничный (да/нет): ");
                string sickLeave = Console.ReadLine();
                
                string sickLeaveDays = "";
                if (sickLeave.ToLower() == "да")
                {
                    Console.Write("Срок больничного (с-по): ");
                    sickLeaveDays = Console.ReadLine();
                }
                
                Console.Write("ФИО врача: ");
                string doctor = Console.ReadLine();
                
                // Формируем новую запись
                string newVisit = $"{lines.Length - 5}) Дата: {date}";
                newVisit += $"\n   Жалобы: {complaints}";
                newVisit += $"\n   Диагноз: {diagnosis}";
                newVisit += $"\n   Назначения: {prescription}";
                newVisit += $"\n   Больничный: {sickLeave}";
                
                if (sickLeave.ToLower() == "да")
                {
                    newVisit += $"\n   Срок больничного: {sickLeaveDays}";
                }
                
                newVisit += $"\n   Врач: {doctor}";
                
                // Добавляем запись в конец файла
                List<string> newLines = new List<string>(lines);
                newLines.Add(newVisit);
                File.WriteAllLines(filepath, newLines);
                
                Console.WriteLine("\nЗапись добавлена! Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
    }
}
        
        
        static void OutputResult(int roleNumber)
        {
            while (true)
            {
                SetNormalColors();
                String[] categories = GetCategoryFromStorage(roleNumber);
                int selectedCategoryIndex = 0;
                string[] products = new string[0];
                int selectedProductIndex = 0;

                if (roleNumber == 1)
                {
                    while (true)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();

                        DrawRectangleQuick(4,4, 60, 20);
                        selectedCategoryIndex = SelectIndexHorizontal(5, 5, categories, selectedCategoryIndex);
                        string category = categories[selectedCategoryIndex];
                        products = GetProductsFromFile(category, roleNumber);

                        selectedProductIndex = SelectIndexVertical(5, 7, products, selectedProductIndex);

                        if (selectedProductIndex == -1)
                        {
                            break;
                        }
                
                        break;
                    }
                } 
                else if (roleNumber == 2)
                {
                    while (true)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();

                        DrawRectangleQuick(4,4, 60, 20);
                        selectedCategoryIndex = SelectIndexHorizontal(5, 5, categories, selectedCategoryIndex);
                        string category = categories[selectedCategoryIndex];
                        products = GetProductsFromFile(category, roleNumber);

                        selectedProductIndex = SelectIndexVertical(5, 7, products, selectedProductIndex);

                        if (selectedProductIndex == -1)
                        {
                            break;
                        }
                        
                        break;
                    }
                }
                else if (roleNumber == 3)       
                {
                    while (true)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();

                        DrawRectangleQuick(4,4, 60, 20);
                        selectedCategoryIndex = SelectIndexHorizontal(5, 5, categories, selectedCategoryIndex);
                        string category = categories[selectedCategoryIndex];
                        products = GetProductsFromFile(category, roleNumber);

                        selectedProductIndex = SelectIndexVertical(5, 7, products, selectedProductIndex);

                        if (selectedProductIndex == -1)
                        {
                            break;
                        }
                
                        break;
                    }
                }

                if (selectedProductIndex == -1)
                {
                    continue;
                }

                string selectFileNumber = products[selectedProductIndex];
                string categoryName = categories[selectedCategoryIndex];
                
                string filepath = "";

                switch (categoryName)
                {
                    case "Пациенты":
                        filepath = $"DB/Patients/{selectFileNumber}.txt";
                        break;
                    case "Врачи":
                        filepath = $"DB/Doctors/{selectFileNumber}.txt";
                        break;
                    case "Участки":
                        filepath = $"DB/Areas/{selectFileNumber}.txt";
                        break;
                    case "Мои пациенты": // ДОБАВЬ ЭТО
                        filepath = $"DB/Patients/{selectFileNumber}.txt";
                        break;
                }

                ShowPatientInfo(filepath, roleNumber);
            }
        }
        
        static void SetNormalColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = true;
        }


        // мое дополнение
        static void DrawRectangleQuick(int x, int y, int width, int height)
        {
            Console.CursorVisible = false;

            string topString = "┌" + new string('─', width - 2) + "┐";
            // верхний левый уголок
            Console.SetCursorPosition(x, y);
            Console.Write(topString);

            string bottomString = "└" + new string('─', width - 2) + "┘";

            // нижний левый уголок
            Console.SetCursorPosition(x, y + height - 1);
            Console.Write(bottomString); // 192

            string spaceString = new string(' ', width - 2);
            string contentString = "│" + spaceString + "│";

            // РИСУЕМ ВЕРТИКАЛИ
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(contentString);
            }

            Console.CursorVisible = true;
        }

        static void ShowMenuItems( int x, int y, string[] items, int selectedIndex)
        {

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < items.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(items[i]);
            }
            // один элемент покрасим
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x, y + selectedIndex);
            Console.WriteLine(items[selectedIndex]);
        }

        static int SelectIndexVertical(int x, int y, string[] items, int selectedIndex)
        {
            Console.CursorVisible = false;

            if (selectedIndex < 0) selectedIndex = 0;

            if (selectedIndex > items.Length-1)
                selectedIndex = items.Length-1;

            bool isWorking = true;
            while (isWorking)
            {
                ShowMenuItems(x, y, items, selectedIndex);

                Console.SetCursorPosition(x, y + items.Length + 2);
                Console.WriteLine("[Enter] Выбрать  [Esc] Назад к категориям");
                // ждём и считываем 1 кнопку
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                // посмотреть что за кнопка и выполнить действия
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        isWorking = false;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedIndex < items.Length - 1)
                            selectedIndex += 1;
                        break;
                    case ConsoleKey.UpArrow:
                        if (selectedIndex > 0)
                            selectedIndex -= 1;
                        break;
                    case ConsoleKey.Escape:
                        return -1;
                }
            }

            Console.CursorVisible = true;
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            return selectedIndex;
        }
        static void DrawHorizontalMenu( int x, int y, string[] menu, int selectedIndex)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x, y);

            int[] positionsX = new int[menu.Length];

            int position = x;
            for (int i = 0; i < menu.Length; i++)
            {
                string word = menu[i];

                positionsX[i] = position;

                Console.Write(word + "  ");
                position += word.Length + 2;
            }

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(positionsX[selectedIndex], y);
            Console.Write(menu[selectedIndex]);
        }

        static int SelectIndexHorizontal(int x, int y, string[] menu, int selectedIndex)
        {
            bool isWorking = true;
            Console.CursorVisible = false;

            while (isWorking)
            {
                DrawHorizontalMenu(x, y, menu, selectedIndex);

                ConsoleKeyInfo info = Console.ReadKey(true);
                switch (info.Key)
                {
                    case ConsoleKey.Enter:
                        isWorking = false;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (selectedIndex > 0)
                            selectedIndex--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (selectedIndex < menu.Length - 1)
                            selectedIndex++;
                        break;
                }
            }

            Console.CursorVisible = true;

            return selectedIndex;
        }

        static string[] GetCategoryFromStorage(int roleNumber)
        {
           string[] categoryes = new string[0];

            switch (roleNumber)
            {
                case 1: // выдаем категорий для ГлавВрача
                    categoryes = new[] {"Пациенты", "Врачи", "Участки"};
                    break;
                case 2: // Регистратура
                    categoryes = new[] {"Пациенты", "Врачи"};
                    break;
                case 3: // Врачи
                    categoryes = new[] {"Мои пациенты"};
                    break;
            }

            return categoryes;
        }

        static int CountLinesFile(string filepath)
        {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            int count = 0; 
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                count++;
            }

            sr.Close();
            fs.Close();
            return count;
        }

        static string[] GetProductsFromFile(string category, int roleNumber)
        {
            string folderPath = "";

            switch (category)
            {
                case "Пациенты":
                    folderPath = Path.Combine("DB", "Patients");
                    break;
                case "Врачи":
                    folderPath = Path.Combine("DB", "Doctors");
                    break;
                case "Участки":
                    folderPath = Path.Combine("DB", "Areas");
                    break;
                case "Мои пациенты":
                    folderPath = Path.Combine("DB", "Patients");
                    break;
                default:
                    return new string[0];
            }

            DirectoryInfo di = new DirectoryInfo(folderPath);
            FileInfo[] files = di.GetFiles("*.txt");

            string[] products = new string[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                products[i] = Path.GetFileNameWithoutExtension(files[i].Name);
            }

            return products;
        }

        static void Main(string[] args)
        {
            
            // вызываем авторизацию 
            int roleNumber = Authorization();
            // выводим таблицу с данными в зависимотсти от должности 
            OutputResult(roleNumber);
            // даем информацию чтобы вывеси картачку поциекта         



        }
    }
}
