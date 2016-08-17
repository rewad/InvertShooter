using UnityEngine;
using System.Collections;
using System;

public class Enemy1 : Enemy {
    void Awake()
    {
        Cache();
        m_speed = UnityEngine.Random.Range(5.0f, 8.0f);
    }
   
    protected override void Movement()
    {
        SimpleMovement();
    }
    
   
	void Update ()
    {
        Movement();
        m_timerRandomDirectional.Update(Time.deltaTime);
	}
}
