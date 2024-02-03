using System;
using Source.BaseLibrary.Observer;

namespace Source.BaseLibrary.MVC.Coin
{
    public interface ICoinModel
    {
        Observable<int> Coins { get; }
        CoinData Serialize();
        void DeSerialize(CoinData savedData);
    }
    public class CoinModel : ICoinModel
    {
        private Observable<int> m_coins = new Observable<int>(0, onValueChanged);
        
        public Observable<int> Coins => m_coins;

        private static void onValueChanged(int obj)
        {
            
        }

        public CoinData Serialize()
        {
            // Implement serialization logic here if needed
            return new CoinData();
        }

        public void DeSerialize(CoinData savedData)
        {
            // Implement deserialization logic here if needed
        }
    }

    [Serializable]
    public struct CoinData
    {
        public int NumberOfCoins;

        public CoinData(int numberOfCoins)
        {
            NumberOfCoins = numberOfCoins;
        }
    }
}