using System;
using System.Collections.Generic;
using System.Threading;

namespace Sheet3Game
{
    struct Object
    {
        public int x;
        public int y;
        public char c;
        public ConsoleColor color;
    }

    class Program
    {
        static void SetBackgroundColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear(); //Important!
        }

        static void PrintOnPosition(int x, int y, char c, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(c);
        }

        static void PrintStringOnPosition(int x, int y, string str, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }

        static void Main()
        {
            SetBackgroundColor();
            int playfieldWidth = 60;
            Console.BufferHeight = Console.WindowHeight = 20;
            Console.BufferWidth = Console.WindowWidth = 60;



            Object player = new Object();
            player.x = 2; // Starting x
            player.y = Console.WindowHeight - 1; // Starting y
            player.c = 'O'; // Object Char
            player.color = ConsoleColor.Yellow;

            Random randomGenerator = new Random();

            // Object List that contains all the positions of the enemies
            List<Object> objects = new List<Object>();

            // Main game loop
            while (true)
            {
                int chance = randomGenerator.Next(0, 100);

                // Enemie Settings
                Object enemie = new Object();
                int rColor = randomGenerator.Next(1, 4);
                // Random color picker
                switch (rColor)
                {
                    case 1:
                        enemie.color = ConsoleColor.DarkYellow;
                        break;
                    case 2:
                        enemie.color = ConsoleColor.DarkRed;
                        break;
                    case 3:
                        enemie.color = ConsoleColor.White;
                        break;
                    case 4:
                        enemie.color = ConsoleColor.DarkGray;
                        break;
                }
                enemie.x = randomGenerator.Next(0, playfieldWidth);
                enemie.y = 0;
                enemie.c = '#';
                objects.Add(enemie);

                // User Input
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.RightArrow:
                            if (player.x + 1 < playfieldWidth)
                            {
                                player.x = player.x + 1;
                            }
                            break;

                        case ConsoleKey.LeftArrow:
                            if (player.x - 1 >= 0)
                            {
                                player.x = player.x - 1;
                            }
                            break;
                    }
                }

                List<Object> newList = new List<Object>();
                for (int i = 0; i < objects.Count; i++)
                {
                    Object oldEnemie = objects[i];
                    Object newObject = new Object();
                    newObject.x = oldEnemie.x;
                    newObject.y = oldEnemie.y + 1; // Creates the next enemie in the list
                    newObject.c = oldEnemie.c;
                    newObject.color = oldEnemie.color;

                    // Death/End Condition
                    if (newObject.y == player.y && newObject.x == player.x)
                    {
                        PrintStringOnPosition(12, 10, "You Dead boi!!", ConsoleColor.DarkRed);
                        PrintStringOnPosition(9, 12, "Press [enter] to get a life", ConsoleColor.White);
                        Console.ReadLine();
                        Console.Clear();
                        PrintStringOnPosition(10, 10, "Syke!! You have no life X_x", ConsoleColor.Black);
                        Console.ReadLine();
                        Environment.Exit(0);
                    }

                    if (newObject.y < Console.WindowHeight)
                    {
                        newList.Add(newObject);
                    }
                }
                // Old list replaces the new list
                // And life goes on...
                objects = newList;
                Console.Clear();

                //Prints player every frame
                PrintOnPosition(player.x, player.y, player.c, player.color);

                // Prints individual Enemies
                foreach (Object O in objects)
                {
                    PrintOnPosition(O.x, O.y, O.c, O.color);
                }

                Thread.Sleep(60);
            }
        }
    }

}
