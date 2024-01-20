using System;
using System.Collections.Generic;

namespace Source.BaseLibrary
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

        public static void Register(EventBinding<T> a_binding) => bindings.Add(a_binding);
        public static void Deregister(EventBinding<T> a_binding) => bindings.Remove(a_binding);

        public static void Raise(T @event)
        {
            foreach (var binding in bindings)
            {
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }
    }

    public static class PredefinedAssemblyUtil
    {
        
    }
}
