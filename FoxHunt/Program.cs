using System;

namespace FoxHunt
{
    class Program
    {

        //Debug mode для удобной проверки корректности
        public static bool Debug = false;

        //Размер поля 
        public static int Size = 5;

        //Количество лисиц
        public static int Fox = 5;

        //Количество пуль
        public static int Ammo = 10;




        static void Main(string[] args)
        {
            Field GameField = new Field();
            GameField.spawnFox();

            do
            {
                if (Debug == true)
                {
                    GameField.showFieldWithFox();
                }
                else
                {
                    GameField.showField();
                }
                Console.WriteLine($"Патроны: {GameField.Ammo}");
                Console.WriteLine($"Лисицы рядом: {GameField.getFoxInRow()}");
                Console.WriteLine($"Лисиц всего: {GameField.FoxLiving}");

                Console.WriteLine("\nВведите X: ");
                int x = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите Y: ");
                int y = int.Parse(Console.ReadLine());

                //Попытка убийства, выделено цветами для более простого понимания 
                Console.Clear();
                if (GameField.tryKillFox(x, y))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Вы попали!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Вы не попали!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                //Проверка на победу или проигрыш
            } while (GameField.Ammo != 0 && GameField.FoxLiving != 0);


            Console.Clear();
            if (GameField.FoxLiving == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Вы выиграли!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Вы проиграли!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
