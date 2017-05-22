namespace _06_Math_Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MathUtil
    {
        public MathUtil(decimal firstNumber, decimal secondNumber)
        {
            this.FirstNumber = firstNumber;
            this.SecondNumber = secondNumber;
        }

        public decimal FirstNumber { get; set; }
        public decimal SecondNumber { get; set; }

        public static decimal Sum(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber + secondNumber;
        }

        public static decimal Substract(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber - secondNumber;
        }

        public static decimal Multiply(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber * secondNumber;
        }

        public static decimal Divide(decimal firstNumber, decimal secondNumber)
        {
            if (secondNumber != 0M)
            {
                return firstNumber / secondNumber;
            }
            else
            {
                throw new ArgumentException("Cannot divide to zero!");
            }
        }

        public static decimal Percentage(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber * (secondNumber / 100M);
        }
    }
}
