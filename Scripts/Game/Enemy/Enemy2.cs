using UnityEngine;
using System.Collections;
using System;

public class Enemy2 : Enemy
{
    private float m_radius;
    private Vector2 m_bound;
    private CustomTimer m_timerBomb;
    void Awake()
    {
        Cache();
        m_radius = m_gameObject.GetComponent<CircleCollider2D>().radius;
        m_speed = UnityEngine.Random.Range(0.5f, 2.0f);
        m_bound = GameInstance.GetInstance().HalfSizeBattlefield;
        GetRandomDirectional();
        m_timerBomb = new CustomTimer(3.0f, CreateBomb, true);
    }
    protected override void Movement()
    {
        BoundMovement(m_bound, m_radius);
    }

    private void CreateBomb()
    {
        GameObject go = GameInstance.GetInstance().ManagerGE.Create<Bomb>();
        Bomb bomb = go.GetComponent<Bomb>();
        bomb.SetPosition(m_transform.position);
        bomb.Create();
    }

    void Update()
    {
        Movement();
        m_timerRandomDirectional.Update(Time.deltaTime);
        m_timerBomb.Update(Time.deltaTime);
    }
}
