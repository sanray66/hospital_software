using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BigProj
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
            

        }
        
        static void OutputResult(int roleNumber)
        {
            String[] categories = GetCategoryFromStorage();
            int selectedCategoryIndex = 0;
            int selectedProductIndex = 0;

            if (roleNumber == 1)
            {
                while (true)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;

                    DrawRectangleQuick(4,4, 60, 20);
                    selectedCategoryIndex = SelectIndexHorizontal(5, 5, categories, selectedCategoryIndex);
                    string category = categories[selectedCategoryIndex];
                    string[] products = GetProductsFromFile(category);

                    selectedProductIndex =  SelectIndexVertical(5, 7, products, selectedProductIndex);
                }
            } 
            else if (roleNumber == 2)
            {
                while (true)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;

                    DrawRectangleQuick(4,4, 60, 20);
                    selectedCategoryIndex = SelectIndexHorizontal(5, 5, categories, selectedCategoryIndex);
                    string category = categories[selectedCategoryIndex];
                    string[] products = GetProductsFromFile(category);

                    selectedProductIndex =  SelectIndexVertical(5, 7, products, selectedProductIndex);
                }
            }
            else if (roleNumber == 3)       
            {
                 while (true)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;

                    DrawRectangleQuick(4,4, 60, 20);
                    selectedCategoryIndex = SelectIndexHorizontal(5, 5, categories, selectedCategoryIndex);
                    string category = categories[selectedCategoryIndex];
                    string[] products = GetProductsFromFile(category);

                    selectedProductIndex =  SelectIndexVertical(5, 7, products, selectedProductIndex);
                }
            }
            else
            {
                Console.WriteLine("Вы ввели некоректные данные, можно только цифры от 1 до 3");
            }
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
            if (selectedIndex > items.Length-1)
                selectedIndex = items.Length-1;

            bool isWorking = true;
            while (isWorking)
            {
                ShowMenuItems(x, y, items, selectedIndex);
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
                        Environment.Exit(0);
                        break;
                }
            }

            Console.CursorVisible = true;

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
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                }
            }

            Console.CursorVisible = true;

            return selectedIndex;
        }

        static string[] GetCategoryFromStorage(int roleNumber)
        {
            // 1) получить информацию о Директории 
            DirectoryInfo di = new DirectoryInfo("DB");
            // 2) получить список файлов - текстовые
            FileInfo[] files = di.GetFiles("*.txt");
            // 3) собираем только имена файлов
            string[] names = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                names[i] = Path.
                    GetFileNameWithoutExtension(files[i].Name);
            }

            return names;
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
        static string[] GetProductsFromFile(string filename)
        {
            // 1) формируем путь + имя файла + расширение
            string filepath = Path.Combine(
                "Storage", 
                filename+".txt");
            int count = CountLinesFile(filepath);
            string[] products = new string[count];

            FileStream fs = new FileStream(filepath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            int i = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                products[i] = line;
                i++;

            }

            sr.Close();
            fs.Close();

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
