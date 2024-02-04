using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Source.BaseLibrary.MVC.Coin;
using Source.BaseLibrary.Observer;
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
            Assert.That(list, NUnit.Framework.Is.Unique);
            Assert.That(list, Has.Exactly(3).Matches<int>(NumberPredicates.IsOdd));
        }

        private ICoinController m_controller;
        private ICoinView m_view;
        private ICoinService m_service;
        private ICoinModel m_model;
        private CoinControllerBuilder m_builder;


        [SetUp]
        public void SetUp()
        {
            m_view = Substitute.For<ICoinView>();
            m_model = Substitute.For<ICoinModel>();
            m_service = Substitute.For<ICoinService>();

            Assert.That(m_view, NUnit.Framework.Is.Not.Null);
            Assert.That(m_service, NUnit.Framework.Is.Not.Null);
            Assert.That(m_model, NUnit.Framework.Is.Not.Null);

            m_model.Coins.Returns(new Observable<int>(0));

            Assert.That(m_model.Coins, NUnit.Framework.Is.Not.Null);
            Assert.That(m_model, NUnit.Framework.Has.Property("Coins").Not.Null);

            m_service.Load().Returns(m_model);

            m_controller = new CoinController(m_view, m_service);
            
            Assert.That(m_controller, NUnit.Framework.Is.Not.Null);
            
        }
        [TearDown]
        public void TearDown(){}

        [Test]
        public void CoinControllerBuilder_ShouldThrowArgumentNullException_WhenViewIsNull()
        {
            Assert.That(()=> new CoinControllerBuilder(null,m_service).Build(),Throws.ArgumentNullException);
            Assert.Throws<ArgumentNullException>(()=> new CoinControllerBuilder(null,m_service).Build());
        }
        [Test]
        public void CoinControllerBuilder_ShouldThrowArgumentNullException_WhenServiceIsNull()
        {
            Assert.That(()=> new CoinControllerBuilder(m_view,null).Build(),Throws.ArgumentNullException);
            Assert.Throws<ArgumentNullException>(()=> new CoinControllerBuilder(m_view,null).Build());
        }

        [Test]
        public void UpdateView_ShouldUpdateCoinsDisplay_WhenCoinsAreCollected()
        {
            m_controller.Collect(1);
            m_view.Received(1).UpdateCoinsDisplay(1);
        }

        [TestCase(5, 5, 10)]
        [TestCase(0, 2, 2)]
        [TestCase(1, 3, 4)]
        public void Collect_ShouldCoins_WhenCollectedAPositiveNumber( int a_initialCoins
                                                                    , int a_coinsToAdd
                                                                    , int a_expectedCoin)
        {
            m_model.Coins.Returns(new Observable<int>(a_initialCoins));
            m_controller.Collect(a_coinsToAdd);
            Assert.That(m_model.Coins.Value, NUnit.Framework.Is.EqualTo(a_expectedCoin));
        }
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