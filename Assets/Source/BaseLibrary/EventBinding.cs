using System;

namespace Source.BaseLibrary
{
    internal interface IEventBinding<T> 
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }
    
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        private Action<T> onEvent = _ => { };
        private Action onEventNoArgs = () => { };

        Action<T> IEventBinding<T>.OnEvent
        {
            get => onEvent;
            set => onEvent = value;
        }

        Action IEventBinding<T>.OnEventNoArgs
        {
            get => onEventNoArgs; 
            set => onEventNoArgs = value;
        }

        public EventBinding(Action<T> a_onEvent) => this.onEvent = a_onEvent;

        public EventBinding(Action a_onEventNoArgs) => this.onEventNoArgs = a_onEventNoArgs;

        public void Add(Action a_onEvent) => this.onEventNoArgs += a_onEvent;

        public void Remove(Action a_onEvent) => this.onEventNoArgs -= a_onEvent;

        public void Add(Action<T> a_onEvent) => this.onEvent += a_onEvent;

        public void Remove(Action<T> a_onEvent) => this.onEvent -= a_onEvent;
    }
}