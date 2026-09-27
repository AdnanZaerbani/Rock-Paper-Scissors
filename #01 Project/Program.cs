using System;

namespace _01_Project
{
    internal class Program
    {
        struct stRoundInfo
        {
            public enChoice Player1Choice;
            public enChoice ComputerChoice;
            public enWinner RoundWinner;
        }
        struct stGameResult
        {
            public sbyte Player1WonTimes;
            public sbyte ComputerWonTimes;
            public sbyte DrawTimes;
        }
        enum enWinner : sbyte
        {
            Player1 = 1 , Computer = 2 , NoWinner = 3 
            
        }
        enum enChoice : sbyte
        {
            Stone = 1 , Paper = 2 ,  Scissors = 3
        }
        static enWinner WhoWinner(stGameResult GameResult)
        {
            if (GameResult.Player1WonTimes == GameResult.ComputerWonTimes)
            {
                return enWinner.NoWinner;
            }
            else if (GameResult.Player1WonTimes > GameResult.ComputerWonTimes)
            {
                return enWinner.Player1;
            }
            return enWinner.Computer;
        }
        static void PrintResults(stGameResult GameResult, sbyte Rounds)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t\t\t\t_________________________________________________________\n");
            Console.WriteLine("\t\t\t\t\t\t+++ G a m e  O v e r +++");
            Console.WriteLine("\t\t\t\t_________________________________________________________\n");
            Console.WriteLine("\t\t\t\t_____________________ [Game Results] ____________________\n");
            Console.WriteLine($"\t\t\t\tGame Rounds        : {Rounds}");
            Console.WriteLine($"\t\t\t\tPlayer1 won times  : {GameResult.Player1WonTimes}");
            Console.WriteLine($"\t\t\t\tComputer won times : {GameResult.ComputerWonTimes}");
            Console.WriteLine($"\t\t\t\tDraw times         : {GameResult.DrawTimes}");
            Console.WriteLine($"\t\t\t\tFinal Winner       : {WhoWinner(GameResult)}");
            Console.WriteLine("\t\t\t\t_________________________________________________________\n");
        }
        static stGameResult Record(stGameResult GameResult, stRoundInfo RoundInfo)
        {
            if (RoundInfo.RoundWinner == enWinner.Player1)
            {
                GameResult.Player1WonTimes++;
                return GameResult;
            }
            else if (RoundInfo.RoundWinner == enWinner.Computer)
            {
                GameResult.ComputerWonTimes++;
                return GameResult;
            }
            GameResult.DrawTimes++;
            return GameResult;
        }
        static void InfoRound(stRoundInfo RoundInfo, sbyte Round)
        {
            Console.WriteLine($"\n____________Round[{Round}]____________\n");
            Console.WriteLine($"Player1  Choice: {RoundInfo.Player1Choice}");
            Console.WriteLine($"Computer Choice: {RoundInfo.ComputerChoice}");
            Console.WriteLine($"Round Winner   : [{RoundInfo.RoundWinner}]");
            Console.WriteLine("________________________________\n");
        }
        static enWinner WhoWinnerRound(stRoundInfo RoundInfo)
        {
            if (RoundInfo.ComputerChoice == RoundInfo.Player1Choice)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                return enWinner.NoWinner;
            }

            switch (RoundInfo.Player1Choice)
            {
                case enChoice.Stone:
                    if (RoundInfo.ComputerChoice == enChoice.Paper)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        return enWinner.Computer;
                    }
                    break;
                case enChoice.Paper:
                    if (RoundInfo.ComputerChoice == enChoice.Scissors)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        return enWinner.Computer;
                    }
                    break;
                case enChoice.Scissors:
                    if (RoundInfo.ComputerChoice == enChoice.Stone)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        return enWinner.Computer;
                    }
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            return enWinner.Player1;
        }
        static enChoice RandomComputerChoice()
        {
            Random rnd = new Random();
            return (enChoice)rnd.Next(1, 3);
        }
        static enChoice ReadPlayerChoice()
        {
            Console.Write("Your Choice: [1]:{0}, [2]:{1}, [3]:{2} ? ", enChoice.Stone, enChoice.Paper, enChoice.Scissors);
        Check:
            string Choice = Console.ReadLine();
            if (!sbyte.TryParse(Choice, out sbyte number) || number > 3 || number < 0)
            {
                Console.WriteLine("Input Erorr, Please Enter a Number between 1 to 3");
                goto Check;
            }
            return (enChoice)Convert.ToSByte(Choice);
        }
        static void CreatRound(sbyte Rounds)
        {
            stRoundInfo RoundInfo = new stRoundInfo();
            stGameResult GameResult = new stGameResult();
            for (sbyte Round = 1; Round <= Rounds; Round++)
            {
                Console.ForegroundColor= ConsoleColor.White;
                Console.WriteLine($"\nRound [{Round}] begins: \n");
                RoundInfo.Player1Choice = ReadPlayerChoice();
                RoundInfo.ComputerChoice = RandomComputerChoice();
                RoundInfo.RoundWinner = WhoWinnerRound(RoundInfo);
                InfoRound(RoundInfo, Round);
                GameResult = Record(GameResult, RoundInfo);
            }
            PrintResults(GameResult, Rounds);
        }
        static sbyte HowManyRounds()
        {
            Console.WriteLine("How Many Rounds 1 to 10 ?");
        Check:
            string Rounds = Console.ReadLine();
            if (!sbyte.TryParse(Rounds, out sbyte number) || number > 10 || number < 0) 
            {
                Console.WriteLine("Input Erorr, Please Enter a Number between 1 to 10");
                goto Check;
            }
            return Convert.ToSByte(Rounds);
        }
        static void StartGame()
        {
            char Answer;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                sbyte Rounds = HowManyRounds();
                CreatRound(Rounds);
                Console.WriteLine("\n\n\t\t\t\tDo you want to play again? Y/N? ");
                Answer = Convert.ToChar(Console.ReadLine());
                if (Answer == 'Y' || Answer == 'y')
                {
                    Console.Clear();
                    Console.ResetColor();
                }
            }
            while (Answer == 'Y' || Answer == 'y');
        }
        static void Main(string[] args)
        {
            StartGame();
            
            Console.ReadKey();
        }
    }
}
