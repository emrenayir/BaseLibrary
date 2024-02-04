using Source.BaseLibrary.Util;

namespace Source.BaseLibrary.MVC.Coin
{
    public class CoinControllerBuilder
    {
        private ICoinController m_controller;
        private ICoinView m_view;
        private ICoinService m_service;

        public CoinControllerBuilder( ICoinView a_view
                                     , ICoinService a_service)
        {
            m_view = a_view;
            m_service = a_service;
        }
        public CoinControllerBuilder WithView(ICoinView a_view)
        {
            m_view = a_view;
            return this;
        }

        public CoinControllerBuilder WithService(ICoinService a_service)
        {
            m_service = a_service;
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