using UnityEngine;
using System.Collections;
using System;

public abstract class IBonus : IPooledObject
{

    private CustomTimer m_timer;
    void Awake()
    {
        Create();
    }
    public override void Create()
    {
        Cache();
        m_gameObject.SetActive(true);
        m_timer = new CustomTimer(10.0f, DestroyBonus, false);
        m_timer.Start();        
    }

    public override void Release()
    {
        Cache();
        m_gameObject.SetActive(false);
    }

    void Update()
    {
        m_timer.Update(Time.deltaTime);
        m_transform.position += Vector3.down * Time.deltaTime * 3.0f;
    }

    public abstract void Action(); 
    
    private void DestroyBonus()
    {
        m_timer.End();
        Release();
    }


    
}
