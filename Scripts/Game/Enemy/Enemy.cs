using UnityEngine;
using System.Collections;

public abstract class Enemy : IPooledObject
{


    protected float m_speed;
    protected CustomTimer m_timerRandomDirectional;
    private Vector3 m_randDirectional;
    private Vector3 m_directionalMovement;


    public override void Create()
    {
        Cache();
        m_gameObject.SetActive(true);
        m_timerRandomDirectional = new CustomTimer(1.0f, GetRandomDirectional, true);
        m_timerRandomDirectional.Start();
        m_directionalMovement = GetRandomVector3();
    }

    public override void Release()
    {
        Cache();
        m_gameObject.SetActive(false);
    }

    protected abstract void Movement();
    protected void SimpleMovement()
    {
        var player = GameInstance.GetInstance().GetPlayer();
        if (player == null) return;

        var pos_player = player.GetPosition();
        m_directionalMovement = pos_player - m_transform.position;
        m_directionalMovement.Normalize();

        m_transform.position += (m_randDirectional + m_directionalMovement) * m_speed * Time.deltaTime;
    }
    protected void BoundMovement(Vector2 bound, float radius)
    {
        if ((m_transform.position.x - radius) < -bound.x)
            m_directionalMovement = Vector3.Reflect(m_directionalMovement, Vector3.right);
        if ((m_transform.position.x + radius) > bound.x)
            m_directionalMovement = Vector3.Reflect(m_directionalMovement, -Vector3.left);
        if ((m_transform.position.y - radius) < -bound.y)
            m_directionalMovement = Vector3.Reflect(m_directionalMovement, Vector3.up);
        if ((m_transform.position.y + radius) > bound.y)
            m_directionalMovement = Vector3.Reflect(m_directionalMovement, Vector3.down);
        m_transform.position += (m_randDirectional * 0.001f + m_directionalMovement) * m_speed * Time.deltaTime;

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        Damage();
    }

    public void Damage()
    {
        GameInstance.GetInstance().EnemyDestroy(); 
        GameObject gold =  GameInstance.GetInstance().ManagerGE.Create<Gold>();
        gold.transform.position = m_transform.position;
        Release();
    }
       
    private Vector3 GetRandomVector3()
    {
        float x = UnityEngine.Random.Range(-10.0f, 10.0f);
        float y = UnityEngine.Random.Range(-10.0f, 10.0f);
        return new Vector3(x, y, 0).normalized;
    }
    protected void GetRandomDirectional()
    {
        m_randDirectional = GetRandomVector3() * UnityEngine.Random.Range(0.2f, 0.5f);
    }
}
