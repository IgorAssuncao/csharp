namespace Calculator
{
    public class Calculator : ICalculator
    {
        public decimal SumTwoNumbers(decimal number, decimal otherNumber)
        {
            return number + otherNumber;
        }
        public decimal SubtractTwoNumbers(decimal number, decimal otherNumber)
        {
            return number - otherNumber;
        }
    }
}
