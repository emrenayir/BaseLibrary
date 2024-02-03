using Source.BaseLibrary.Util;

namespace Source.BaseLibrary.MVC.Coin
{
    public interface ICoinController
    {
        void Collect(int coins);
        void UpdateView(int coins);

        void Save();
        ICoinModel Load();
    }

    public class CoinController : ICoinController
    {
        private readonly ICoinModel m_model;
        private readonly ICoinView m_view;
        private readonly ICoinService m_service;

        public CoinController( ICoinView a_view
                             , ICoinService a_service )
        {
            Preconditions.CheckNotNull(a_view, "CoinView cannot be null");
            Preconditions.CheckNotNull(a_service, "CoinService cannot be null");

            this.m_view = a_view;
            this.m_service = a_service;

            m_model = Load();
            m_model.Coins.AddListener(UpdateView);
            m_model.Coins.Invoke();
        }

        public void Collect(int coins) => m_model.Coins.Set(m_model.Coins.Value + coins);

        public void UpdateView(int coins) => m_view.UpdateCoinsDisplay(coins);

        public void Save() => m_service.Save(m_model);
        public ICoinModel Load() => m_service.Load();
        
        //TODO Builder
    }
}
