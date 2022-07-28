using System;


namespace String_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            StringCalculator stringCalculator = new StringCalculator();
            Console.Write("Enter value: ");
            string userNumber = Console.ReadLine();
            int result = stringCalculator.Add(userNumber);
            Console.WriteLine("Sum{0}", result);
            Console.Read();
        }
    }
}
