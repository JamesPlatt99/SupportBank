﻿using System;
using System.Collections.Generic;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ConsoleApp1
{
    internal class Program
    {
        public static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private static void Main()
        {
            Console.ForegroundColor = System.ConsoleColor.White;
            Console.BackgroundColor = System.ConsoleColor.DarkCyan;
            int choice;
            ConfigureNLog();
            logger.Log(LogLevel.Info, "The program was started.");
            PrintMainMenu();
            while ((choice = GetIntValueBetween(0, 4)) != 4)
            {
                switch (choice)
                {
                    case 0:
                        logger.Log(LogLevel.Info, "The user chose the default option.");
                        Default def = new Default();
                        break;

                    case 1:
                        logger.Log(LogLevel.Info, "The user chose the import option.");
                        TransactionListReader transactionReader = new TransactionListReader();
                        transactionReader.Start();
                        break;

                    case 2:
                        logger.Log(LogLevel.Info, "The user chose the export option.");
                        TransactionListCreator transactionListCreator = new TransactionListCreator();
                        transactionListCreator.Start();
                        break;

                    case 3:
                        logger.Log(LogLevel.Info, "The user chose the convert option.");
                        ConvertFile convert = new ConvertFile();
                        convert.Start();
                        break;
                }
                PrintMainMenu();
            }
        }

        private static void PrintMainMenu()
        {
            Console.WriteLine("Support Bank");
            Console.WriteLine("-----------------------------");
            Console.WriteLine(" 0. Default");
            Console.WriteLine(" 1. Read From file");
            Console.WriteLine(" 2. Create new transactions list");
            Console.WriteLine(" 3. Convert");
            Console.WriteLine();
            Console.WriteLine(" 4. Exit");
        }

        public static int GetIntValueBetween(int minValue, int maxValue)
        {
            string inputStr;
            int inputInt;
            while (true)
            {
                inputStr = Console.ReadLine();
                if (int.TryParse(inputStr, out inputInt))
                {
                    if (inputInt >= minValue && inputInt <= maxValue)
                    {
                        return inputInt;
                    }
                }
                Console.WriteLine("Please try again.");
            }
        }

        public static string ChooseFile()
        {
            string[] files = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory());
            int choice;
            List<string> validFiles = new List<string>();
            foreach (string file in files)
            {
                foreach (ValidFiles fileType in Enum.GetValues(typeof(ValidFiles)))
                {
                    if (file.Substring(file.Length - fileType.ToString().Length) == fileType.ToString())
                    {
                        validFiles.Add(file);
                    }
                }
            }
            Console.WriteLine("Choose file:");
            for (int i = 0; i < validFiles.Count; i++)
            {
                Console.WriteLine("   {0} - {1}", i, validFiles[i]);
            }
            choice = GetIntValueBetween(0, validFiles.Count - 1);
            return validFiles[choice];
        }

        public static string ChooseFileType(string method)
        {
            string input;
            int output;
            int i = 0;
            Console.WriteLine("Choose the file type to {0}: ", method);

            foreach (Program.ValidFiles fileType in Enum.GetValues(typeof(Program.ValidFiles)))
            {
                Console.WriteLine("    {0}. {1}", i, fileType.ToString());
                i++;
            }
            Console.WriteLine();
            Console.WriteLine("     {0}. Back", i + 1);
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out output))
                {
                    if (output >= 0 && output <= i + 1)
                    {
                        int j = 0;
                        foreach (Program.ValidFiles fileType in Enum.GetValues(typeof(Program.ValidFiles)))
                        {
                            if (j == output)
                            {
                                return fileType.ToString();
                            }
                            j++;
                        }
                    }
                    else
                    {
                        Program.logger.Log(LogLevel.Warn, String.Format("Invalid input \"{0}\" from user", input));
                        Console.WriteLine("Invalid input, please try again.");
                    }
                }
            }
        }

        public enum ValidFiles
        {
            csv,
            json,
            xml
        }

        private static void ConfigureNLog()
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = "SupportBankLog.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
        }
    }
}