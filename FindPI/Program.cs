using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindPI
{
    class Program
    {
        static readonly char[] acceptedAnswers = new char[] { 'y', 'Y', 'n', 'N' };
        static readonly uint numberOfDecimalPlacesToWarnUser = 100000;

        /// <summary>
        /// Find PI to the Nth Digit - Enter a number and have the program generate PI up to that many decimal places. Keep a limit to how far the program will go.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            while (true)
            {
                // Get number of decimal places.
                uint numberOfDecimalPlaces = GetNumberOfDecimalPlaces();

                // Calculate and show PI.
                Console.WriteLine(HighPrecision.GetPi((int)numberOfDecimalPlaces));

                // Find out if calculate another PI or exit.
                if (!UserWantsMore())
                    break;
            }
        }

        /// <summary>
        /// Method forces user to enter a valid input (number of decimal places) and returns it.
        /// </summary>
        /// <returns></returns>
        private static uint GetNumberOfDecimalPlaces()
        {
            uint? numberOfDecimalPlaces = null;
            do
            {
                Console.Write("Enter a number of decimal places: ");
                numberOfDecimalPlaces = ValidateAndCastToNumber(Console.ReadLine());

            } while (!numberOfDecimalPlaces.HasValue);

            return numberOfDecimalPlaces.Value;
        }

        /// <summary>
        /// Method forces user to enter a valid input (Y/y - for continue, N/n - for exit) and return it as a boolean.
        /// </summary>
        /// <returns></returns>
        private static bool UserWantsMore()
        {
            bool? wantMore = null;
            do
            {
                Console.Write("Want more? (y/n) ");
                string answer = Console.ReadLine();
                wantMore = CastAnswerToBool(answer != string.Empty ? answer[0] : '\0');
            } while (!wantMore.HasValue);

            return wantMore.Value;
        }

        /// <summary>
        /// Method tries to parse and validate user's input and returns it if is valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static uint? ValidateAndCastToNumber(string value)
        {
            uint number;
            if (uint.TryParse(value, out number))
            {
                if (number >= numberOfDecimalPlacesToWarnUser)
                {
                    bool? calculate = null;
                    do
                    {
                        Console.WriteLine("Calculating PI with so many decimal places can take a long time depending on your hardware. Do you really want to calulcate it? (y/n)");
                        string answer = Console.ReadLine();
                        calculate = CastAnswerToBool(answer != string.Empty ? answer[0] : '\0');
                    } while (!calculate.HasValue);

                    if (!calculate.Value)
                        return default(uint?);
                }
                return number;
            }

            return default(uint?);
        }

        /// <summary>
        /// Method casts user's answer to nullable boolean (Y/y - true, N/n - false, otherwise - null)
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        private static bool? CastAnswerToBool(char answer)
        {
            if (acceptedAnswers.Contains(answer))
            {
                return answer.Equals('y') || answer.Equals('Y');
            }
            else
            {
                return null;
            }
        }
    }
}
