using UnityEngine;
using System.Collections;

public class Sky : MonoBehaviour
{
    private Sprite[] m_cloudSprites;
    private CustomTimer m_timer;
    private Bounds m_bound;
    void Awake()
    {
        m_cloudSprites = new Sprite[6];
        for (int i = 1; i <= 6; i++)
        {
            var texture = Resources.Load("Clouds\\Cloud" + i.ToString()) as Texture2D;
            m_cloudSprites[i - 1] = Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width,texture.height)), Vector2.zero);
        }
        m_timer = new CustomTimer(5.0f, NewCloud, true);
        m_bound = new Bounds();
        m_bound.center = Vector3.zero;

        Vector2 bound = GameInstance.GetInstance().HalfSizeBattlefield;
        m_bound.extents = new Vector3(bound.x, bound.y);
        NewCloud();
    }


    void Update()
    {
        m_timer.Update(Time.deltaTime);
    }

    private void NewCloud()
    {
        Cloud cloud = GameInstance.GetInstance().ManagerGE.Create<Cloud>().GetComponent<Cloud>();
        cloud.Create();
        cloud.NewCloud(m_bound, m_cloudSprites[UnityEngine.Random.Range(0, 6)]);
    }
}
