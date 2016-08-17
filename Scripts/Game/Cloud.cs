using UnityEngine;
using System.Collections;
using System;

public class Cloud : IPooledObject
{
    private float m_direction;
    private Bounds m_boundCloud;
    private Bounds m_boundGame;
    private bool m_isIntersect;
    private SpriteRenderer m_cloudRenderer;
    private float m_speed;
    public override void Create()
    {
        Cache();
        m_gameObject.SetActive(true);
        if (!m_cloudRenderer)
        {
            m_cloudRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public override void Release()
    {
        Cache();
        m_gameObject.SetActive(false);
    }

    public void NewCloud(Bounds bound, Sprite sprite)
    {
        m_boundGame = bound;
        m_isIntersect = false;

        m_cloudRenderer.sprite = sprite;
        m_boundCloud = m_cloudRenderer.bounds;
        m_speed = UnityEngine.Random.Range(1, 3);

        int orientation = UnityEngine.Random.Range(0, 100);

        float x = m_boundGame.extents.x;
        float y = m_boundGame.extents.y;
        y = UnityEngine.Random.Range(-y, y - m_boundCloud.extents.y);

        if (orientation < 50)
        {
            m_direction = -1.0f;
            x += 0.1f;
        }
        else
        {
            x *= -1.0f;
            x -= m_boundCloud.extents.x * 2.0f;
            m_direction = 1.0f;
        }

        SetPosition(new Vector3(x, y, 0));
    }

    void Update()
    {
        if (m_boundCloud.Intersects(m_boundGame))
        {
            m_isIntersect = true;
        }
        else
        {
            if (m_isIntersect)
            {
                Release();
            }
        }
        AddPosition(Vector3.right * m_direction * m_speed * Time.deltaTime);
    }
}
