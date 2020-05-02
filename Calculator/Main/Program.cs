using System;
using UI;
using MathCustom;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = 0;

            Console.WriteLine("Welcome");

            do
            {
                Menu.ShowMenu();
                input = Menu.ReadOptionFromConsole("Choose an option");

                switch (input)
                {
                    case 0:
                        break;

                    case 1:
                        CallMethod("Sum");
                        break;
                    
                    case 2:
                        CallMethod("Subtract");
                        break;

                    case 3:
                        CallMethod("Multiply");
                        break;

                    case 4:
                        CallMethod("Divide");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again!");
                        break;
                }

            } while (input > 0);

            Menu.SayBye();
        }

        static void CallMethod(string methodName)
        {
            string[] methodsNames = { "Sum", "Subtract", "Multiply", "Divide" };

            if (!Array.Exists(methodsNames, name => name == methodName))
            {
                return;
            }

            decimal firstNumber = Menu.ReadDecimalFromConsole("Insert number:");
            decimal secondNumber = Menu.ReadDecimalFromConsole("Insert number:");

            switch (methodName)
            {
                case "Sum":
                    Console.WriteLine("Result: {0}", Calculator.SumTwoNumbers(firstNumber, secondNumber));
                    break;

                case "Subtract":
                    Console.WriteLine("Result: {0}", Calculator.SubtractTwoNumbers(firstNumber, secondNumber));
                    break;

                case "Multiply":
                    Console.WriteLine("Result: {0}", Calculator.MultiplyTwoNumbers(firstNumber, secondNumber));
                    break;

                case "Divide":
                    if (secondNumber == 0)
                    {
                        Console.WriteLine("0 isn't a valid number to divide");
                        break;
                    }
                    Console.WriteLine("Result: {0}", Calculator.DivideTwoNumbers(firstNumber, secondNumber));
                    break;

                default:
                    Console.WriteLine("Non-existing method.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
