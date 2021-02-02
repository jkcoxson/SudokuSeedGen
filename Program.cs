using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SeedGen
{
    class Program
    {
        public static List<List<int>> board = new List<List<int>>();
        public static List<int> row1 = new List<int>();
        public static List<int> row2 = new List<int>();
        public static List<int> row3 = new List<int>();
        public static List<int> row4 = new List<int>();
        public static List<int> row5 = new List<int>();
        public static List<int> row6 = new List<int>();
        public static List<int> row7 = new List<int>();
        public static List<int> row8 = new List<int>();
        public static List<int> row9 = new List<int>();
        public static Random rnd = new Random();
        private static readonly object syncLock = new object();
        private static int ctrlaltdel = 0;
        private static int difficulty;

        static void Main(string[] args)
        {
            Console.Write("How many puzzles would you like?  ");
            int toWrite = Int32.Parse(Console.ReadLine());
            Console.Write("Difficulty? 1-5ish  ");
            difficulty = Int32.Parse(Console.ReadLine());

            for (int i =0; i < toWrite; i++)
            {
                Console.Write("On puzzle ");
                Console.WriteLine(i);
                
                GenPuzzle();
                Console.WriteLine(board.Count);
                string puzzling="";
                if (board.Count == 9)
                {
                    puzzling = TakeOutDinner();
                }
                else
                {
                    resetPuzzle();
                    Console.WriteLine("We dead?");
                }
                
                if (puzzling != "")
                {
                    Writeout(puzzling, Boardstring());
                }
                
                resetPuzzle();


            }


            GenPuzzle();
            bool genDone = false;
            Console.WriteLine("\n");
            Console.WriteLine(TakeOutDinner());
            Console.WriteLine("\n");
            printout();
            Console.WriteLine("\n\n\nAll done, press any key to ctrl shift q q out of here");
            Console.ReadLine();

        }
        static void GenPuzzle()
        {
            //Initialize the board
            board.Add(row1);
            board.Add(row2);
            board.Add(row3);
            board.Add(row4);
            board.Add(row5);
            board.Add(row6);
            board.Add(row7);
            board.Add(row8);
            board.Add(row9);
            for (int i = 0; i < 9; i++)
            {
                for (int o = 1; o < 10; o++)
                {
                    board[i].Add(0);
                }
            }
            //printout();
            Console.WriteLine(NumbColumn(0, 0));
            bool puzzlefilled = false;
            while (!puzzlefilled)
            {
                ctrlaltdel = 0;
                FillBox(0, 0);
                if (ctrlaltdel < 500)
                {
                    FillBox(3, 0);
                }
                if (ctrlaltdel < 500)
                {
                    FillBox(6, 0);
                }
                if (ctrlaltdel < 500)
                {
                    FillBox(0, 3);
                }
                if (ctrlaltdel < 500)
                {
                    FillBox(3, 3);
                }
                if (ctrlaltdel < 500)
                {
                    FillBox(6, 3);
                }
                if (ctrlaltdel < 500)
                {
                    FillBox(0, 6);
                }
                if (ctrlaltdel < 500)
                {
                    FillBox(6, 6);
                }
                if (ctrlaltdel < 500)
                {
                    FillBox(3, 6);
                }
                if (NumbColumn(0,0) | NumbColumn(0,1) | NumbColumn(0,2) | NumbColumn(0,3) | NumbColumn(0,4) | NumbColumn(0,5) | NumbColumn(0,6) | NumbColumn(0, 8))
                {
                    board = new List<List<int>>();
                    row1 = new List<int>();
                    row2 = new List<int>();
                    row3 = new List<int>();
                    row4 = new List<int>();
                    row5 = new List<int>();
                    row6 = new List<int>();
                    row7 = new List<int>();
                    row8 = new List<int>();
                    row9 = new List<int>();
                    board.Add(row1);
                    board.Add(row2);
                    board.Add(row3);
                    board.Add(row4);
                    board.Add(row5);
                    board.Add(row6);
                    board.Add(row7);
                    board.Add(row8);
                    board.Add(row9);

                    for (int i = 0; i < 9; i++)
                    {
                        for (int o = 1; o < 10; o++)
                        {
                            board[i].Add(0);
                        }
                    }
                    //Console.WriteLine(board[0][0]);
                }
                else
                {
                    puzzlefilled = true;
                    //printout();
                }
            }
            //Console.ReadKey();
            
        }


        static void printout()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int o = 0; o < 9; o++)
                {
                    Console.Write(board[i][o]);
                }
                Console.WriteLine(" ");
            }
        }


        static string Boardstring()
        {
            string output = "";
            for (int i = 0; i < 9; i++)
            {
                for (int o = 0; o < 9; o++)
                {
                    output+=(board[i][o]);
                }
                
            }
            return output;
        }


        /// <summary>
        /// Returns if the column contains the specified number or not. True if it exists.
        /// 
        /// </summary>
        /// <param name="tester">Number to check for.</param>
        /// <param name="column">Column to check in.(0-8)</param>
        /// <returns></returns>
        static bool NumbColumn(int tester,int column)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i][column] == tester)
                {
                    return true;
                }
            }


            return false;
        }

       
        /// <summary>
        /// Fills one of the 9 boxes in sudoku. Pretty fun.
        /// </summary>
        /// <param name="x">The top left corner.</param>
        /// <param name="y">The top left corner.</param>
        static void FillBox(int x, int y)
        {
            bool filled = false;
            while (!filled)
            {
                List<int> available = new List<int>();
                
                available.Add(1);
                available.Add(2);
                available.Add(3);
                available.Add(4);
                available.Add(5);
                available.Add(6);
                available.Add(7);
                available.Add(8);
                available.Add(9);


                bool single = false;
                int failsafe = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int o = 0; o < 3; o++)
                    {
                        if (ctrlaltdel < 500)
                        {
                            //Console.WriteLine("Are you here again?");
                            single = false;
                            while (!single)
                            {
                                int date;
                                lock (syncLock)
                                {
                                    date = rnd.Next(-1, available.Count);
                                }

                                if (date == -1)
                                {
                                    date = 0;
                                }
                                if (!NumbColumn(available[date], x + i) && !board[y + o].Contains(available[date]))
                                {
                                    //Console.WriteLine(date);
                                    board[y + o][x + i] = available[date];
                                    available.RemoveAt(date);
                                    single = true;
                                    //printout();







                                }
                                else
                                {
                                    //Console.WriteLine("Skippedy doo da");
                                    failsafe++;
                                    if (failsafe > 50)
                                    {
                                        i = 0;
                                        o = 0;
                                        available = new List<int>();
                                        available.Add(1);
                                        available.Add(2);
                                        available.Add(3);
                                        available.Add(4);
                                        available.Add(5);
                                        available.Add(6);
                                        available.Add(7);
                                        available.Add(8);
                                        available.Add(9);
                                        board[y][x] = 0;
                                        board[y + 1][x] = 0;
                                        board[y + 2][x] = 0;
                                        board[y][x + 1] = 0;
                                        board[y][x + 2] = 0;
                                        board[y + 1][x + 1] = 0;
                                        board[y + 1][x + 2] = 0;
                                        board[y + 2][x + 1] = 0;
                                        board[y + 2][x + 2] = 0;
                                        //Console.WriteLine("Houston, let's try this again");
                                        failsafe = 0;
                                        ctrlaltdel++;
                                        if (ctrlaltdel > 500)
                                        {
                                           
                                            i = 3;
                                            o = 3;
                                            
                                            //Console.WriteLine("Abort mission, we'll get em next time");
                                            single = true;
                                            System.Threading.Thread.Sleep(2000);



                                        }

                                    }
                                }

                            }
                        }
                        
                    }
                }
                filled = true;
                
            }
            
        }


        private static string[] python(string board)
        {
            string progToRun = "Sudoku.py";
            char[] splitter = { '\r' };
            Process proc = new Process();
            proc.StartInfo.FileName = "python.exe";
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;

            // call hello.py to concatenate passed parameters
            proc.StartInfo.Arguments = string.Concat(progToRun + " " + board);
            proc.Start();

            StreamReader sReader = proc.StandardOutput;
            string[] output = sReader.ReadToEnd().Split(splitter);

            foreach (string s in output)
                //Console.WriteLine(s);

            proc.WaitForExit();

            return output;
        }



        private static string TakeOutDinner()
        {
            Console.WriteLine("\n");
            int seatbelt = 0;
            string theend;
            //Console.Write("Enter a difficulty 1-5:   ");
            //difficulty = Int32.Parse(Console.ReadLine());

            string dinner = "";
            for (int i = 0; i < board.Count; i++)
            {
                for (int p = 0; p < board[0].Count; p++)
                {
                    dinner += board[i][p];
                    
                }
            }
            int toRemove=1;
            if (difficulty == 1)
            {
                toRemove = rnd.Next(15,20);
            }
            if (difficulty == 2)
            {
                toRemove = rnd.Next(21, 30);
            }
            if (difficulty == 3)
            {
                toRemove = rnd.Next(31, 40);
            }
            if (difficulty == 4)
            {
                toRemove = rnd.Next(45, 52);
            }
            if (difficulty == 5)
            {
                toRemove = rnd.Next(55, 64);
            }
            
            string breakfast = (dinner.Clone()).ToString();
            for (int i = 0; i <= toRemove; i++)
            {
                int target = rnd.Next(80);
                while (breakfast[target] == ' ' | breakfast[target] == '0')
                {
                    target = rnd.Next(80);
                }
                char backup = breakfast[target];
                breakfast = ReplaceAt(breakfast, target, '0');
                //Console.WriteLine(String.Concat("Length: " + breakfast.Length));
                string[] golf=python(breakfast);
                List<string> clubs = new List<string>();
                for (int p = 0; p < golf.Length; p++)
                {
                    clubs.Add(golf[p]);
                }
                bool holeinone = false;
                for (int p =0; p < clubs.Count; p++)
                {
                    if (clubs[p].Contains("Completely solved!"))
                    {
                        holeinone = true;
                    }
                }
                for (int p = 0; p < clubs.Count; p++)
                {
                    if (clubs[p].Contains("ValueError: Invalid Sudoku board"))
                    {
                        holeinone = true;
                        breakfast = "";
                    }
                }
                //Console.WriteLine(holeinone);
                //Console.WriteLine(String.Concat("Trying to remove " + toRemove));
                if (!holeinone)
                {
                    seatbelt++;
                    if (seatbelt > 500)
                    {
                        breakfast = (dinner.Clone()).ToString();
                        i = 0;
                    }
                    else
                    {
                        breakfast = ReplaceAt(breakfast, target, backup);
                        i = i - 1;
                        seatbelt = 0;
                    }
                    
                }

            }

            return breakfast;
        }


        public static string ReplaceAt(string input, int index, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }


        public static void resetPuzzle()
        {
            board = new List<List<int>>();
            row1 = new List<int>();
            row2 = new List<int>();
            row3 = new List<int>();
            row4 = new List<int>();
            row5 = new List<int>();
            row6 = new List<int>();
            row7 = new List<int>();
            row8 = new List<int>();
            row9 = new List<int>();
            board.Add(row1);
            board.Add(row2);
            board.Add(row3);
            board.Add(row4);
            board.Add(row5);
            board.Add(row6);
            board.Add(row7);
            board.Add(row8);
            board.Add(row9);

            for (int i = 0; i < 9; i++)
            {
                for (int o = 1; o < 10; o++)
                {
                    board[i].Add(0);
                }
            }
            Console.WriteLine("Reset");
        }

        public static void Writeout(string puzzlingPuzzle, string donePuzzle)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Output\puzzles.txt", true))
            {
                file.WriteLine(puzzlingPuzzle);
                file.WriteLine(donePuzzle);
                
            }




        }

    }
}
