using System;
using System.Collections.Generic;

namespace _Project.Scripts.GameContext.Internal
{
    public interface IGameLocator
    {
        void AddService(object service);

        void AddServices(IEnumerable<object> services);

        void RemoveService(object service);

        T GetService<T>();
    }

    public sealed class GameLocator : IGameLocator
    {
        private readonly List<object> _services = new();

        public void AddServices(IEnumerable<object> services)
        {
            _services.AddRange(services);
        }

        public void AddService(object service)
        {
            _services.Add(service);
        }

        public void RemoveService(object service)
        {
            _services.Remove(service);
        }

        public T GetService<T>()
        {
            foreach (var service in _services)
            {
                if (service is T result)
                {
                    return result;
                }
            }

            throw new Exception($"Service of type {typeof(T)} is not found!");
        }
    }
}