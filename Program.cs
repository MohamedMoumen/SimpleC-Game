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
        static ConsoleColor RandomColor()
        {
            Random randomGenerator = new Random();
            int rColor = randomGenerator.Next(1, 6);
            switch (rColor)
            {
                case 1:
                    return ConsoleColor.DarkYellow;
                    
                case 2:
                    return ConsoleColor.DarkRed;
                    
                case 3:
                    return ConsoleColor.White;
                    
                case 4:
                    return ConsoleColor.DarkGray;
                    
                case 5:
                    return ConsoleColor.DarkCyan;
                    
                case 6:
                    return ConsoleColor.DarkGray;
                    
            }
            return 0;
        }

        static char RandomEnemie()
        {
            Random randomGenerator = new Random();
            int rColor = randomGenerator.Next(1, 6);
            switch (rColor)
            {
                case 1:
                    return '^';
                    
                case 2:
                    return '@';
                    
                case 3:
                    return '*';
                    
                case 4:
                    return '$';

                case 5:
                    return '&';

                case 6:
                    return '+';       
            }
            return 'a';
        }

        static void SetBackgroundColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
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
            player.x = playfieldWidth/2; // Starting x
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
                int rColor = randomGenerator.Next(1, 7);
                // Random color picker
                enemie.color = RandomColor();
                // Random enemie char picker
                enemie.c = RandomEnemie();
                enemie.x = randomGenerator.Next(0, playfieldWidth);
                enemie.y = 0;
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
                        PrintStringOnPosition(10, 10, "Syke!! You have no life X_x", ConsoleColor.DarkBlue);
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
