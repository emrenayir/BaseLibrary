using Source.BaseLibrary.Util;

namespace Source.BaseLibrary.MVC.Coin
{
    public class CoinControllerBuilder
    {
        private ICoinView m_view;
        private ICoinService m_service;

        public CoinControllerBuilder WithView(ICoinView view)
        {
            m_view = view;
            return this;
        }

        public CoinControllerBuilder WithService(ICoinService service)
        {
            m_service = service;
            return this;
        }

        public CoinController Build()
        {
            Preconditions.CheckNotNull(m_view, "CoinView cannot be null");
            Preconditions.CheckNotNull(m_service, "CoinService cannot be null");

            ICoinModel model = m_service.Load();
            model.Coins.AddListener(m_view.UpdateCoinsDisplay);
            model.Coins.Invoke();

            return new CoinController(m_view, m_service);
        }
    }
}