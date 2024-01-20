using System;
using UnityEngine;

namespace Source.BaseLibrary
{
    /// <summary>
    /// Will show the usage of event bus 
    /// </summary>
    public class TestHero : MonoBehaviour
    {
        [SerializeField] private int m_health = 100;
        [SerializeField] private int m_mana = 10;
        
        private EventBinding<TestEvent> m_testEventBinding;
        private EventBinding<PlayerEvent> m_playerEventBinding;

        private void OnEnable()
        {
            m_testEventBinding = new EventBinding<TestEvent>(handleTestEvent);
            EventBus<TestEvent>.Register(m_testEventBinding);

            m_playerEventBinding = new EventBinding<PlayerEvent>(handlePlayerEvent);
            EventBus<PlayerEvent>.Register(m_playerEventBinding);
        }
        
        private void OnDisable()
        {
            EventBus<TestEvent>.Deregister(m_testEventBinding);
            EventBus<PlayerEvent>.Deregister(m_playerEventBinding);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EventBus<TestEvent>.Raise(new TestEvent());
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                EventBus<PlayerEvent>.Raise(new PlayerEvent
                {
                     health = m_health,
                     mana = m_mana
                });
            }
        }

        private void handleTestEvent()
        {
            Debug.Log("Test event received");
        }

        private void handlePlayerEvent(PlayerEvent a_playerEvent)
        {
            Debug.Log($"Player event received! Health: {a_playerEvent.health}, Mana: {a_playerEvent.mana}");
        }
    }
}