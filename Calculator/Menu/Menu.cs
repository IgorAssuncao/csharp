using System;

namespace Menu
{
    public class Menu
    {
        public static void ShowMenu()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("0 - Exit");
            Console.WriteLine("1 - Sum");
            Console.WriteLine("2 - Subtract");
            Console.WriteLine("3 - Multiply");
            Console.WriteLine("4 - Divide");
            Console.WriteLine("--------------------");
        }

        public static void SayBye()
        {
            Console.WriteLine("Exiting... Bye!");
        }

        public static int ReadOptionFromConsole(string _message)
        {
            string _string = "";

            do
            {
                Console.WriteLine(_message);
                _string = Console.ReadLine();
                if (_string == "" || !Common.Validator.ValidateIntegersFromString(_string))
                {
                    Console.WriteLine("Oops, that's not valid option. Try again.");
                }
            } while (_string == "" || !Common.Validator.ValidateIntegersFromString(_string));

            return Int16.Parse(_string);
        }

        public static decimal ReadDecimalFromConsole(string _message)
        {
            string _string = "";

            do
            {
                Console.WriteLine(_message);
                _string = Console.ReadLine();
                if (_string == "" || !Common.Validator.ValidateDecimalsFromString(_string))
                {
                    Console.WriteLine("Oops, that's not valid number. Try again.");
                }
            } while (_string == "" || !Common.Validator.ValidateDecimalsFromString(_string));

            return Decimal.Parse(_string);
        }
    }
}
