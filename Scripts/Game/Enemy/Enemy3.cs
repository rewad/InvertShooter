using UnityEngine;
using System.Collections;
using System;

public class Enemy3 : Enemy
{

    private CustomTimer m_timerShoot;
    void Awake()
    {
        Cache();
        m_speed = UnityEngine.Random.Range(0.2f, 3.0f);
        m_timerShoot = new CustomTimer(0.3f, Shoot, true);
        m_timerShoot.Start();
    }
    protected override void Movement()
    {
        SimpleMovement();
    }

    void Update()
    {
        Movement();
        m_timerShoot.Update(Time.deltaTime); 
    }

    private void Shoot()
    {
        var player = GameInstance.GetInstance().GetPlayer();
        var dir = player.GetPosition() - m_transform.position;
        dir.Normalize();

        GameObject bullet_go = GameInstance.GetInstance().ManagerGE.Create<BulletEnemy>();
        BulletEnemy bullet = bullet_go.GetComponent<BulletEnemy>();
        bullet.Direction = dir;
        bullet.SetPosition(m_transform.position);
    }
}
