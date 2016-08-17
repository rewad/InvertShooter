using UnityEngine;
using System.Collections;
using System;

public class Gold : IPooledObject
{
    private CustomTimer m_timerDeath;
    public override void Create()
    {
        Cache();
        m_gameObject.SetActive(true);
        m_timerDeath = new CustomTimer(3.0f, Release, false);
        m_timerDeath.Start();
    }

    void Update()
    {
        m_timerDeath.Update(Time.deltaTime); 
    }
     
    public override void Release()
    {
        Cache();
        m_gameObject.SetActive(false);
    }

    public  void Action()
    {
        Release();
    }
}
