using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly Queue<T> pool = new Queue<T>();
    private readonly T prefab;
    private readonly Transform parent;

    public ObjectPool(T prefab, int initialCount, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        // Створюємо початкові об'єкти
        for (int i = 0; i < initialCount; i++)
        {
            AddObjectToPool();
        }
    }

    private void AddObjectToPool()
    {
        T obj = Object.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    public T Get()
    {
        if (pool.Count == 0)
        {
            AddObjectToPool();
        }

        T obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
