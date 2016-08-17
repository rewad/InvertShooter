using UnityEngine;
using System.Collections;
using System;

public class Bomb : IPooledObject
{
    private CustomTimer m_timer;
    public override void Create()
    {
        Cache();
        m_gameObject.SetActive(true);
        m_timer = new CustomTimer(3.0f, Explosion, false);
        m_timer.Start();

    }

    public override void Release()
    {
        Cache();
        if (m_timer !=null) m_timer.End();
        m_gameObject.SetActive(false);
    }


    void Update()
    {
        m_timer.Update(Time.deltaTime);
    }


    private void Explosion()
    {
        if (Vector3.Distance(m_transform.position, GameInstance.GetInstance().GetPlayer().GetPosition()) < 2.0f)
        {
            GameInstance.GetInstance().GetPlayer().Damage();
        }

        Release();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            GameInstance.GetInstance().GetPlayer().Damage();
        }
        Release();
    }
}
