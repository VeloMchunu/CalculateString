using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace String_Calculator
{
    public class StringCalculator
    {
        //other delimitors can be added if required in future
       
        private const string seperator = "//";
        private List<string> Delimitors = new List<string>() { ",", "\n",";","||" };

        public int Add(string numbers)
        {
            if (String.IsNullOrWhiteSpace(numbers))
            {
                return 0;
            }
            if (numbers.StartsWith(seperator))
            {
                numbers = AddCustomDelemitors(numbers);
            }
            var digits = RemoveNonDigits(numbers);

            return digits.Sum();
        }

        private string AddCustomDelemitors(string numbers)
        {
            string[] customSeperators = { seperator, "[", "]" };
            var customSeperator = numbers.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).First();

            numbers = numbers.Substring(customSeperator.Length, numbers.Length - customSeperator.Length);
            var allCustomSeperators = customSeperator.Split(customSeperators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var sep in allCustomSeperators)
            {
                Delimitors.Add(sep);
            }
            return numbers;
        }

        private IList<int> RemoveNonDigits(string numbers)
        {
            var digits = numbers.Split(Delimitors.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var numberOnly = new List<int>();
            foreach (var num in digits)
            {   
                var number = int.Parse(num);
                if (number < 0)
                {
                    throw new ApplicationException(string.Format("Negatives not allowed:{0}" , number));
                }
                //assumption made that the numbers being added will not surpass the max va=lue an int can hold
                if (number <= int.MaxValue)
                {
                    numberOnly.Add(number);
                }
            }
            return numberOnly;
        }
    }
    //The Tests could be refractoredby adding Test Cases for the methods to allow for multiple test
    //scenarios without haing to rewrite one test manually.

   
    public class StringCalculatorTests
    {
        StringCalculator calc;
        [SetUp]
        public void setup()
        {
            calc = new StringCalculator();
        }
        [Test]
        public void  ShouldReturnZeroifEmpty()
        {
            var result = calc.Add("");
            Assert.AreEqual(0, result);
        }
        [Test]
        public void ShouldReturnSameNumber()
        {
            var result = calc.Add("1");
            Assert.AreEqual(1, result);
        }
       
        [Test]
        public void ShouldReturnSumofTwoNumbers()
        {
            var result = calc.Add("1,2");
            Assert.AreEqual(3, result);
        }
        [Test]
        public void ShouldReturnSumofTwoNumbers2()
        {
            var result = calc.Add("1,3");
            Assert.AreEqual(4, result);
        }
        
        [Test]
        public void ShouldReturnZeroNewLine()
        {
            var result = calc.Add("\n");
            Assert.AreEqual(0, result);
        }
        [Test]
        public void TreatNewLineAsDelimitor()
        {
            var result = calc.Add("1\n2");
            Assert.AreEqual(3, result);
        }
        [Test]
        public void DiffrentDelimitorsAddNumbers()
        {
            var result = calc.Add("//A\n1A2");
            Assert.AreEqual(3, result);
        }
        [Test]
        public void DiffrentDelimitorsAddNumbersOther()
        {
            var result = calc.Add("//B\n1B2");
            Assert.AreEqual(3, result);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void ShouldShowExceptionOnNegativeNumber()
        {
            var result = calc.Add("-1");
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void ShouldShowAllNegatives()
        {
            var result = calc.Add("-1,-2");
        }
        
        [Test]
        public void ShouldAddMultipleDelimiters()
        {
            var result = calc.Add("//[A][b]\n1A2b3");
            Assert.AreEqual(6, result);
        }
        

    }
}
