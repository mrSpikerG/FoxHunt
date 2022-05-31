using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxHunt
{
    class Field
    {
        public int[,] FieldInfo { get; set; }




        //Живых лисиц
        public int FoxLiving { get; set; }

        //патронов осталось
        public int Ammo { get; set; }

        //Кординаты для поиска количества лисиц (-1 по умолчанию)
        private int oldX = -1;
        private int oldY = -1;


        public Field()
        {
            this.FieldInfo = new int[Program.Size, Program.Size];
            this.FoxLiving = Program.Fox;
            this.Ammo = Program.Ammo;
        }

        //Выводит поле (просто визуализация)
        public void showField()
        {
            for (int i = 0; i < Program.Size; i++)
            {
                for (int j = 0; j < Program.Size; j++)
                {
                    Console.Write("■  ");
                }
                Console.WriteLine();
            }
        }

        //Debug mode для проверки корректности
        public void showFieldWithFox()
        {
            for (int i = 0; i < Program.Size; i++)
            {
                for (int j = 0; j < Program.Size; j++)
                {
                    Console.Write($"{FieldInfo[i, j]}  ");
                }
                Console.WriteLine();
            }
        }

        //Создаём лисиц
        public void spawnFox()
        {
            preGenerateField();

            Random rand = new Random();
            for (int i = 0; i < Program.Fox; i++)
            {
                int iCord = rand.Next(0, 5);
                int jCord = rand.Next(0, 5);

                //Добавляем лису
                this.FieldInfo[iCord, jCord]++;
            }
        }

        //Подготовка массива к добавлению лисиц
        private void preGenerateField()
        {
            for (int i = 0; i < Program.Size; i++)
            {
                for (int j = 0; j < Program.Size; j++)
                {
                    //Выставляем значения массиву 
                    //Значения массива будут показывать количество лисиц
                    this.FieldInfo[i, j] = 0;
                }
            }
        }

        //Метод для проверки на убийство
        public bool tryKillFox(int x, int y)
        {
            x--;
            y--;
            try
            {
                if (this.FieldInfo[y, x] > 0)
                {
                    //Уменьшаем количество живых лисиц
                    this.FoxLiving--;

                    //Обновляем информацию о поле
                    this.FieldInfo[y, x]--;
                    
                    //Тратим патрон
                    this.Ammo--;
                    return true;
                }
                //Сохраняем информацию о координатах
                this.oldX = x;
                this.oldY = y;

                //Тратим патрон
                this.Ammo--;
            }
            catch (IndexOutOfRangeException e)
            {
                //Проверка на "дурака"
                Console.WriteLine("Введены некорректные координаты");
            }
            return false;
        }

        //проверка лисиц по горизонтали и вертикали от главной лисы
        public int getFoxInRow()
        {

            if (oldX == -1)
            {
                return 0;
            }

            int fox = 0;
            for (int i = 0; i < Program.Size; i++)
            {
                for (int j = 0; j < Program.Size; j++)
                {
                    if (i == oldY || j == oldX)
                    {
                        if (this.FieldInfo[i, j] != 0)
                        {
                            fox++;
                        }
                    }
                }
            }
            return fox;
        }
    }
}
