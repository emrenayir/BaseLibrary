namespace Source.BaseLibrary.MVC.Coin
{
    public interface ICoinView
    {
        void UpdateCoinsDisplay(int coins);
    }

    public class CoinView : ICoinView
    {
        // Implement the actual logic for updating the UI or displaying coins in your game
        public void UpdateCoinsDisplay(int coins)
        {
            // For example, in Unity, you might update a text component
            // assuming have a Text component named coinText in your view
            // coinText.text = "Coins: " + coins;
        }
    }
}