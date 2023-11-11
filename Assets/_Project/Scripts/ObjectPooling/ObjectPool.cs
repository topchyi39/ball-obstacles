using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.ObjectPooling
{
    public class ObjectPool
    {
        private IObjectPool<PooledObject> Pool
        {
            get
            {
                _pool ??= new ObjectPool<PooledObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                    OnDestroyPoolObject, _collectionChecks, _defaultCapacity, _maxPoolSize);
                return _pool;
            }
        }
        
        private IObjectPool<PooledObject> _pool;

        private readonly PooledObject _prefab;
        private readonly bool _collectionChecks;
        private readonly int _defaultCapacity;
        private readonly int _maxPoolSize;

        private List<PooledObject> _activatedObjects;

        public ObjectPool(PooledObject prefab, bool collectionChecks,  int defaultCapacity, int maxPoolSize)
        {
            _prefab = prefab;
            _collectionChecks = collectionChecks;
            _defaultCapacity = defaultCapacity;
            _maxPoolSize = maxPoolSize;
            _activatedObjects = new List<PooledObject>(maxPoolSize);
        }

        public PooledObject Get() => Pool.Get();
        
        private PooledObject CreatePooledItem()
        {
            var pooledObject = Object.Instantiate(_prefab);
            pooledObject.SetPool(Pool);
            return pooledObject;
        }

        private void OnReturnedToPool(PooledObject pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
            _activatedObjects.Remove(pooledObject);
        }

        private void OnTakeFromPool(PooledObject pooledObject)
        {
            pooledObject.TakeFromPool();
            pooledObject.gameObject.SetActive(true);
            _activatedObjects.Add(pooledObject);
        }

        private void OnDestroyPoolObject(PooledObject pooledObject)
        {
            Object.Destroy(pooledObject.gameObject);
        }
    }
}