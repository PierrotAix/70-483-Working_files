using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FizzBuzz.Tests
{
    [TestClass]
    public class Evaluator
    {
        [TestMethod]
        public void Method_Scenario_Expetation()
        {
            // arrange
            var expected = 1;

            // act
            var actual = 1;

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Evaluate_NeitherDiviseByThreeNorFive_ReturnNumber()
        {
            // arrange
            var expected = 2;

            // act
            var actual = FizzBuzz.Evaluator.Evaluate(expected);

            // assert
            Assert.AreEqual(expected, actual);
            //Assert.ThrowsException<NotImplementedException>( () => FizzBuzz.Evaluator.Evaluate(2) );
        }

        //[TestMethod]
        //public void Evaluate_DivideByThree_ReturnFizz()
        //{
        //    // arrange
        //    var expected = "Fizz";

        //    // act
        //    var actual = FizzBuzz.Evaluator.Evaluate(3);

        //    // assert
        //    Assert.AreEqual(expected, actual);           
        //}

        [DataTestMethod]
        [DataRow(3)]
        [DataRow(6)]
        public void Evaluate_DivideByThree_ReturnFizz(int value)
        {
            // arrange
            var expected = "Fizz";

            // act
            var actual = FizzBuzz.Evaluator.Evaluate(value);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Evaluate_DivideByFive_ReturnBuzz()
        {
            // arrange
            var expected = "Buzz";

            // act
            var actual = FizzBuzz.Evaluator.Evaluate(5);

            // assert
            Assert.AreEqual(expected, actual);
        }


    }
}
