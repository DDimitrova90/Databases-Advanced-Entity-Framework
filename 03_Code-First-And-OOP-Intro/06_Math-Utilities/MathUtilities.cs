namespace _06_Math_Utilities
{
    using System;

    public class MathUtilities
    {
        public static void Main()
        {
            string input = Console.ReadLine();

            while (input != "End")
            {
                string[] operationArgs = input.Split(' ');

                string operation = operationArgs[0];
                decimal firstNumber = decimal.Parse(operationArgs[1]);
                decimal secondNumber = decimal.Parse(operationArgs[2]);

                switch (operation)
                {
                    case "Sum":
                        Console.WriteLine($"{MathUtil.Sum(firstNumber, secondNumber):F2}");
                        break;
                    case "Subtract":
                        Console.WriteLine($"{MathUtil.Substract(firstNumber, secondNumber):F2}");
                        break;
                    case "Multiply":
                        Console.WriteLine($"{MathUtil.Multiply(firstNumber, secondNumber):F2}");
                        break;
                    case "Divide":
                        Console.WriteLine($"{MathUtil.Divide(firstNumber, secondNumber):F2}");
                        break;
                    case "Percentage":
                        Console.WriteLine($"{MathUtil.Percentage(firstNumber, secondNumber):F2}");
                        break;
                }

                input = Console.ReadLine();
            }
        }
    }
}
