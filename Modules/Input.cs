using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4Movie.Modules
{
    internal class Input
    {
        public static int GetIntWithPrompt(string prompt, string errorMessage)
        {
            bool conversionSuccessful = false;
            int userIntInput = 0;
            do
            {
                Console.Write(prompt);
                string? userInput = Console.ReadLine(); // the string? means we can allow a null or emptry string to be entered

                if (userInput == null || userInput == "")
                {
                    Console.WriteLine(errorMessage);
                    continue; //This sends it back to the start of the loop as we know it's not an integer without even running the TryParse
                }

                conversionSuccessful = Int32.TryParse(userInput, out userIntInput);

                if (!conversionSuccessful)
                {
                    Console.WriteLine(errorMessage);
                }
            } while (!conversionSuccessful);

            return userIntInput;
        }

        public static char GetCharWithPrompt(string prompt, string errorMessage)
        {
            char userCharInput = ' ';
            bool isChar = false;

            if (!String.IsNullOrEmpty(prompt))
            {
                Console.Write(prompt);
            }

            do
            {
                string userInput = Console.ReadLine();
                userInput = userInput.ToUpper();

                if (!String.IsNullOrEmpty(userInput) && userInput.Length == 1)
                {
                    userCharInput = userInput[0];
                    isChar = true;
                }
                else
                {
                    Console.Write(errorMessage);
                }

            } while (!isChar);

            return userCharInput;
        }
        public static string GetStringWithPrompt(string? prompt, string? errorMessage)
        {
            if (!String.IsNullOrEmpty(prompt))
                Console.Write(prompt);

            do
            {
                string? userInput = Console.ReadLine();

                if (!String.IsNullOrEmpty(userInput))
                {
                    return userInput;
                }
                else
                {
                    if (!String.IsNullOrEmpty(errorMessage))
                        Console.Write(errorMessage);
                }
            } while (true);
        }
    }
}
