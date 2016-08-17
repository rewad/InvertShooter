using System;
using UnityEngine;
using System.Collections.Generic;
public class ManagerGameEntity
{

    private Dictionary<Type, TPool> m_dictPools;
    public ManagerGameEntity()
    {
        m_dictPools = new Dictionary<Type, TPool>();
    }

    public void AddPool(Type type, string name, int size = 5)
    {
        if (m_dictPools.ContainsKey(type)) return;

        var new_pool = new TPool();
        new_pool.CreatePool(name, size);
        m_dictPools.Add(type, new_pool);
    }

    public GameObject Create<T>()
    {
        TPool pool;
        if (m_dictPools.TryGetValue(typeof(T), out pool))
        {
            return pool.GetObject();
        }
        return null;
    }

    public void ClearManager()
    {
        foreach (var pool in m_dictPools)
            pool.Value.ClearPool();
    }

}
