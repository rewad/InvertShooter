using UnityEngine;
using System.Collections;

public class BulletEnemy : IPooledObject {
    private CustomTimer m_timer;
    void Awake()
    {
        Create();
    }
    public Vector3 Direction
    {
        get
        {
            return m_direction;
        }
        set
        {
            m_direction = value;
        }
    }

    private Vector3 m_direction;

    void Update()
    {
        m_transform.Translate(Time.deltaTime * m_direction * 10.0f);
        m_timer.Update(Time.deltaTime);
    }

    public override void Create()
    {
        Cache();
        m_timer = new CustomTimer(3.0f, Release);
        m_timer.Restart();
        m_gameObject.SetActive(true);
    }
 
    void OnCollisionEnter2D(Collision2D coll)
    {
        Release();
    }
    public override void Release()
    {
        Cache();
        m_timer.End();
        m_gameObject.SetActive(false);
    } 
    
     
}
