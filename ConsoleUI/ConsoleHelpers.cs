using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class ConsoleHelpers
    {
        public static void DisplayMenuOptions()
        {
            Console.WriteLine("Library Management App Menu");
            Console.WriteLine();
            Console.WriteLine("1. View Books");
            Console.WriteLine("2. Add Book");
            Console.WriteLine("3. Update Book");
            Console.WriteLine("4. Remove Book");
            Console.WriteLine("5. Exit");
            Console.WriteLine();
        }

        public static void Return()
        {
            Console.WriteLine("\nPress enter to return to the menu.");
            Console.ReadLine();
            Console.Clear();
        }

        public static void DisplayUpdateOptions()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to update?");
            Console.WriteLine();
            Console.WriteLine("1. Title");
            Console.WriteLine("2. Author");
            Console.WriteLine("3. Year Published");
            Console.WriteLine("4. Genre");
            Console.WriteLine();
        }

        public static string GetFieldToUpdate(int fieldNumber)
        {
            switch (fieldNumber)
            {
                case 1:
                    return "Title";
                case 2:
                    return "Author";
                case 3:
                    return "PublishedYear";
                case 4:
                    return "Genre";
                default:
                    throw new ArgumentException();
            }
        }

        public static object GetUpdatedValue(string fieldToUpdate, string message)
        {
            if (fieldToUpdate == "PublishedYear")
            {
                int output = GetInt(message, true, 0, 2035);
                return output;
            }
            else
            {
                string output = GetString(message);
                return output;
            }
        }

        public static string GetString(string message)
        {
            string output = "";

            while (string.IsNullOrWhiteSpace(output))
            {
                Console.Write(message);
                output = Console.ReadLine();
            }

            return output;
        }

        public static int GetInt(string message, bool useMinMax, int min = 0, int max = 0)
        {
            int output = 0;
            bool isValidInt = false;
            bool isValidRange = true;

            while (isValidInt == false || isValidRange == false)
            {
                Console.Write(message);
                string answer = Console.ReadLine();

                isValidInt = int.TryParse(answer, out output);

                if (useMinMax)
                {
                    isValidRange = output >= min && output <= max; 
                }

                if (isValidInt == false)
                {
                    Console.WriteLine("Invalid number.");
                    Console.WriteLine();
                }
                else if (isValidRange == false)
                {
                    Console.WriteLine("The number is not in a valid range.");
                    Console.WriteLine();
                }
            }

            return output;
        }
    }
}
