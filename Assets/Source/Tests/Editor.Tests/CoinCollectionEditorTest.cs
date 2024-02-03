using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Source.BaseLibrary.MVC.Coin;
using UnityEngine.TestTools;
using Is = UnityEngine.TestTools.Constraints.Is;

namespace Source.Tests.Editor.Tests
{
    public class CoinCollectionEditorTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void CoinCollectionEditorTestSimplePasses()
        {
            //1st level Is/Has/Does/Contains
            //2nd level All/Not/Some/Exactly
            //Or/And/Not
            //Is unique/Is ordered
            //Assert.IsTrue

            string username = "Username123";
            Assert.That(username, Does.StartWith("U"));
            Assert.That(username, Does.EndWith("3"));

            var list = new List<int> { 1, 2, 3, 4, 5 };
            Assert.That(list, Contains.Item(3));
            Assert.That(list, NUnit.Framework.Is.All.Positive);
            Assert.That(list, Has.Exactly(2).LessThan(3));
            Assert.That(list, NUnit.Framework.Is.Ordered);
            Assert.That(list,NUnit.Framework.Is.Unique);
            Assert.That(list, Has.Exactly(3).Matches<int>(NumberPredicates.IsOdd));
        }

        private ICoinController m_controller;
        private ICoinView m_view;
        

    }

    public static class NumberPredicates
    {
        public static bool IsEven(int a_number)
        {
            return a_number % 2 == 0;
        }
        public static bool IsOdd(int a_number)
        {
            return a_number % 2 != 0;
        }
    }
    
    
}
