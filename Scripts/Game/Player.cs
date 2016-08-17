using UnityEngine;
using System.Collections;

public class Player : CacheObject
{

    private float m_speed; 
    private Vector3 m_prevPosition;
    private float m_radiusPlayer;
    private int m_life;
    private CustomTimer m_customTimer;
    private int m_inputMode;
    private int m_score; 
    void Start()
    {
        Cache(); 
        m_radiusPlayer = GetComponent<CircleCollider2D>().radius;
        m_speed = 15.0f;
        m_life = 3;
        m_customTimer = new CustomTimer(0.05f, ShootDirection, true);
        m_customTimer.Start();
        m_inputMode = PlayerPrefs.GetInt("Input");
        SetPosition(Vector3.zero);
        m_score = 0;
    }

    public void Restart()
    {
        m_life = 3;
        m_score = 0;
        m_customTimer.Restart();
        m_prevPosition = Vector3.zero;
        SetPosition(Vector3.zero);
        m_inputMode = PlayerPrefs.GetInt("Input");
        GameInstance.GetInstance().UpdateGameUI(m_score, m_life);
    }

    public int GetLife()
    {
        return m_life;
    }
    void FixedUpdate()
    {
        Vector3 dir = Vector3.zero;
        if (m_inputMode == 0)
        {
            dir = (Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up).normalized;
            dir *= Time.deltaTime * m_speed;
        }
        else
        {
            Vector3 mouse_position = Input.mousePosition;
            mouse_position.z = 0.0f;
            Vector3 world_position = Camera.main.ScreenToWorldPoint(mouse_position);
            world_position.z = 0.0f;
            if (Vector3.Distance(GetPosition(), world_position) > 0.5f)
            {
                dir = (world_position - m_transform.position).normalized;
                dir *= Time.deltaTime * m_speed * 0.6f;
            }
        }

        m_prevPosition = GetPosition();
        m_transform.Translate(dir);
        BoundScreenCollision();



    }

    private void BoundScreenCollision()
    {
        Vector2 bound_screen = GameInstance.GetInstance().HalfSizeBattlefield;
        Vector3 current_position = m_transform.position;

        if (-bound_screen.x > m_transform.position.x - m_radiusPlayer)
            current_position.x = -bound_screen.x + m_radiusPlayer + 0.1f;
        if (-bound_screen.y > m_transform.position.y - m_radiusPlayer)
            current_position.y = -bound_screen.y + m_radiusPlayer + 0.1f;
        if (bound_screen.x < m_transform.position.x + m_radiusPlayer)
            current_position.x = bound_screen.x - m_radiusPlayer - 0.1f;
        if (bound_screen.y < m_transform.position.y + m_radiusPlayer)
            current_position.y = bound_screen.y - m_radiusPlayer - 0.1f;
        m_transform.position = current_position;
    }

    void Update()
    {
        bool is_stop = false;
        if ((m_prevPosition - m_transform.position).magnitude == 0.0f)
            is_stop = true;

        if (!is_stop)
        {
            m_customTimer.Update(Time.deltaTime);
        }
       
    }

    private void ShootDirection()
    {
        Vector3 dir = m_prevPosition - m_transform.position;
        dir.Normalize();
        if (dir.magnitude < 0.1f) dir = Vector3.right;

        Shoot(dir);
    }

    private void Shoot(Vector3 dir)
    {
        GameObject bullet_go = GameInstance.GetInstance().ManagerGE.Create<BulletPlayer>();
        BulletPlayer bullet = bullet_go.GetComponent<BulletPlayer>();
        bullet.Direction = dir;
        bullet.SetPosition(m_transform.position);
    }

    public void CircleShoot()
    {
        for (int i = 0; i < 20; i++)
            Shoot(Quaternion.Euler(0, 0, i * 36.0f) * Vector3.right);
    }


    public void Damage()
    {
        m_life--;
        if (m_life < 0) m_life = 0;
        GameInstance.GetInstance().UpdateGameUI(m_score, m_life);

        if (m_life <= 0)
        {
            GameInstance.GetInstance().GameOver();
        }
    }
    public int GetScore()
    {
        return m_score;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        Damage();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Gold")
        {
            m_score++;
            Gold money = other.GetComponent<Gold>();
            money.Release();
            GameInstance.GetInstance().UpdateGameUI(m_score, m_life); 
        }
        else if (other.tag == "Bonus")
        {
            IBonus bonus = other.GetComponent<IBonus>();
            bonus.Action();
            bonus.Release();
        }

    }

}
