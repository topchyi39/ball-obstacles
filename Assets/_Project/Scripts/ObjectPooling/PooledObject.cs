using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.ObjectPooling
{
    public abstract class PooledObject : MonoBehaviour
    {
        private IObjectPool<PooledObject> _pool;

        protected void ReturnToPool()
        {
            _pool.Release(this);
        }
        
        public void SetPool(IObjectPool<PooledObject> pool)
        {
            _pool = pool;
        }

        public virtual void TakeFromPool() {  }
    }
}